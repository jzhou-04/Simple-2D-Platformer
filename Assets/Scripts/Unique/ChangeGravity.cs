using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Unique
{
    public class ChangeGravity : MonoBehaviour
    {
        public float gravityScale = -9.8f;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                Physics2D.gravity = new Vector3(0, gravityScale, 0);
            }
        }
    }
}
