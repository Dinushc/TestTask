using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Misc.Pools
{
    public class DynamicUiList<T> where T : Component
    {
        private readonly WrapperPool<T> _pool;
        private readonly List<T> _views = new();

        public DynamicUiList(T prefab, Action<T> onCreateAction, Transform content)
        {
            _pool = new WrapperPool<T>(prefab, onCreateAction, content);
        }

        public void GetNewViews(int count, out List<T> views)
        {
            ClearList();
            GetViews(count, out views);
        }

        private void GetViews(int count, out List<T> views)
        {
            for (var i = 0; i < count; i++)
            {
                var view = _pool.Get();
                _views.Add(view);
                view.transform.SetSiblingIndex(i);
            }
            views = _views;
        }

        public void ClearList()
        {
            _views.ForEach(view => _pool.Release(view));
            _views.Clear();
        }
    }
}
