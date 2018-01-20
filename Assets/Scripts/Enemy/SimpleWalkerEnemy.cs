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
            print("Stopping walking");
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
                string direction = Direction == -1 ? "Left" : "Right";
                print("Starting walking [" + direction + "] at " + Speed + " units per second");
            }

            Rigidbody.velocity = new Vector2(Speed * Direction, Rigidbody.velocity.y);

            switch (Direction)
            {
                case -1:
                    // Moving Left
                    if (transform.position.x <= _leftBound)
                    {
                        print("Too far left, turning around");
                        TurnAround();
                    }

                    break;
                case 1:
                    //Moving Right
                    if (transform.position.x >= _rightBound)
                    {
                        print("Too far right, turning around");
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
                print("Collided with a wall");
                if (wall.IsObstacle)
                {
                    TurnAround();
                }
            }
            
        }
    }
}