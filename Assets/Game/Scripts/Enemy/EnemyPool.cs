using System;
using Game.Generic;
using UnityEngine.Events;

namespace Game.Enemy
{
    public class EnemyPool : ICustomObjectPool<Enemy>
    {
        private ICustomPool<Enemy> _pool;

        private Func<Enemy> OnCreateInstance;
        private UnityAction<Enemy> OnUpdatePlayerInfo;

        public EnemyPool(Func<Enemy> onCreateInstance, UnityAction<Enemy> onUpdatePlayerInfo)
        {
            OnUpdatePlayerInfo = onUpdatePlayerInfo;
            OnCreateInstance = onCreateInstance;
            _pool = new CustomPool<Enemy>(this);
        }

        #region PoolSettings

        public void TakeObject(Enemy objPool)
        {
            OnUpdatePlayerInfo?.Invoke(objPool);
            objPool.gameObject.SetActive(true);
        }

        public Enemy CreateObject()
        {
            var newObject = OnCreateInstance?.Invoke();

            if (newObject == null) return null;

            newObject.gameObject.SetActive(false);
            return newObject;
        }

        public void ReleaseObject(Enemy objToReturn)
        {
            objToReturn.gameObject.SetActive(false);
        }

        #endregion
    }
}