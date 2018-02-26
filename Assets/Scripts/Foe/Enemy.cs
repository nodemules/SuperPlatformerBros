﻿using System;
using Interfaces;
using UnityEngine;

namespace Foe
{    
    [Serializable]
    public abstract class Enemy : MonoBehaviour, IEnemy, IKillable
    {
        public Rigidbody2D Rigidbody { get; set; }
        public Vector3 InitialPosition { get; set; }

        public AudioClip DeathAudioClip
        {
            get { return _deathAudioClip; }
            set { _deathAudioClip = value; }
        }

        private AudioSource _audioSource;

        public bool Invulnerable { get; set; }
        public bool Dead { get; set; }

        public bool EnableMovement;
        public float Speed;
        public Vector2 Range;
        [SerializeField] 
        private AudioClip _deathAudioClip;

        protected int Direction { get; set; }

        public void Start()
        {
            DoInitialization();
        }

        protected void DoInitialization()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            InitialPosition = transform.position;
            Direction = -1;
            _audioSource = GetComponent<AudioSource>();
        }

        public void Update()
        {
            if (EnableMovement)
            {
                Move();
            }
            else
            {
                StopMoving();
            }
        }

        protected abstract void Move();
        protected abstract void StopMoving();
        protected abstract void TurnAround();

        protected void OnCollisionEnter2D(Collision2D other)
        {
            Collider2D otherCollider = other.collider;

            IEnemy enemy = otherCollider.GetComponent<IEnemy>();
            if (enemy != null)
            {
                TurnAround();
                return;
            }

            IKillable killable = otherCollider.GetComponent<IKillable>();
            if (killable != null && !Dead)
            {
                killable.Kill();
            }
        }

        public void Kill()
        {
            if (Invulnerable || Dead)
            {
                return;
            }

            if (DeathAudioClip != null)
            {
                _audioSource.PlayOneShot(DeathAudioClip);    
            }
            
            Dead = true;
            PlayDead();
            Rigidbody.isKinematic = false;
            Rigidbody.bodyType = RigidbodyType2D.Dynamic;
            Rigidbody.gravityScale = 10;
            EnableMovement = false;
        }

        private void PlayDead()
        {
            Vector2 localScale = gameObject.transform.localScale;
            if (localScale.y > 0)
            {
                localScale.y *= -1;
                transform.localScale = localScale;
            }
        }
    }
}