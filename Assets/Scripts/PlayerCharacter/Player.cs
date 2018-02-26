using System;
using Interfaces;
using UnityEngine;

namespace PlayerCharacter
{
    public class Player : MonoBehaviour, IPlayer, IKillable
    {
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Experience { get; set; }
        public bool Invulnerable { get; set; }
        public bool Dead { get; set; }

        [SerializeField] private AudioClip _deathAudioClip;
        private AudioSource _audioSource;

        public AudioClip DeathAudioClip
        {
            get { return _deathAudioClip; }
            set { _deathAudioClip = value; }
        }

        public void Start()
        {
            MaxHealth = 100;
            Health = MaxHealth;
            _audioSource = GetComponent<AudioSource>();
        }

        public void Update()
        {
            if (GlobalGameState.IsPaused)
            {
                return;
            }
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
            movement.MovementEnabled = false;

            Animator animator = GetComponent<Animator>();
            animator.enabled = false;
            double deathAnimationLength = 1.00d;

            if (DeathAudioClip != null)
            {
                deathAnimationLength = DeathAudioClip.length;
                _audioSource.PlayOneShot(DeathAudioClip);
            }

            int z = 90;

            if (movement.IsFacingRight())
            {
                z *= -1;
            }

            gameObject.transform.Rotate(0, 0, z);
            Invoke("TrueDeath", Convert.ToSingle(deathAnimationLength * 1.15));
        }

        // ReSharper disable once MemberCanBeMadeStatic.Local
        // @bhaertlein - Methods provided to UnityEngine.Invoke() can not be `static`
        private void TrueDeath()
        {
            GlobalGameState.PlayerDeath();
        }
    }
}