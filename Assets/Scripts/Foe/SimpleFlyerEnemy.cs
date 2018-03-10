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
        private bool _isFlying;

        public new void Start()
        {
            DoInitialization();
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

        private void StopFlying()
        {
            _isFlying = false;
            Rigidbody.velocity = new Vector2(0, 0);
        }

        private void Fly()
        {
            if (!EnableMovement)
            {
                return;
            }

            if (!_isFlying)
            {
                _isFlying = true;
            }

            Rigidbody.velocity = new Vector2(0, Speed * Direction);

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
//        [SuppressMessage("ReSharper", "RedundantJumpStatement")]
        public new void OnCollisionEnter2D(Collision2D other)
        {
            base.OnCollisionEnter2D(other);
            print("SimpleFlyerEnemy colliding!");
            Collider2D otherCollider = other.collider;
            IPlatform platform = otherCollider.GetComponent<IPlatform>();
            if (platform != null)
            {
                print("SimpleFlyerEnemy colliding with platform!");
                TurnAround();
                return;
            }

            Wall wall = otherCollider.GetComponent<IBoundary>() as Wall;
            if (wall != null && wall.IsObstacle)
            {
                print("SimpleFlyerEnemy colliding with wall!");
                TurnAround();
                return;
            }

            IBlock block = otherCollider.GetComponent<IBlock>();
            if (block != null)
            {
                print("SimpleFlyerEnemy colliding with block!");
                TurnAround();
                return;
            }
            
            print("SimpleFlyerEnemy colliding with " + otherCollider.name);
        }
    }
}