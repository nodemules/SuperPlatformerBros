using Level;
using PlayerCharacter;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Environment
{
    public class WinBlock : Block
    {
        private new void OnCollisionEnter2D(Collision2D other)
        {
            Player player = other.collider.GetComponent<Player>();
            if (player != null)
            {
                LevelLoader.NextLevel();
            }
        }
    }
}