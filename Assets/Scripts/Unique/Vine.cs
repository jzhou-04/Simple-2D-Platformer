using Platformer.Gameplay;
using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Unique
{
    public class Vine : MonoBehaviour
    {
        public float moveSpeed;
        public int damageAmount;
        public bool canMove = false;
        public Transform startPosition, endPosition;
        public bool triggerInvicibiity;
        public float aliveTime = 4f;

        private bool reachedEndpoint = false;
        private Renderer _renderer;
        private float timer;
        private float tempTime;
        private SpriteRenderer _spriteRenderer;

        public void Setup()
        {
            GetComponent<EnvironmentDamage>().triggerInvincibility = triggerInvicibiity;
            GetComponent<EnvironmentDamage>().damageAmount = damageAmount;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            float zRotation = -Mathf.Atan((endPosition.position.x - startPosition.position.x) / (endPosition.position.y - startPosition.position.y));
            zRotation *= Mathf.Rad2Deg;
            if (startPosition.position.y > endPosition.position.y) { zRotation = 180 + zRotation; }
            var rotationVector = transform.rotation.eulerAngles;
            rotationVector.z = zRotation;
            transform.rotation = Quaternion.Euler(rotationVector);
            canMove = true;
            timer = aliveTime;
            gameObject.SetActive(true);
        }

        void Update()
        {
            if (canMove && transform != null)
            {
                timer -= Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, endPosition.position, moveSpeed * Time.deltaTime);
                if (_renderer.isVisible && transform.position == endPosition.position && !reachedEndpoint)
                {
                    CameraShake.Instance.ShakeCamera(2f, 1f, .7f);
                    tempTime = timer;
                    reachedEndpoint = true;
                }
            }

            if (reachedEndpoint)
            {
                _spriteRenderer.color = new Color(1f, 1f, 1f, Mathf.Lerp(1f, 0f, 1 - (timer / (aliveTime - tempTime))));
                if (timer <= 0f)
                {
                    Destroy(gameObject);
                }
            }    
        }

        private void Awake()
        {
            gameObject.SetActive(false);
            _renderer = GetComponent<Renderer>();
        }
    }
}
