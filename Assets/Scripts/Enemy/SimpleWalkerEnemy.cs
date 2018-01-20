using Environment;
using Interfaces;
using UnityEngine;

namespace Enemy
{
    public class SimpleWalkerEnemy : Enemy
    {
        private float _leftBound;
        private float _rightBound;
        private bool _isWalking;

        public new void Start()
        {
            DoInitialization();
            _leftBound = InitialPosition.x - Range.x;
            _rightBound = InitialPosition.x + Range.y;
        }

        protected override void Move()
        {
            Walk();
        }

        protected override void StopMoving()
        {
            StopWalking();
        }

        private void StopWalking()
        {
            _isWalking = false;
            Rigidbody.velocity = new Vector2(0, Rigidbody.velocity.y);
        }

        private void Walk()
        {
            if (!EnableMovement)
            {
                return;
            }

            if (!_isWalking)
            {
                _isWalking = true;
            }

            Rigidbody.velocity = new Vector2(Speed * Direction, Rigidbody.velocity.y);

            switch (Direction)
            {
                case -1:
                    // Moving Left
                    if (transform.position.x <= _leftBound)
                    {
                        TurnAround();
                    }

                    break;
                case 1:
                    // Moving Right
                    if (transform.position.x >= _rightBound)
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

        private new void OnCollisionEnter2D(Collision2D other)
        {
            base.OnCollisionEnter2D(other);
            Collider2D otherCollider = other.collider;
            Wall wall = otherCollider.GetComponent<IBoundary>() as Wall;
            if (wall != null)
            {
                if (wall.IsObstacle)
                {
                    TurnAround();
                }
            }
        }
    }
}