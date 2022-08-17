using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Unique
{
    public class BuffRespawner : MonoBehaviour
    {
        public static BuffRespawner Instance { get; private set; }

        public GameObject[] buffs;
        private Vector3[] positions;
        //private List<Vector3> positions;

        public void RespawnBuffs()
        {
            int index = 0;
            foreach (GameObject buff in buffs)
            {
                buff.GetComponent<SpriteRenderer>().enabled = true;
                buff.GetComponent<Collider2D>().enabled = true;
                buff.transform.position = positions[index];
                index++;
            }
        }

        private void Awake()
        {
            Instance = this;
            positions = new Vector3[buffs.Length];
            int index = 0;
            foreach (GameObject buff in buffs)
            {
                positions[index] = buff.transform.position;
                index++;
            }
        }
    }
}