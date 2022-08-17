using Platformer.Gameplay;
using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Unique
{
    public class GhostMode : MonoBehaviour
    {
        public float time = 2f;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                EnvironmentDamage[] objects = FindObjectsOfType<EnvironmentDamage>();
                foreach (EnvironmentDamage damagingObject in objects)
                {
                    damagingObject.GetComponent<Collider2D>().enabled = false;
                }
                collision.GetComponent<SpriteRenderer>().color = new Color(46f/255f, 227f/255f, 253f/255f, .5f);
                StartCoroutine(ResetLayer(objects, collision));
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<Collider2D>().enabled = false;
            }
        }

        private IEnumerator ResetLayer(EnvironmentDamage[] objects, Collider2D collision)
        {
            yield return new WaitForSeconds(time);
            foreach (EnvironmentDamage damagingObject in objects)
            {
                if (damagingObject != null)
                    damagingObject.GetComponent<Collider2D>().enabled = true;
            }
            collision.GetComponent<SpriteRenderer>().color = new Color(46f / 255f, 227f / 255f, 253f / 255f, 1f);
        }
    }
}
