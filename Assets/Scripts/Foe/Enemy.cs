using System;
using Interfaces;
using UnityEngine;

namespace Foe
{
    [Serializable]
    public abstract class Enemy : MonoBehaviour, IEnemy, IVaporizable
    {
        #region properties

        [SerializeField] private AudioClip _deathAudioClip;
        [SerializeField] private AudioClip _vaporizeAudioClip;
        [SerializeField] private bool _invulnerable;
        [SerializeField] private bool _vaporizable;
        [SerializeField] private bool _friendlyFire;

        private AudioSource _audioSource;

        public bool EnableMovement;
        public float Speed;
        public Vector2 Range;

        public Rigidbody2D Rigidbody { get; set; }
        public Vector3 InitialPosition { get; set; }
        public Animator Animator { get; set; }
        public bool IsDead { get; set; }
        public float DiedAt { get; set; }

        protected int Direction { get; set; }

        public bool Vaporizable
        {
            get { return _vaporizable; }
            set { _vaporizable = value; }
        }
        
        public bool FriendlyFire
        {
            get { return _friendlyFire; }
            set { _friendlyFire = value; }
        }

        public AudioClip VaporizeAudioClip
        {
            get { return _vaporizeAudioClip; }
            set { _vaporizeAudioClip = value; }
        }

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
            Animator = GetComponent<Animator>();
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
                if (!FriendlyFire)
                {
                    return;  
                }
            }

            IKillable killable = otherCollider.GetComponent<IKillable>();
            if (killable != null && !IsDead)
            {
                killable.Kill();
            }
        }

        public void Kill()
        {
            if (IsDead && Vaporizable && IsVaporizable())
            {
                Invoke("Vaporize", 0.1f);
            }

            if (Invulnerable || IsDead)
            {
                return;
            }

            if (_audioSource != null && DeathAudioClip != null)
            {
                _audioSource.PlayOneShot(DeathAudioClip);
            }

            IsDead = true;
            PlayDead();
            Rigidbody.isKinematic = false;
            Rigidbody.mass = 100.0f;
            Rigidbody.bodyType = RigidbodyType2D.Dynamic;
            Rigidbody.gravityScale = 10.0f;
            EnableMovement = false;
            DiedAt = Time.timeSinceLevelLoad;
        }

        private bool IsVaporizable()
        {
            if (Equals(DiedAt, 0.0f))
            {
                return false;
            }

            return DiedAt + 1.0f <= Time.timeSinceLevelLoad;
        }

        public void Vaporize()
        {
            float delay = 0.33f;
            if (_audioSource != null && VaporizeAudioClip != null)
            {
                _audioSource.PlayOneShot(VaporizeAudioClip);
                delay = VaporizeAudioClip.length * 1.01f;
            }

            Destroy(gameObject, delay);
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