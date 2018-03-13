using System;
using Interfaces;
using UnityEngine;

namespace TriggerArea
{
    public class OutOfBounds : MonoBehaviour, IBoundary
    {
        [SerializeField] private bool _destroyObject;

        public bool IsObstacle
        {
            get { return false; }
        }
        
        public bool DestroyObject
        {
            get { return _destroyObject; }
            set { _destroyObject = value; }
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            IKillable killable = other.GetComponent<IKillable>();
            if (killable != null)
            {
                killable.Kill();
            }

            if (_destroyObject)
            {
                Destroy(other);
            }
        }
    }
}