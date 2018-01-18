using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerMove : MonoBehaviour
    {
        public int Speed = 10;
        public int JumpPower = 1250;

        private bool _facingRight = true;
        private Rigidbody2D _playerRigidbody;
        
        private Collider2D _playerCollider;
        private GameObject[] _platforms;

        public void Start()
        {
            _playerRigidbody = gameObject.GetComponent<Rigidbody2D>();
            _playerCollider = gameObject.GetComponent<Collider2D>();
            _platforms = GameObject.FindGameObjectsWithTag("Platforms");
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
                    print("Turning to the left, moveX=" + moveX);
                }
                else if (movingRight && !_facingRight)
                {
                    FlipPlayerX();
                    print("Turning to the right, moveX=" + moveX);
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
            _playerRigidbody.AddForce(Vector2.up * JumpPower);
        }

        // TODO - make this work with collider detection with platform tagged GameObjects
        private void JumpWithPlatformCollider()
        {
            print(_platforms.Length + " platforms found");
            bool onGround = false;
            foreach (GameObject platform in _platforms)
            {
                onGround = _playerCollider.IsTouching(platform.GetComponent<Collider2D>());
                print("Checking a platform, onGround=" + onGround);
                if (onGround)
                {
                    break;
                }
            }

            if (onGround)
            {
                _playerRigidbody.AddForce(Vector2.up * JumpPower);
            }
        }

        private void FlipPlayerX()
        {
            _facingRight = !_facingRight;
            Vector2 localScale = gameObject.transform.localScale;
            localScale.x *= -1;
            print("localScale.x=" + localScale.x);
            transform.localScale = localScale;
        }
    }
}