using UnityEngine;

namespace Platformer.Mechanics
{
    /// <summary>
    /// Marks a gameobject as a spawnpoint in a scene.
    /// </summary>
    
    [RequireComponent(typeof(Collider2D))]
    public class SpawnPoint : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                GameController.Instance.model.spawnPoint = gameObject.transform;
                gameObject.GetComponent<Collider2D>().enabled = false;
            }
        }
    }
}