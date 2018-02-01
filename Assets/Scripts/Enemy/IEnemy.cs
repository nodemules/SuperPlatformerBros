using UnityEngine;

namespace Enemy
{
    public interface IEnemy
    {
        Rigidbody2D Rigidbody { get; set; }
        Vector3 InitialPosition { get; set; }
        AudioSource AudioSource { get; set; }
    }
}