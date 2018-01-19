using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
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
            gameObject.transform.Rotate(0, 0, 90);
            Animator animator = gameObject.GetComponent<Animator>();
            animator.enabled = false;
            PlayerMove movement = gameObject.GetComponent<PlayerMove>();
            movement.enabled = false;

        }
    }
}