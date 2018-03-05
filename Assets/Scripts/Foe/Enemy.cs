using System;
using Interfaces;
using UnityEngine;

namespace Foe
{
    [Serializable]
    public abstract class Enemy : MonoBehaviour, IEnemy, IKillable
    {
        #region properties

        [SerializeField] private AudioClip _deathAudioClip;
        [SerializeField] private bool _invulnerable;

        private AudioSource _audioSource;

        public bool EnableMovement;
        public float Speed;
        public Vector2 Range;

        public Rigidbody2D Rigidbody { get; set; }
        public Vector3 InitialPosition { get; set; }

        public bool IsDead { get; set; }
        protected int Direction { get; set; }

        public AudioClip DeathAudioClip
        {
            get { return _deathAudioClip; }
            set { _deathAudioClip = value; }
        }

        public bool Invulnerable
        {
            get { return _invulnerable; }
            set { _invulnerable = value; }
        }

        #endregion

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
            if (killable != null && !IsDead)
            {
                killable.Kill();
            }
        }

        public void Kill()
        {
            if (Invulnerable || IsDead)
            {
                return;
            }

            if (DeathAudioClip != null)
            {
                _audioSource.PlayOneShot(DeathAudioClip);
            }

            IsDead = true;
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