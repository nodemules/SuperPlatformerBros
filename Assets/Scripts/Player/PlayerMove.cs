﻿using System.Linq;
using Environment;
using Interfaces;
using UnityEngine;

namespace Player
{
    public class PlayerMove : MonoBehaviour
    {
        public int Speed = 10;
        public int JumpPower = 2500;

        private bool _facingRight;
        private Rigidbody2D _playerRigidbody;

        private Collider2D _playerCollider;
        private const int MaxContacts = 10;

        public bool IsFacingRight()
        {
            return _facingRight;
        }

        public void Start()
        {
            _playerRigidbody = gameObject.GetComponent<Rigidbody2D>();
            _playerCollider = gameObject.GetComponent<Collider2D>();
        }

        public void Update()
        {
            float moveX = Input.GetAxis("Horizontal");
            Move(moveX);
        }

        private void Move(float moveX)
        {
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }


            bool movingRight = moveX > 0.0f;
            bool moving = !Equals(moveX, 0.0f);

            if (moving)
            {
                if (!movingRight && _facingRight)
                {
                    FlipPlayerX();
                }
                else if (movingRight && !_facingRight)
                {
                    FlipPlayerX();
                }
                else
                {
                    // moving in the correct direction, do nothing for now
                }
            }

            _playerRigidbody.velocity = new Vector2(moveX * Speed, _playerRigidbody.velocity.y);
        }

        private void Jump()
        {
            ContactPoint2D[] contacts = new ContactPoint2D[MaxContacts];
            if (_playerCollider.GetContacts(contacts) < 1)
            {
                return;
            }

            bool grounded = contacts
                .Select(p => p.collider.gameObject.GetComponent<IJumpable>())
                .Any(jumpable => jumpable != null);

            if (grounded)
            {
                Vector3 gravity = Physics.gravity;
                Vector2 factor = new Vector2(0, gravity.y * _playerRigidbody.gravityScale);
                factor *= 0.10f;

                _playerRigidbody.AddForce(factor * JumpPower);
            }
        }

        private void FlipPlayerX()
        {
            _facingRight = !_facingRight;
            Vector2 localScale = gameObject.transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }
}