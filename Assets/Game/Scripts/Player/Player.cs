using System;
using Game.So;
using UnityEngine;

namespace Game.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private CharacterSo _playerSo;

        public Action<Player> collisionOccurred = delegate { };

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out Enemy.Enemy enemyComponent))
            {
                enemyComponent.TakeDamage();
                TakeDamage();
                collisionOccurred?.Invoke(this);
            }
        }

        private void TakeDamage()
        {
            _playerSo.DecrementLife(1);
            if (_playerSo.CurrentLife <= 0)
            {
                PlayerDead();
            }
        }

        private void PlayerDead()
        {
            Debug.Log("Dead");
        }
    }
}