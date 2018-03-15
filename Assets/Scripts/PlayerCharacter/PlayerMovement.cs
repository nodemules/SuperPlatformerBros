using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces;
using UnityEngine;

namespace PlayerCharacter
{
    public class PlayerMovement : MonoBehaviour
    {
        #region properties

        private const int DefaultAcceleration = 25;
        private const int MaxContacts = 100;
        private const float JumpingReduxFactor = 0.1f;
        private const float WalkingReduxFactor = 1.0f;

        private AudioSource _playerAudioSource;
        private Rigidbody2D _playerRigidbody;
        private Collider2D _playerCollider;
        private Animator _playerAnimator;

        public float JumpPower = 7.75f;
        public float FallMultiplier = 2.8f;
        public float LowJumpModifier = 2.1f;

        public AudioClip JumpAudio;

        public int Acceleration = DefaultAcceleration;
        public float MaxSpeed = 5;
        public bool MovementEnabled = true;

        public bool IsFacingRight { get; private set; }

        private bool _isJumping;

        #endregion

        public void Start()
        {
            _playerRigidbody = gameObject.GetComponent<Rigidbody2D>();
            _playerCollider = gameObject.GetComponent<Collider2D>();
            _playerAudioSource = GetComponent<AudioSource>();
            _playerAnimator = GetComponent<Animator>();
            _playerAnimator.Play("Still");
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
                DetermineAnimation(moveX);
                Move(moveX);
                PlayerGravity();
            }
        }

        private void PlayerGravity()
        {
            if (_playerRigidbody.velocity.y < 0)
            {
                _playerRigidbody.velocity += Vector2.up * Physics2D.gravity.y * FallMultiplier * Time.deltaTime;
            }
            else if (_playerRigidbody.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                _playerRigidbody.velocity += Vector2.up * Physics2D.gravity.y * LowJumpModifier * Time.deltaTime;
            }
        }

        private void DetermineAnimation(float moveX)
        {
            bool moving = !Equals(moveX, 0.0f);

            if (moving || _isJumping)
            {
                _playerAnimator.Play("Walking");
            }
            else
            {
                _playerAnimator.Play("Still");
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
                _playerAnimator.Play("Walking");
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

            _isJumping = true;
            _playerAudioSource.PlayOneShot(JumpAudio);
            _playerRigidbody.velocity = Vector2.up * JumpPower;
        }

        private bool IsGrounded()
        {
            ContactPoint2D[] contacts = new ContactPoint2D[MaxContacts];
            int numContacts = _playerCollider.GetContacts(contacts);

            if (numContacts < 1)
            {
                return false;
            }

            List<ContactPoint2D> standingPoints = contacts
                .ToList()
                .Where(p => p.collider != null && p.normal.y * _playerRigidbody.gravityScale > 0)
                .ToList();

            if (standingPoints.Count == 0)
            {
                return false;
            }

            bool grounded = standingPoints
                .Select(p => p.collider.gameObject.GetComponent<IJumpable>())
                .Any(jumpable => jumpable != null);

            if (!grounded)
            {
                grounded = standingPoints
                    .Select(p => p.collider.gameObject.GetComponent<IKillable>())
                    .Any(enemy => enemy != null && enemy.IsDead);
            }

            return grounded;
        }

        private void EndJump()
        {
            _isJumping = false;
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