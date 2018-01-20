using Interfaces;
using UnityEngine;

namespace TriggerArea
{
    public class StartArea : MonoBehaviour, IBoundary
    {
        public bool IsObstacle
        {
            get { return false; }
        }
    }
}