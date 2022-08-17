using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Gameplay
{

    public class EnemyDamage : MonoBehaviour
    {
        public int damageAmount = 34;
        public AudioClip ouch;
        public PatrolPath.Mover mover;
        public AnimationController control;
        public AudioSource _audio;
        public Collider2D _collider;
        public ParticleSystem deathEffect;
        SpriteRenderer spriteRenderer;

        public Bounds Bounds => _collider.bounds;

        void Awake()
        {
            control = GetComponent<AnimationController>();
            _collider = GetComponent<Collider2D>();
            _audio = GetComponent<AudioSource>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.tag == "Player")
            {
                PlayerController player = collision.collider.GetComponent<PlayerController>();
                EnemyDamage enemy = gameObject.GetComponent<EnemyDamage>();
                var willHurtEnemy = player.Bounds.center.y >= enemy.Bounds.max.y;

                if (willHurtEnemy)
                {
                    GameController.Instance.objectToBeDamaged = gameObject;
                    GameController.Instance.damageAmount = player.damage;         
                    Schedule<Damage>();
                    player.Bounce(2f);
                }
                else
                {
                    GameController.Instance.objectToBeDamaged = player.gameObject;
                    GameController.Instance.damageAmount = damageAmount;
                    GameController.Instance.bypassInvincibility = false;
                    Schedule<Damage>().triggerInvincibility = true;
                }
            }
        }
    }
}
