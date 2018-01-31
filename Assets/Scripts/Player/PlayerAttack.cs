using Enemy;
using UnityEngine;

namespace Player
{
    public class PlayerAttack : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            Enemy.Enemy enemy = other.gameObject.GetComponent<IEnemy>() as Enemy.Enemy;
            if (enemy != null)
            {
                enemy.Kill();
            }
        }
    }
}