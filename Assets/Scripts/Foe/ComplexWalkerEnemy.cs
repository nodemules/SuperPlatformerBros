using System;
using Interfaces;
using PlayerCharacter;
using UnityEngine;
using Random = System.Random;

namespace Foe
{
    public class ComplexWalkerEnemy : SimpleWalkerEnemy, IBoss
    {
        public Transform Player;
        public float SmoothTime = 5.0f;
        private int _jumpPossible;

        private Vector3 _smoothVelocity = Vector3.zero;
        
        public new void Update()
        {
            base.Update();
            Move();
        }

        protected override void Move()
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            transform.position = Vector3.SmoothDamp(transform.position, Player.position,
                ref _smoothVelocity, SmoothTime);

            _jumpPossible = rnd.Next(1, 100);
            if (_jumpPossible == 3)
            {
                Jump();
            }
        }

        private void Jump()
        {
            Rigidbody.velocity = Vector2.up * 10;
        }
    }
}