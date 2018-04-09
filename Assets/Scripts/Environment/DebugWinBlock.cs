using System.Diagnostics.CodeAnalysis;
using Level;
using PlayerCharacter;
using UnityEngine;

namespace Environment
{
    public class DebugWinBlock : Block
    {
        public int LevelNumber;

        [SuppressMessage("ReSharper", "RedundantJumpStatement")]
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
            Player player = other.collider.GetComponent<Player>();
            if (player != null && !player.IsDead)
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