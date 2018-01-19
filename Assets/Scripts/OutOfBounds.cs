using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class OutOfBounds : MonoBehaviour
    {
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