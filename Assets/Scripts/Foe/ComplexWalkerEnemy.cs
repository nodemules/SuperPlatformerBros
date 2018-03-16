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
        
        private Vector3 _smoothVelocity = Vector3.zero;

        public new void Update()
        {
            base.Update();
            Move();
        }

        protected override void Move()
        {
            transform.position = Vector3.SmoothDamp(transform.position, Player.position,
                ref _smoothVelocity, SmoothTime);
            
        }
    }
}