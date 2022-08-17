using Platformer.Gameplay;
using UnityEngine;

namespace Platformer.Unique
{
    public class VineSpawner : MonoBehaviour
    {
        public float initialWaitTime = 0f;
        public float spawnIntervalTime = 2f;
        public float spawnWarningTime = 2f;
        public float spawnTime = 4f;
        public float moveSpeed = 30f;
        public int damageAmount = 34;
        public bool partOfBoss = false;
        public bool triggerInvincibility = true;
        public bool isTrigger = false;
        public bool isPoisoned = false;
        public int poisonDamage = 10;
        public int poisonTicks = 2;
        public int poisonInterval = 3;
        public Transform startPoint;
        public Transform endPoint;
        public ParticleSystem warningParticles;
        public Vine vine;

        private bool inInitialWait = false;
        private bool atRest = false;
        private bool inWarning = false;
        private bool spawned = false;
        private float timer = 0f;

        // Start is called before the first frame update
        void Start()
        {
            if (partOfBoss)
            {
                timer = 0f;
            }
            else if (initialWaitTime > 0f)
            {
                inInitialWait = true;
                timer = initialWaitTime;
            }
            else
            {
                atRest = true;
                timer = spawnIntervalTime;
            }
        }

        // Update is called once per frame
        void Update()
        {
            timer -= Time.deltaTime;
            if (inInitialWait && timer < 0f)
            {
                timer = spawnIntervalTime;
                inInitialWait = false;
                atRest = true;
            }
            else if (atRest && timer < 0f)
            {
                timer = spawnWarningTime;
                StartWarning();
                atRest = false;
                inWarning = true;
            }
            else if (inWarning && timer < 0f)
            {
                timer = spawnTime;
                SpawnVine();
                inWarning = false;
                spawned = true;
            }
            else if (spawned && timer < 0f)
            {
                timer = spawnIntervalTime;
                spawned = false;
                atRest = true;
            }
        }

        public void StartWarning()
        {
            ParticleSystem particleSystemOne = Instantiate(warningParticles, startPoint);
            ParticleSystem particleSystemTwo = Instantiate(warningParticles, endPoint);
            Destroy(particleSystemOne.gameObject, spawnWarningTime);
            Destroy(particleSystemTwo.gameObject, spawnWarningTime);
        }

        public void SpawnVine()
        {
            Vine spawnedVine = Instantiate(vine, startPoint);
            if (partOfBoss) { spawnedVine.GetComponent<Collider2D>().isTrigger = true; }
            spawnedVine.GetComponent<Collider2D>().isTrigger = isTrigger;
            spawnedVine.transform.SetParent(null);
            spawnedVine.damageAmount = damageAmount;
            spawnedVine.startPosition = startPoint;
            spawnedVine.endPosition = endPoint;
            spawnedVine.moveSpeed = moveSpeed;
            spawnedVine.triggerInvicibiity = triggerInvincibility;
            spawnedVine.aliveTime = spawnTime;
            if (isPoisoned && spawnedVine.GetComponent<PoisonedObject>() != null)
            {
                PoisonedObject poisonedObject = spawnedVine.GetComponent<PoisonedObject>();
                poisonedObject.poisonDamageAmount = poisonDamage;
                poisonedObject.poisonDamageInterval = poisonInterval;
                poisonedObject.totalPoisonDamageTicks = poisonTicks;
            }
            spawnedVine.Setup();
            //Destroy(spawnedVine.gameObject, spawnTime);
        }
    }
}
