using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Gameplay
{
    public class DisappearingPlatform : MonoBehaviour
    {
        public bool startDisappearing = false;
        public float timeBeforeDisappearing;
        public float disappearTime;
        public float timeBeforeReappearing;
        public float reappearTime;
        public float timer = 0f;

        private bool disappearing = false;     
        private bool startingToReappear = false;
        private bool reappearing = false;
        private SpriteRenderer spriteRenderer;
        private Collider2D _collider;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            startDisappearing = true;
            timer = timeBeforeDisappearing;
        }

        void Update()
        {
            if (startDisappearing)
            {
                timer -= Time.deltaTime;
                if (timer <= 0f) 
                { 
                    startDisappearing = false; 
                    disappearing = true;
                    timer = disappearTime;
                }
            }
            else if (disappearing)
            {
                timer -= Time.deltaTime;
                spriteRenderer.color = new Color(1f, 1f, 1f, Mathf.Lerp(1f, 0f, 1 - (timer / disappearTime)));
                if (timer <= 0f)
                {
                    disappearing = false;
                    startingToReappear = true;
                    timer = timeBeforeReappearing;
                }
            }
            else if (startingToReappear)
            {
                timer -= Time.deltaTime;
                if (timer <= 0f)
                {
                    startingToReappear = false;
                    reappearing = true;
                    timer = reappearTime;
                }
            }
            else if (reappearing)
            {
                timer -= Time.deltaTime;
                spriteRenderer.color = new Color(1f, 1f, 1f, Mathf.Lerp(0f, 1f, 1 - (timer / reappearTime)));
                if (timer <= 0f)
                {
                    reappearing = false;
                }
            }
        }

        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }
}
