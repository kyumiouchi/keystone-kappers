using System;
using Game.Player;
using UnityEngine;

namespace Game.Enemy
{
    public class EnemyGenerate : MonoBehaviour
    {
        [SerializeField] private Enemy _prefabEnemy;
        [SerializeField] private Player.Player _player;
        [SerializeField] private Camera _mainCamera;
        
        private EnemyPool _enemyPool;
        
        private void Start()
        {
            _enemyPool = new EnemyPool(OnCreateInstance, OnUpdatePlayerInfo);
            Vector3 leftBottomWorldPosition = _mainCamera.ScreenToWorldPoint(Vector3.zero);
            // _leftPosition = new Vector3(leftBottomWorldPosition.x, )
        }

        private Enemy OnCreateInstance()
        {
            // var enemy = Instantiate(_prefabEnemy, Player.Player, Quaternion.identity);
            return null;
        }

        private void OnUpdatePlayerInfo(Enemy enemy)
        {
            
        }
    }
}
