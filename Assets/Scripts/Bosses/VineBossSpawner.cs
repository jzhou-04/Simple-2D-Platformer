using Cinemachine;
using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer.Bosses
{
    public class VineBossSpawner : MonoBehaviour
    {
        public GameObject vineBoss;
        public Collider2D originalConfiner;
        public Collider2D newConfiner;
        public CinemachineConfiner cinemachine;
        public GameObject closingDoor;
        public GameObject endDoor;
        public Transform initialPosition;
        public Transform endPosition;
        public Slider bossSlider;
        public bool collided = false;
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                collided = true;
                bossSlider.enabled = true;
                GetComponent<Collider2D>().enabled = false;
                bossSlider.gameObject.SetActive(true);
                cinemachine.m_BoundingShape2D = newConfiner;
                Instantiate(vineBoss);
            }
        }

        void Update()
        {
            if (collided && closingDoor.transform.position != endPosition.transform.position)
            {
                closingDoor.transform.position = Vector3.MoveTowards(closingDoor.transform.position, endPosition.transform.position, 5 * Time.deltaTime);
            }
        }
    }
}
