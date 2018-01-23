using UnityEngine;

namespace Enemy
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
    }
}