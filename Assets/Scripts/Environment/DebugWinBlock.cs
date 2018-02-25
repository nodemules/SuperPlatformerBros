using Level;
using UnityEngine;

namespace Environment
{
    public class DebugWinBlock : WinBlock
    {
        public int LevelNumber;

        public void Start()
        {
            if (!Debug.isDebugBuild)
            {
                Destroy(gameObject);
                return;
            }
        }

        public new void OnCollisionEnter2D(Collision2D other)
        {
            Player.Player player = other.collider.GetComponent<Player.Player>();
            if (player != null)
            {
                if (LevelNumber > 0)
                {
                    LevelLoader.ChangeLevel("Level" + LevelNumber);
                }
                else
                {
                    LevelLoader.NextLevel();
                }
            }
        }
    }
}