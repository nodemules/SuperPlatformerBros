using Environment;
using Interfaces;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace Enemy
{
    public abstract class Enemy : MonoBehaviour, IEnemy
    {
        public Rigidbody2D Rigidbody { get; set; }
        public Vector3 InitialPosition { get; set; }

        public bool EnableMovement;
        public float Speed;
        public Vector2 Range;

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
            if (killable != null)
            {
                killable.Kill();
            }
        }
    }
}