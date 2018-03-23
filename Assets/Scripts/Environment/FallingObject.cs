﻿using Interfaces;
using UnityEngine;

namespace Environment
{
    public class FallingObject : MonoBehaviour, ITriggerable
    {
        private Rigidbody2D _rigidbody2D;
        private const int _forceFactor = 1000;
        public Vector2 Direction;
        public bool EnableDirectionalForce;

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public void Trigger()
        {
            if (EnableDirectionalForce)
            {
                _rigidbody2D.AddForce(Direction * _forceFactor);
            }
            else
            {
                _rigidbody2D.gravityScale = 1;
            }

            Destroy(gameObject, 2f);
        }
    }
}