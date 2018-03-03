using Interfaces;
using UnityEngine;

namespace TriggerArea
{
    public class FallingObject : MonoBehaviour , ITriggerable
    {
        private Rigidbody2D _rigidbody2D;
        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public void Trigger()
        {
            _rigidbody2D.gravityScale = 1;
            Destroy(gameObject, 2f);
        }

    }
}