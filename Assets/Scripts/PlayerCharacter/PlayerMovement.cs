using System;
using System.Linq;
using Interfaces;
using UnityEngine;

namespace PlayerCharacter
{
    public class PlayerMovement : MonoBehaviour
    {
        public int Speed = 10;
        public int JumpPower = 2500;
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


            bool movingRight = moveX > 0.0f;
            bool moving = !Equals(moveX, 0.0f);

            if (moving && (!movingRight && _facingRight || movingRight && !_facingRight))
            {
                FlipPlayerX();
            }

            _playerRigidbody.velocity = new Vector2(moveX * Speed, _playerRigidbody.velocity.y);
        }

        private void Jump()
        {
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
                Vector3 gravity = Physics.gravity;
                Vector2 factor = new Vector2(0, gravity.y * _playerRigidbody.gravityScale * -1);
                factor *= 0.10f;

                _playerAudioSource.PlayOneShot(JumpAudio);
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