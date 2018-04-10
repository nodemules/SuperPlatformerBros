using Interfaces;
using UnityEngine;

namespace Environment
{
    public class FallingObject : MonoBehaviour, ITriggerable
    {
        private Rigidbody2D _rigidbody2D;
        private const int ForceFactor = 500;
        public Vector2 Direction;
        public bool EnableDirectionalForce;

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _rigidbody2D.isKinematic = true;
        }

        public void Trigger()
        {
            if (EnableDirectionalForce)
            {
                _rigidbody2D.isKinematic = false;
                _rigidbody2D.AddForce(Direction * ForceFactor);
            }
            else
            {
                _rigidbody2D.isKinematic = false;
                _rigidbody2D.gravityScale = 1;
            }

            Destroy(gameObject, 2f);
        }
    }
}