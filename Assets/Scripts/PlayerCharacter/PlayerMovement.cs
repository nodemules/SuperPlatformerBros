using System;
using System.Linq;
using Interfaces;
using UnityEngine;

namespace PlayerCharacter
{
    public class PlayerMovement : MonoBehaviour
    {
        private const int DefaultAcceleration = 25;
        private const int JumpAcceleration = 35;
        private const float JumpLength = 1.0f;
        public int Acceleration = DefaultAcceleration;
        public int JumpPower = 7;
        public int MaxSpeed = 5;
        private bool _isJumping;
        
        public AudioClip JumpAudio;
        private AudioSource _playerAudioSource;

        public bool MovementEnabled = true;

        private bool _facingRight;
        private Rigidbody2D _playerRigidbody;

        private Collider2D _playerCollider;
        private const int MaxContacts = 100;

        public bool IsFacingRight()
        {
            return _facingRight;
        }

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

            if (!moving)
            {
                _playerRigidbody.velocity = new Vector2(0f, _playerRigidbody.velocity.y);
                return;
            }
            
            bool movingRight = moveX > 0.0f;
            
            if (!movingRight && _facingRight || movingRight && !_facingRight)
            {
                FlipPlayerX();
            }
            
            if (Math.Abs(_playerRigidbody.velocity.x) > MaxSpeed)
            {
                return;
            }
            _playerRigidbody.AddForce(new Vector2(moveX * Acceleration, 0f));
        }

        private void Jump()
        {
            if (_isJumping)
            {
                return;
            }
            
            ContactPoint2D[] contacts = new ContactPoint2D[MaxContacts];
            int numContacts = _playerCollider.GetContacts(contacts);

            if (numContacts < 1)
            {
                return;
            }

            bool grounded = contacts.ToList()
                .Where(p => p.collider != null)
                .Select(p => p.collider.gameObject.GetComponent<IJumpable>())
                .Any(jumpable => jumpable != null);

            if (grounded)
            {
                _isJumping = true;
                Acceleration = JumpAcceleration;
                Vector2 force = new Vector2(0, JumpPower * _playerRigidbody.gravityScale);

                _playerAudioSource.PlayOneShot(JumpAudio);
                _playerRigidbody.AddForce(force, ForceMode2D.Impulse);
                
                Invoke("EndJump", JumpLength);
            }
        }

        private void EndJump()
        {
            _isJumping = false;
            Acceleration = DefaultAcceleration; 
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