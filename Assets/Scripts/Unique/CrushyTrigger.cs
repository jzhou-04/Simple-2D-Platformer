using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Unique
{
    public class CrushyTrigger : MonoBehaviour
    {
        public Crushy crushy;
        public float delayTime = 1f;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player" && !crushy.triggered && !crushy.returning)
            {
                StartCoroutine(TriggerCrushy());
            }
        }

        private IEnumerator TriggerCrushy()
        {
            yield return new WaitForSeconds(delayTime);
            crushy.GetComponent<SpriteRenderer>().sprite = crushy.sprites[1];
            crushy.triggered = true;
        }
    }
}
