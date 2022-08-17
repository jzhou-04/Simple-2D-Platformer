using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Gameplay
{
    public class JumpPad : MonoBehaviour
    {
        public float bounceAmount = 10f;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.Bounce(bounceAmount);
            }
        }
    }
}
