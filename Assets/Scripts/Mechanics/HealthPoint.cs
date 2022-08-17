using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics
{
    public class HealthPoint : MonoBehaviour
    {
        public int amount = 1;
        public ParticleSystem restoreHealthEffect;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                Health health = collision.gameObject.GetComponent<Health>();
                if (health != null)
                {
                    health.Increment(amount);
                    ParticleSystem restoreHealth = Instantiate(restoreHealthEffect, collision.transform);
                    Destroy(restoreHealth, 1.3f);
                }
                Destroy(gameObject);
            }
        }
    }
}
