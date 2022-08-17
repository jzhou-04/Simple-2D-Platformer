using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Unique;

public class PoisonedObject : MonoBehaviour
{
    public int poisonDamageAmount = 10;
    public int totalPoisonDamageTicks = 2;
    public float poisonDamageInterval = 3f;


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player" && PoisonDamage.Instance != null)
        {
            PoisonDamage.Instance.poisonDamageAmount = poisonDamageAmount;
            PoisonDamage.Instance.totalPoisonDamageTicks = totalPoisonDamageTicks;
            PoisonDamage.Instance.poisonDamageInterval = poisonDamageInterval;
            PoisonDamage.Instance.PoisonPlayer();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && PoisonDamage.Instance != null)
        {
            PoisonDamage.Instance.poisonDamageAmount = poisonDamageAmount;
            PoisonDamage.Instance.totalPoisonDamageTicks = totalPoisonDamageTicks;
            PoisonDamage.Instance.poisonDamageInterval = poisonDamageInterval;
            PoisonDamage.Instance.PoisonPlayer();
        }
    }
}
