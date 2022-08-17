using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics
{
    public class AutomaticMovingPlatform : MonoBehaviour
    {
        public float moveTime = 5f;
        public float stopTime = 2f;
        public Transform startPosition, endPosition;

        private float timer = 0f;
        private bool atStart = true;
        private bool atStop = false;
        private bool inMotion = false;
        private Collision2D collision;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                PlayerController player = collision.collider.GetComponent<PlayerController>();
                Collider2D collider = GetComponent<Collider2D>();
                if (player.Bounds.center.y >= collider.bounds.max.y)
                {
                    this.collision = collision;
                    this.collision.gameObject.transform.SetParent(transform);
                }
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                this.collision = null;
            }
        }

        // Update is called once per frame
        void Update()
        {
            timer -= Time.deltaTime;

            if (timer < 0f && !inMotion)
            {
                inMotion = true;
                timer = moveTime;
                if (collision != null)
                {
                    collision.gameObject.transform.SetParent(transform);
                }
            }

            if (inMotion)
            {
                if (atStart)
                {
                    float xPos = Mathf.Lerp(startPosition.transform.position.x, endPosition.transform.position.x, 1 - (timer / moveTime));
                    float yPos = Mathf.Lerp(startPosition.transform.position.y, endPosition.transform.position.y, 1 - (timer / moveTime));
                    float zPos = Mathf.Lerp(startPosition.transform.position.z, endPosition.transform.position.z, 1 - (timer / moveTime));
                    transform.position = new Vector3(xPos, yPos, zPos);
                }
                else if (atStop)
                {
                    float xPos = Mathf.Lerp(endPosition.transform.position.x, startPosition.transform.position.x, 1 - (timer / moveTime));
                    float yPos = Mathf.Lerp(endPosition.transform.position.y, startPosition.transform.position.y, 1 - (timer / moveTime));
                    float zPos = Mathf.Lerp(endPosition.transform.position.z, startPosition.transform.position.z, 1 - (timer / moveTime));
                    transform.position = new Vector3(xPos, yPos, zPos);
                }
            }

            if (gameObject.transform.position == startPosition.position && !atStart)
            {
                atStart = true;
                atStop = false;
                inMotion = false;
                timer = stopTime;
                if (collision != null) 
                { 
                    collision.gameObject.transform.SetParent(null);
                }
            }
            else if (gameObject.transform.position == endPosition.position && !atStop)
            {
                atStop = true;
                atStart = false;
                inMotion = false;
                timer = stopTime;
                if (collision != null)
                {
                    collision.gameObject.transform.SetParent(null);
                }
            }
        }

    }
}
