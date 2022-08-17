using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Gameplay
{
    public class EnemyRespawner : MonoBehaviour
    {
        public static EnemyRespawner Instance { get; private set; }

        public GameObject[] enemies;
        private Vector3[] positions;
        //private List<Vector3> positions;

        public void RespawnEnemies()
        {
            int index = 0;
            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<SpriteRenderer>().enabled = true;
                enemy.GetComponent<Collider2D>().enabled = true;
                enemy.transform.position = positions[index];
                enemy.SetActive(true);
                index++;
            }
        }

        private void Awake()
        {
            Instance = this;
            positions = new Vector3[enemies.Length];
            int index = 0;
            foreach (GameObject enemy in enemies)
            {
                positions[index] = enemy.transform.position;
                index++;
            }
        }
    }
}