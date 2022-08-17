using Cinemachine;
using Platformer.Mechanics;
using Platformer.Unique;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer.Bosses
{
    public class VineBossControllerEnd : MonoBehaviour
    {
        public float initialWaitTime = 4f;
        public float stageOneLength = 20;
        public float[] stageOneSpawnInterval = { 3f, 6f };
        public float stageTwoLength = 20;
        public float[] stageTwoSpawnInterval = { 1f, 3f };
        public float stageThreeLength = 20;
        public float[] stageThreeSpawnInterval = { .5f, 2f };
        public float[] yRange = new float[2];
        public float[] xValues = new float[2];
        public float warningTime = 2f;
        public float vineSpawnTime = 4f;
        public float moveSpeed = 30f;
        public int damage = 10;
        public VineSpawner vineSpawner;
        public Vine vine;
        public GameObject dummyPoint;
        public PlayerController playerController;

        private bool bossFightStarted = false;
        private bool inWaitPhase = false;
        private bool inStageOne = false;
        private bool inStageTwo = false;
        private bool inStageThree = false;
        private float timer = 0;
        private float spawnTimer = 0;
        private VineBossSpawner spawner;
        private Slider bossSlider;
        private float totalTime;
        private float totalTimer = 0f;


        private void Update()
        {
            if (bossFightStarted) 
            { 
                timer -= Time.deltaTime; 
                totalTimer -= Time.deltaTime;
                bossSlider.value = Mathf.Lerp(1f, 0f, 1 - (totalTimer / totalTime));
            }
            
            if (timer < 0 && inWaitPhase)
            {
                timer = stageOneLength;
                inWaitPhase = false;
                inStageOne = true;
            }
            else if (timer < 0 && inStageOne)
            {
                timer = stageTwoLength;
                inStageOne = false;
                inStageTwo = true;
            }
            else if (timer < 0 && inStageTwo)
            {
                timer = stageThreeLength;
                inStageTwo = false;
                inStageThree = true;
            }
            else if (timer < 0 && inStageThree)
            {
                inStageThree = false;
                StartCoroutine(EndBoss());
            }

            if (inStageOne)
            {
                spawnTimer -= Time.deltaTime;
                if (spawnTimer < 0)
                {
                    StartWarning();
                    spawnTimer = Random.Range(stageOneSpawnInterval[0], stageOneSpawnInterval[1]);
                }
            }
            else if (inStageTwo)
            {
                spawnTimer -= Time.deltaTime;
                if (spawnTimer < 0)
                {
                    StartWarning();
                    spawnTimer = Random.Range(stageTwoSpawnInterval[0], stageTwoSpawnInterval[1]);
                }
            }
            else if (inStageThree)
            {
                spawnTimer -= Time.deltaTime;
                if (spawnTimer < 0)
                {
                    StartWarning();
                    spawnTimer = Random.Range(stageThreeSpawnInterval[0], stageThreeSpawnInterval[1]);
                }
            }

            if (bossFightStarted && !playerController.health.IsAlive)
            {
                timer = 0;
                bossFightStarted = false;
                inWaitPhase = false;
                inStageOne = false;
                inStageTwo = false;
                inStageThree = false;
                spawner.cinemachine.m_BoundingShape2D = spawner.originalConfiner;
                spawner.GetComponent<Collider2D>().enabled = true;
                bossSlider.gameObject.SetActive(false);
                spawner.collided = false;
                spawner.closingDoor.transform.position = spawner.initialPosition.position;
                Destroy(gameObject);
            }
        }

        private IEnumerator EndBoss()
        {
            yield return new WaitForSeconds(5f);
            spawner.cinemachine.m_BoundingShape2D = spawner.originalConfiner;
            bossSlider.gameObject.SetActive(false);
            Destroy(spawner.endDoor);
        }

        private void StartWarning()
        {
            float startX = xValues[Random.Range(0, 2)];
            float endX;
            if (startX == xValues[0])
            {
                endX = xValues[1];
            }
            else
            {
                endX = xValues[0];
            }

            GameObject dummyStartPoint = Instantiate(dummyPoint);
            GameObject dummyEndPoint = Instantiate(dummyPoint);
            dummyStartPoint.transform.position = new Vector3(startX, Random.Range(yRange[0], yRange[1]), 2);
            dummyEndPoint.transform.position = new Vector3(endX, Random.Range(yRange[0], yRange[1]), 2);
            Destroy(dummyStartPoint, 10f);
            Destroy(dummyEndPoint, 10f);

            VineSpawner uniqueVineSpawner = Instantiate(vineSpawner);
            vineSpawner.partOfBoss = true;
            Destroy(uniqueVineSpawner, 10f);
            uniqueVineSpawner.startPoint = dummyStartPoint.transform;
            uniqueVineSpawner.endPoint = dummyEndPoint.transform;
            uniqueVineSpawner.moveSpeed = moveSpeed;
            uniqueVineSpawner.spawnWarningTime = warningTime;
            uniqueVineSpawner.spawnTime = vineSpawnTime;
            uniqueVineSpawner.isTrigger = true;
            uniqueVineSpawner.vine = vine;
            uniqueVineSpawner.damageAmount = damage;
            uniqueVineSpawner.StartWarning();
            StartCoroutine(SpawnVine(uniqueVineSpawner));
        }

        private IEnumerator SpawnVine(VineSpawner uniqueVineSpawner)
        {
            yield return new WaitForSeconds(uniqueVineSpawner.spawnWarningTime);
            uniqueVineSpawner.SpawnVine();
        }

        private void Awake()
        {
            inWaitPhase = true;
            bossFightStarted = true;
            timer = initialWaitTime;
            spawner = FindObjectOfType<VineBossSpawner>();
            bossSlider = spawner.bossSlider;
            playerController = FindObjectOfType<PlayerController>();
            totalTime = stageOneLength + stageTwoLength + stageThreeLength + initialWaitTime;
            totalTimer = totalTime;
        }
    }
}
