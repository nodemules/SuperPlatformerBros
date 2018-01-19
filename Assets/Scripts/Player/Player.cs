using Assets.Scripts;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour, IKillable
    {
        private int _playerHealth;
        private bool _dead;
        public int MaxHealth = 100;


        public void Start()
        {
            _playerHealth = MaxHealth;
        }

        public void Update()
        {
            if (_playerHealth <= 0)
            {
                Kill();
            }
        }

        public void Kill()
        {
            if (!_dead)
            {
                Die();
            }
        }

        private void Die()
        {
            _dead = true;
            _playerHealth = 0;
            
            PlayerMove movement = gameObject.GetComponent<PlayerMove>();
            Animator animator = gameObject.GetComponent<Animator>();

            int z = 90;

            if (!movement.IsFacingRight())
            {
                z *= -1;
            }
            
            gameObject.transform.Rotate(0, 0, z);
            
            movement.enabled = false;
            animator.enabled = false;
        }
    }
}