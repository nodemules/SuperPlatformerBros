using Interfaces;
using UnityEngine;

namespace Environment
{
    public class Block : MonoBehaviour, IBlock, IJumpable
    {
        // ReSharper disable once Unity.RedundantEventFunction
        protected void OnCollisionEnter2D(Collision2D other)
        {
            // do block things
        }
    }
}