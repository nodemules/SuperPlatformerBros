using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerMove : MonoBehaviour
    {
        public int Speed = 10;
        public int JumpPower = 1250;
        private bool _facingRight = true;

        void Update()
        {
            Move(Input.GetAxis("Horizontal"));
        }

        private void Move(float moveX)
        {
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }

            bool moving = Double.Equals(moveX, 0.0f);
            bool movingRight = moveX > 0.0f;

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

            Rigidbody2D playerRigidbody = gameObject.GetComponent<Rigidbody2D>();
            playerRigidbody.velocity = new Vector2(moveX * Speed, playerRigidbody.velocity.y);
        }

        private void Jump()
        {
            Rigidbody2D playerRigidbody = gameObject.GetComponent<Rigidbody2D>();
            playerRigidbody.AddForce(Vector2.up * JumpPower);
        }

        // TODO - make this work with collider detection with platform tagged GameObjects
        private void JumpWithPlatformCollider()
        {
            Rigidbody2D playerRigidbody = gameObject.GetComponent<Rigidbody2D>();
            Collider2D playerCollider = gameObject.GetComponent<Collider2D>();
            GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platforms");
            print(platforms.Length + " platforms found");
            bool onGround = false;
            foreach (GameObject platform in platforms)
            {
                onGround = playerRigidbody.IsTouching(platform.GetComponent<Collider2D>());
                print("Checking a platform, onGround=" + onGround);
                if (onGround)
                {
                    break;
                }
            }

            if (onGround)
            {
                playerRigidbody.AddForce(Vector2.up * JumpPower);
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