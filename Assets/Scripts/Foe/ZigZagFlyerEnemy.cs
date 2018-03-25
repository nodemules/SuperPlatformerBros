using UnityEngine;

namespace Foe
{
    public class ZigZagFlyerEnemy : SimpleFlyerEnemy
    {
        public Vector2 LeftRightBound;
        private float _leftBound;
        private float _rightBound;
        private int _leftRightDirection = 1;

        private new void Start()
        {
            DoInitialization();
            _leftBound = InitialPosition.x - LeftRightBound.x;
            _rightBound = InitialPosition.x + LeftRightBound.y;
        }

        protected override void Move()
        {
            Fly();
        }

        protected override void StopMoving()
        {
            StopFlying();
        }

        protected new void Fly()
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
            Rigidbody.velocity = new Vector2(Speed * _leftRightDirection, Speed * Direction);
        }

        protected new void CheckDirection()
        {
            base.CheckDirection();
            switch (_leftRightDirection)
            {
                case -1:
                    // Moving Left
                    if (transform.position.x <= _leftBound)
                    {
                        _leftRightDirection *= -1;
                    }

                    break;
                case 1:
                    //Moving Right
                    if (transform.position.x >= _rightBound)
                    {
                        _leftRightDirection *= -1;
                    }

                    break;
                default:
                    _leftRightDirection = 1;
                    break;
            }
        }
    }
}