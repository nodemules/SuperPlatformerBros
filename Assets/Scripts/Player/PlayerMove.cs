﻿using UnityEngine;

namespace Player
{
    public class PlayerMove : MonoBehaviour
    {
        public int Speed = 10;
        public int JumpPower = 2500;

        private bool _facingRight = true;
        private Rigidbody2D _playerRigidbody;

        private Collider2D _playerCollider;
        private GameObject[] _platforms;
        private GameObject[] _blocks;

        public bool IsFacingRight()
        {
            return _facingRight;
        }

        public void Start()
        {
            _playerRigidbody = gameObject.GetComponent<Rigidbody2D>();
            _playerCollider = gameObject.GetComponent<Collider2D>();
            _platforms = GameObject.FindGameObjectsWithTag("Platforms");
            _blocks = GameObject.FindGameObjectsWithTag("Blocks");
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
            bool onGround = false;
            bool touchingBlock = false;
            foreach (GameObject platform in _platforms)
            {
                onGround = _playerCollider.IsTouching(platform.GetComponent<Collider2D>());
                if (onGround)
                {
                    break;
                }
            }

            foreach (GameObject block in _blocks)
            {
                touchingBlock = _playerCollider.IsTouching(block.GetComponent<Collider2D>());
                if (touchingBlock)
                {
                    break;
                }
            }

            if (onGround || touchingBlock)
            {
                _playerRigidbody.AddForce(Vector2.up * JumpPower);
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