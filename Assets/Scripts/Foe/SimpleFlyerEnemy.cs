using System.Diagnostics.CodeAnalysis;
using Environment;
using Interfaces;
using UnityEngine;

namespace Foe
{
    public class SimpleFlyerEnemy : Enemy
    {
        private float _lowerBound;
        private float _upperBound;
        protected bool IsFlying;

        public new void Start()
        {
            DoInitialization();
        }

        protected new void DoInitialization()
        {
            base.DoInitialization();
            Rigidbody.gravityScale = 0.0f;
            _lowerBound = InitialPosition.y - Range.x;
            _upperBound = InitialPosition.y + Range.y;
        }

        protected override void Move()
        {
            Fly();
        }

        protected override void StopMoving()
        {
            StopFlying();
        }

        protected void StopFlying()
        {
            IsFlying = false;
            Rigidbody.velocity = new Vector2(0, 0);
        }

        protected void Fly()
        {
            if (!EnableMovement)
            {
                return;
            }

            if (!IsFlying)
            {
                IsFlying = true;
            }
            CheckDirection();
            Rigidbody.velocity = new Vector2(0, Speed * Direction);
        }

        protected void CheckDirection()
        {
            switch (Direction)
            {
                case -1:
                    // Moving Down
                    if (transform.position.y <= _lowerBound)
                    {
                        TurnAround();
                    }

                    break;
                case 1:
                    //Moving Up
                    if (transform.position.y >= _upperBound)
                    {
                        TurnAround();
                    }

                    break;
                default:
                    Direction = 1;
                    break;
            }
            
        }

        protected override void TurnAround()
        {
            Direction *= -1;
        }

        // @bhaertlein - allow the redundant return to ensure control flow is respected when 
        //   this functionality is extended
        [SuppressMessage("ReSharper", "RedundantJumpStatement")]
        public new void OnCollisionEnter2D(Collision2D other)
        {
            base.OnCollisionEnter2D(other);
            Collider2D otherCollider = other.collider;
            IPlatform platform = otherCollider.GetComponent<IPlatform>();
            if (platform != null)
            {
                TurnAround();
                return;
            }

            Wall wall = otherCollider.GetComponent<IBoundary>() as Wall;
            if (wall != null && wall.IsObstacle)
            {
                TurnAround();
                return;
            }

            IBlock block = otherCollider.GetComponent<IBlock>();
            if (block != null)
            {
                TurnAround();
                return;
            }
        }
    }
}