using Assets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Environment
{
    public class WinBlock : Block
    {
        private new void OnCollisionEnter2D(Collision2D other)
        {
            Player.Player player = other.collider.GetComponent<Player.Player>();
            if (player != null)
            {
                print("Player wins, Game Over");
                ApplicationState.Ending = 1;
                SceneManager.LoadScene("GameOver");
            }
        }
    }
}