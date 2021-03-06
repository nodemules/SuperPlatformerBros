﻿using System;
using UnityEngine;
using Random = System.Random;

namespace Foe
{
    public class ComplexWalkerEnemy : Enemy, IBoss, IPowerful
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
            if (EnableMovement)
            {
                transform.position = Vector3.SmoothDamp(transform.position, Player.position,
                    ref _smoothVelocity, SmoothTime);

                Random rnd = new Random(DateTime.Now.Millisecond);
                _jumpPossible = rnd.Next(1, 100);
                if (_jumpPossible == 3)
                {
                    Jump();
                }
            }

        }

        protected override void StopMoving()
        {
            StopWalking();
        }

        private void StopWalking()
        {
            Animator.enabled = false;
            Rigidbody.velocity = new Vector2(0, Rigidbody.velocity.y);
        }

        private void KillBoss()
        {
            transform.position = Vector3.zero;
            Invoke("BlowUp", 0.75f);
        }

        private void BlowUp()
        {
            Destroy(gameObject);
        }

        protected override void TurnAround()
        {
            // Do Nothing;
        }

        private void Jump()
        {
            Rigidbody.velocity = Vector2.up * 10;
        }

        public void PowerUp()
        {
            if (IsDead)
            {
                return;
            }

            SmoothTime = 12;
            gameObject.transform.localScale *= 2;
        }
        
        public void StartMoving()
        {
            Animator.enabled = true;
            gameObject.GetComponent<Enemy>().EnableMovement = true;
        }

        public void EnableBoss()
        {
            gameObject.SetActive(true);
        }
    }
}