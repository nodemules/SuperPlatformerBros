using Assets.Scripts;
using Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    public class Player : MonoBehaviour, IPlayer, IKillable
    {
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Experience { get; set; }
        public bool Invulnerable { get; set; }
        public bool Dead { get; set; }

        public void Start()
        {
            MaxHealth = 100;
            Health = MaxHealth;
        }

        public void Update()
        {
            if (Health <= 0)
            {
                Kill();
            }
        }

        public void Kill()
        {
            if (!Dead)
            {
                Die();
            }
        }

        private void Die()
        {
            Dead = true;
            Health = 0;

            PlayerMovement movement = gameObject.GetComponent<PlayerMovement>();

            int z = 90;

            if (!movement.IsFacingRight())
            {
                z *= -1;
            }

            gameObject.transform.Rotate(0, 0, z);

            // Stop the game
            ApplicationState.Ending = -1;
            print("Player has died, Game over");
            SceneManager.LoadScene("GameOver");
        }
    }
}