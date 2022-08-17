using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics
{
    public class ManualMovingPlatform : MonoBehaviour
    {
        public float moveTime = 5f;
        public float stopTime = 2f;
        public bool isSticky = true;
        public Transform startPosition, endPosition;

        private float timer = 0f;
        private bool atStart = true;
        private bool atStop = false;
        private bool inMotion = false;
        private Collision2D collision;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (atStart && collision.gameObject.tag == "Player")
            {
                PlayerController player = collision.collider.GetComponent<PlayerController>();
                Collider2D collider = GetComponent<Collider2D>();
                if (player.Bounds.center.y >= collider.bounds.max.y)
                {
                    this.collision = collision;
                    this.collision.gameObject.transform.SetParent(transform);
                    timer = moveTime;
                    atStart = false;
                    inMotion = true;
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (inMotion && timer >= 0f)
            {
                timer -= Time.deltaTime;
                float xPos = Mathf.Lerp(startPosition.transform.position.x, endPosition.transform.position.x, 1 - (timer / moveTime));
                float yPos = Mathf.Lerp(startPosition.transform.position.y, endPosition.transform.position.y, 1 - (timer / moveTime));
                float zPos = Mathf.Lerp(startPosition.transform.position.z, endPosition.transform.position.z, 1 - (timer / moveTime));
                transform.position = new Vector3(xPos, yPos, zPos);
            }
            else if (inMotion && timer < 0f)
            {
                timer = stopTime;
                atStop = true;
                inMotion = false;
                collision.gameObject.transform.SetParent(null);
            }

            if (atStop && timer >= 0f)
            {
                timer -= Time.deltaTime;
            }
            else if (atStop && timer < 0f)
            {
                collision.gameObject.transform.SetParent(null);
                gameObject.SetActive(false);
                transform.position = startPosition.position;
                gameObject.SetActive(true);
                atStop = false;
                atStart = true;
            }
        }
    }
}
