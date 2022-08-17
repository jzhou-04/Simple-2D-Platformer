using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Unique
{
    public class Crushy : MonoBehaviour
    {
        public Sprite[] sprites = new Sprite[2];
        public bool triggered = false;
        public bool returning = false;
        public Transform startPosition, endPosition;
        public float moveSpeed = 20f;
        public float returnSpeed = 5f;
        public float stayTime = 3f;

        // Update is called once per frame
        void Update()
        {
            if (triggered)
            {
                transform.position = Vector3.MoveTowards(transform.position, endPosition.position, moveSpeed * Time.deltaTime);
                if (transform.position == endPosition.position)
                {
                    triggered = false;
                    if (!GameController.Instance.playerIsDead)
                    {
                        CameraShake.Instance.ShakeCamera(5f, 2f, .6f);
                    }
                    StartCoroutine(ReturnToOriginalPosition());
                }
            }
            else if (returning)
            {
                transform.position = Vector3.MoveTowards(transform.position, startPosition.position, returnSpeed * Time.deltaTime);
                if (transform.position == startPosition.position)
                {
                    returning = false;
                }
            }
        }

        private IEnumerator ReturnToOriginalPosition()
        {
            yield return new WaitForSeconds(stayTime);
            GetComponent<SpriteRenderer>().sprite = sprites[0];
            returning = true;
        }
    }
}
