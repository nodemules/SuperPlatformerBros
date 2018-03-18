﻿using Level;
using PlayerCharacter;
using UnityEngine;

namespace Environment
{
    public class WarpBlock : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            Player player = other.collider.GetComponent<Player>();
            if (player != null)
            {
                LevelLoader.GoToWarpZone();
            }
        }
        
    }
}