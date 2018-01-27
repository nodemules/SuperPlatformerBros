using Interfaces;
using UnityEngine;

namespace Environment
{
    public class Wall : MonoBehaviour, IBoundary, IJumpable
    {
        public bool IsObstacle
        {
            get { return true; }
        }
    }
}