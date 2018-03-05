using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Foe;
using Interfaces;
using NUnit.Framework;
using UnityEngine;

namespace PlayerCharacter
{
    public class PlayerMovement : MonoBehaviour
    {
        #region properties

        private const int DefaultAcceleration = 25;
        private const int JumpAcceleration = 35;
        private const int MaxContacts = 100;
        private const float JumpingReduxFactor = 0.1f;
        private const float WalkingReduxFactor = 1.0f;

        private AudioSource _playerAudioSource;
        private Rigidbody2D _playerRigidbody;
        private Collider2D _playerCollider;

        public AudioClip JumpAudio;

        public int Acceleration = DefaultAcceleration;
        public int JumpPower = 7;
        public int MaxSpeed = 5;
        public bool MovementEnabled = true;

        public bool IsFacingRight { get; private set; }

        #endregion

        public void Start()
        {
            _playerRigidbody = gameObject.GetComponent<Rigidbody2D>();
            _playerCollider = gameObject.GetComponent<Collider2D>();
            _playerAudioSource = GetComponent<AudioSource>();
        }

        public void Update()
        {
            if (GlobalGameState.IsPaused)
            {
                return;
            }

            float moveX = Input.GetAxis("Horizontal");
            if (MovementEnabled)
            {
                Move(moveX);
            }
        }

        private void Move(float moveX)
        {
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }

            bool moving = !Equals(moveX, 0.0f);

            if (!moving && !Equals(_playerRigidbody.velocity.x, 0.0f))
            {
                float reduxFactor = WalkingReduxFactor;

                if (!IsGrounded())
                {
                    reduxFactor = JumpingReduxFactor;
                }

                float reducer = 1.00f - reduxFactor;

                _playerRigidbody.velocity = new Vector2(_playerRigidbody.velocity.x * reducer,
                    _playerRigidbody.velocity.y);
                return;
            }

            bool movingRight = moveX > 0.0f;

            if (moving && (!movingRight && IsFacingRight || movingRight && !IsFacingRight))
            {
                _playerRigidbody.velocity = new Vector2(0.0f, _playerRigidbody.velocity.y);
                FlipPlayerX();
            }

            if (Math.Abs(_playerRigidbody.velocity.x) > MaxSpeed)
            {
                return;
            }

            Vector2 velocity = new Vector2(moveX * Acceleration, 0f);

            if (!Equals(_playerRigidbody.velocity.x, 0.0f) &&
                _playerRigidbody.velocity.x < MaxSpeed * 0.3)
            {
                velocity *= 1.33f;
            }

            _playerRigidbody.AddForce(velocity);
        }

        private void Jump()
        {
            if (!IsGrounded())
            {
                return;
            }

            Acceleration = JumpAcceleration;
            Vector2 force = new Vector2(0, JumpPower * _playerRigidbody.gravityScale);

            _playerAudioSource.PlayOneShot(JumpAudio);
            _playerRigidbody.AddForce(force, ForceMode2D.Impulse);
        }

        private bool IsGrounded()
        {
            ContactPoint2D[] contacts = new ContactPoint2D[MaxContacts];
            int numContacts = _playerCollider.GetContacts(contacts);

            if (numContacts < 1)
            {
                return false;
            }

            bool grounded = contacts.ToList()
                .Where(p => p.collider != null && p.normal.y > 0)
                .Select(p => p.collider.gameObject.GetComponent<IJumpable>())
                .Any(jumpable => jumpable != null);

            if (!grounded)
            {
                grounded = contacts.ToList()
                    .Where(p => p.collider != null && p.normal.y > 0)
                    .Select(p => p.collider.gameObject.GetComponent<IKillable>())
                    .Any(enemy => enemy != null && enemy.IsDead);
            }

            return grounded;
        }

        private void EndJump()
        {
            Acceleration = DefaultAcceleration;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            IJumpable jumpable = other.gameObject.GetComponent<IJumpable>();
            if (jumpable != null)
            {
                EndJump();
            }
        }

        private void FlipPlayerX()
        {
            IsFacingRight = !IsFacingRight;
            Vector2 localScale = gameObject.transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }
}