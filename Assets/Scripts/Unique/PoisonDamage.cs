using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Platformer.Core.Simulation;
using Platformer.Mechanics;

namespace Platformer.Unique
{
    public class PoisonDamage : MonoBehaviour
    {
        public static PoisonDamage Instance { get; private set; }

        public int poisonDamageAmount;
        public int totalPoisonDamageTicks;
        public float poisonDamageInterval;
        public ParticleSystem poisonEffect;
        public PlayerController playerController;

        private int poisonTicks = 0;
        private bool isPoisoned = false;
        private ParticleSystem uniquePoisonEffect;

        private IEnumerator DealPoisonDamage()
        {
            yield return new WaitForSeconds(poisonDamageInterval);
            while (poisonTicks < totalPoisonDamageTicks && isPoisoned)
            {

                GameController.Instance.objectToBeDamaged = GameController.Instance.model.player.gameObject;
                GameController.Instance.damageAmount = poisonDamageAmount;
                GameController.Instance.bypassInvincibility = true;
                Schedule<Damage>().triggerInvincibility = false;
                poisonTicks++;
                yield return new WaitForSeconds(poisonDamageInterval);
            }
            poisonTicks = 0;
        }

        public void PoisonPlayer()
        {
            if (!isPoisoned)
            { 
                isPoisoned = true;
                if (uniquePoisonEffect == null)
                {
                    uniquePoisonEffect = Instantiate(poisonEffect, playerController.transform);
                    Destroy(uniquePoisonEffect, totalPoisonDamageTicks * poisonDamageInterval + 1);
                }
                IEnumerator coroutine = DealPoisonDamage();
                StartCoroutine(coroutine);
                }
            else
            {
                StopAllCoroutines();
                Destroy(uniquePoisonEffect);
                uniquePoisonEffect = Instantiate(poisonEffect, playerController.transform);
                Destroy(uniquePoisonEffect, totalPoisonDamageTicks * poisonDamageInterval + 1);
                poisonTicks = 0;
                IEnumerator coroutine = DealPoisonDamage();
                StartCoroutine(coroutine);
            }
        }

        public void StopPoison()
        {
            isPoisoned = false;
            if (uniquePoisonEffect != null)
            {
                Destroy(uniquePoisonEffect);
            }
        }

        public void Awake()
        {
            Instance = this;
        }
    }
}
