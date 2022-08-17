using System;
using System.Collections;
using Platformer.Gameplay;
using UnityEngine;
using UnityEngine.UI;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// Represebts the current vital statistics of some game entity.
    /// </summary>
    public class Health : MonoBehaviour
    {
        /// <summary>
        /// The maximum hit points for the entity.
        /// </summary>
        public int maxHP = 1;
        public int currentHP;
        public float hitlagTime = .125f;
        public float hitlagSlowTo = .25f;
        public float healthBarUpdateTime = .75f;
        public Slider healthBar;

        private bool isPlayer;
        private float healthBarTimer = 0f;
        private float previousHealth;

        /// <summary>
        /// Indicates if the entity should be considered 'alive'.
        /// </summary>
        public bool IsAlive => currentHP > 0;    

        /// <summary>
        /// Increment the HP of the entity.
        /// </summary>
        public void Increment(int amount)
        {
            currentHP = Mathf.Clamp(currentHP + amount, 0, maxHP);
            UpdateHealth();
        }

        /// <summary>
        /// Decrement the HP of the entity. Will trigger a HealthIsZero event when
        /// current HP reaches 0.
        /// </summary>
        public void Decrement(int amount, bool triggerInvincibility)
        {
            if (isPlayer)
            {
                currentHP = Mathf.Clamp(currentHP - amount, 0, maxHP);
                if (currentHP == 0)
                {
                    Schedule<PlayerDeath>();
                }
                ScheduleCameraShake(amount);
                TriggerHitlag();
                if (triggerInvincibility)
                {
                    Schedule<StartInvincibility>();
                }
                UpdateHealth();
            }
            else if (!isPlayer)
            {
                currentHP = Mathf.Clamp(currentHP - amount, 0, maxHP);
                if (currentHP == 0)
                {
                    ParticleSystem deathEffect = Instantiate(gameObject.GetComponent<EnemyDamage>().deathEffect, gameObject.transform);
                    Destroy(deathEffect.gameObject, 1f);
                    GetComponent<SpriteRenderer>().enabled = false;
                    GetComponent<EnemyDamage>()._collider.enabled = false;
                    StartCoroutine(DisableObject(2f));
                }
            }
        }

        /// <summary>
        /// Decrement the HP of the entitiy until HP reaches 0.
        /// </summary>
        public void KillPlayer()
        {
            currentHP = 0;
            Schedule<PlayerDeath>();
            UpdateHealth();
        }

        private void UpdateHealth()
        {
            healthBarTimer = healthBarUpdateTime;
            previousHealth = healthBar.value;
        }

        public void Update()
        {
            if (healthBarTimer >= 0 && isPlayer)
            {
                healthBarTimer -= Time.deltaTime;
                healthBar.value = Mathf.Lerp(previousHealth, (float)currentHP / maxHP, 1 - (healthBarTimer / healthBarUpdateTime));
            }
        }

        private void ScheduleCameraShake(int amount)
        {
            float modifier = amount / (float) maxHP;
            float intensityModifier = modifier * 1.75f;
            float timeModifier = modifier * .5f;

            float intensity = Mathf.Clamp(2.5f * intensityModifier, .85f, 4f);
            float frequency = Mathf.Clamp(1.5f * modifier, .51f, 3f);
            float time = Mathf.Clamp(.9f * timeModifier, .306f, 1.8f);
            CameraShake.Instance.ShakeCamera(intensity, frequency, time);
        }

        private void TriggerHitlag()
        {
            PlayerController player = gameObject.GetComponent<PlayerController>();
            player.invincibilityStartWaitTime = hitlagTime / 3.0f;
            Hitlag.Instance.TriggerHitLag(hitlagSlowTo, hitlagTime);
        }

        private IEnumerator DisableObject(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            gameObject.SetActive(false);
        }

        void Awake()
        {
            currentHP = maxHP;
            if (gameObject.tag == "Player")
            {
                isPlayer = true;
                UpdateHealth();
            }
        }
    }
}
