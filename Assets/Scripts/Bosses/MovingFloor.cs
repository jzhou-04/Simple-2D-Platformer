using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Bosses
{
    public class MovingFloor : MonoBehaviour
    {
        public float moveSpeed = 2f;
        public float stopTime = 2f;
        public float[] yInterval = new float[2];

        private float timer = 0;
        private float destinationY;

        // Update is called once per frame
        void Update()
        {
            if (transform.position.y != destinationY)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, destinationY, transform.position.z), moveSpeed * Time.deltaTime);
            }
            else if (timer >= 0)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    float newY = Random.Range(yInterval[0], yInterval[1]); ;
                    while (Mathf.Abs(destinationY - newY) <= 1)
                    {
                        newY = Random.Range(yInterval[0], yInterval[1]);
                    }
                    destinationY = newY;
                    timer = stopTime;
                }

            }
        }

        private void Start()
        {
            destinationY = Random.Range(yInterval[0], yInterval[1]);
            timer = stopTime;
        }
    }
}
