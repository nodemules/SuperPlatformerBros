﻿using UnityEngine;

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
            print("Stopping flying");
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
                string direction = Direction == -1 ? "Down" : "Up";
                print("Starting flying [" + direction + "] at " + Speed + " units per second");
            }

            Rigidbody.velocity = new Vector2(0, Speed * Direction);

            switch (Direction)
            {
                case -1:
                    // Moving Left
                    if (transform.position.y <= _lowerBound)
                    {
                        print("Too far down, turning around");
                        TurnAround();
                    }

                    break;
                case 1:
                    //Moving Right
                    if (transform.position.y >= _upperBound)
                    {
                        print("Too far up, turning around");
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