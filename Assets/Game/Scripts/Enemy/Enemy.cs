using UnityEngine;

namespace Game.Enemy
{
    public class Enemy : MonoBehaviour
    {
        public void TakeDamage()
        {
            Debug.Log($"Enemy {gameObject.name} take damage");
        }
    }
}