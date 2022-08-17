using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Gameplay
{
    public class DisappearingPlatformController : MonoBehaviour
    {
        public DisappearingPlatform[] platforms;
        public bool startDisappearing = false;
        public float timeBeforeDisappearing = 1f;
        public float disappearTime = 3f;
        public float timeBeforeReappearing = 5f;
        public float reappearTime = 3f;
        private bool disappearing = false;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!disappearing && collision.gameObject.tag == "Player")
            {
                PlayerController player = collision.collider.GetComponent<PlayerController>();
                Collider2D collider = GetComponent<Collider2D>();
                if (player.Bounds.center.y >= collider.bounds.max.y)
                {
                    disappearing = true;
                    foreach (DisappearingPlatform platform in platforms)
                    {
                        platform.startDisappearing = true;
                        platform.timer = timeBeforeDisappearing;

                    }
                    StartCoroutine(RemoveCollider());
                }
            }
        }

        private IEnumerator RemoveCollider()
        {
            yield return new WaitForSeconds(timeBeforeDisappearing + disappearTime);
            GetComponent<Collider2D>().enabled = false;
            disappearing = false;
            StartCoroutine(ReinstateCollider());
        }

        private IEnumerator ReinstateCollider()
        {
            yield return new WaitForSeconds(timeBeforeReappearing + reappearTime);
            GetComponent<Collider2D>().enabled = true;
        }

        private void Awake()
        {
            foreach (DisappearingPlatform platform in platforms)
            {
                platform.timeBeforeDisappearing = timeBeforeDisappearing;
                platform.disappearTime = disappearTime;
                platform.timeBeforeReappearing = timeBeforeReappearing;
                platform.reappearTime = reappearTime;
            }
        }
    }
}
