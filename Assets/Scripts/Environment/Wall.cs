using Interfaces;
using UnityEngine;

namespace Environment
{
    public class Wall : MonoBehaviour, IBoundary
    {
        public bool IsObstacle
        {
            get { return true; }
        }
    }
}