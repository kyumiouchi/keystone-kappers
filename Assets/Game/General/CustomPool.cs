using System.Collections.Generic;
using UnityEngine;

namespace Game.Generic
{
    public class CustomPool<T> : ICustomPool<T> where T : Component
    {
        private ICustomObjectPool<T> _iCustomObjectPool;
        private Stack<T> _pool = new Stack<T>();

        public CustomPool(ICustomObjectPool<T> iCustomObjectPool)
        {
            _iCustomObjectPool = iCustomObjectPool;
            CreateInitialPool();
        }

        public T Get()
        {
            if (_pool.Count <= 0)
            {
                CreatePoolObject();
            }

            var objPool = _pool.Pop();
            _iCustomObjectPool.TakeObject(objPool);

            return objPool;
        }

        public void Return(T objToReturn)
        {
            _iCustomObjectPool.ReleaseObject(objToReturn);
            _pool.Push(objToReturn);
        }

        public int Count()
        {
            return _pool.Count;
        }

        private void CreateInitialPool()
        {
            CreatePoolObject();
        }

        private void CreatePoolObject()
        {
            _pool.Push(_iCustomObjectPool.CreateObject());
        }
    }

    public interface ICustomObjectPool<T> where T : Component
    {
        void TakeObject(T objPool);
        T CreateObject();
        void ReleaseObject(T objToReturn);
    }

    public interface ICustomPool<T> where T : Component
    {
        T Get();

        void Return(T objToReturn);

        int Count();
    }
}
