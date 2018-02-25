using Level;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Environment
{
    public class WinBlock : Block
    {
        public int LevelNumber;
        
        private new void OnCollisionEnter2D(Collision2D other)
        {
            Player.Player player = other.collider.GetComponent<Player.Player>();
            if (player != null)
            {
                if (LevelNumber > 0)
                {
                    LevelLoader.ChangeLevel("Level" + LevelNumber);
                    return;
                }
                LevelLoader.NextLevel();
            }
        }
    }
}