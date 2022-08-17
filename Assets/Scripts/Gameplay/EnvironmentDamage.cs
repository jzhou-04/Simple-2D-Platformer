using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Gameplay
{
    public class EnvironmentDamage : MonoBehaviour
    {
        public int damageAmount = 34;
        public bool triggerInvincibility = true;

        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.tag == "Player")
            {
                GameController.Instance.objectToBeDamaged = collision.collider.gameObject;
                GameController.Instance.damageAmount = damageAmount;
                GameController.Instance.bypassInvincibility = false;
                Schedule<Damage>().triggerInvincibility = true;
            }
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                GameController.Instance.objectToBeDamaged = collision.gameObject;
                GameController.Instance.damageAmount = damageAmount;
                GameController.Instance.bypassInvincibility = false;
                Schedule<Damage>().triggerInvincibility = triggerInvincibility;
            }
        }
    }
}
