using System;
using UnityEngine;
using UnityEngine.Pool;

namespace UI.Misc.Pools
{
    public class WrapperPool<T>
            where T : Component
    {
        private readonly ObjectPool<T> _pool;
        private readonly Action<T> _actionOnCreate;
        private readonly T _prefab;
        private readonly Transform _parent;

        public WrapperPool(T prefab, Action<T> actionOnCreate, Transform parent = null)
        {
            _pool = new ObjectPool<T>(Create, actionOnRelease: ActionOnRelease);
            _actionOnCreate = actionOnCreate;
            _prefab = prefab;
            _parent = parent;
        }

        public virtual T Get()
        {
            var result = _pool.Get();
            result.gameObject.SetActive(true);
            return result;
        }

        public virtual void Release(T obj)
        {
            obj.gameObject.SetActive(false);
            _pool.Release(obj);
        }

        protected void ActionOnRelease(T obj)
        {
            obj.gameObject.SetActive(false);
        }

        protected T Create()
        {
            var result = UnityEngine.Object.Instantiate(_prefab, _parent);

            if (_actionOnCreate != null)
                _actionOnCreate(result);

            return result;
        }
    }
}
