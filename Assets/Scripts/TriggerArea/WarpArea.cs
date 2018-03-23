using Interfaces;
using PlayerCharacter;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TriggerArea
{
    public class WarpArea : MonoBehaviour, IBoundary, ITriggerable
    {
        public bool IsObstacle
        {
            get { return false; }
        }

        public void Trigger()
        {
            Scene scene = SceneManager.GetActiveScene();
            GameObject levelContainer = GameObject.Find(scene.name + "Container");
            Player player = levelContainer.GetComponentInChildren<Player>();
            if (player != null)
            {
                BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();
                player.transform.position = boxCollider2D.bounds.center;
                Rigidbody2D playerRigidbody2D = player.GetComponent<Rigidbody2D>();
                if (playerRigidbody2D != null)
                {
                    playerRigidbody2D.velocity = new Vector2();
                }
            }
        }
    }
}