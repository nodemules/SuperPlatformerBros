using Interfaces;
using UnityEngine;

namespace TriggerArea
{
    public class OutOfBounds : MonoBehaviour, IBoundary
    {
        public bool IsObstacle
        {
            get { return false; }
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            IKillable killable = other.GetComponent<IKillable>();
            if (killable != null)
            {
                killable.Kill();
            }
        }
    }
}