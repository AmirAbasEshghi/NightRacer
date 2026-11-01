using System.Collections.Generic;
using UnityEngine;

namespace NightRacer.Pooling
{
    public class Pool<T> where T : Component
    {
        private readonly Queue<T> pool;

        public Pool(T prefab, Transform parent, int initialCount)
        {
            pool = new Queue<T>(initialCount);
            for (int i = 0; i < initialCount; i++)
            {
                T item = Object.Instantiate(prefab, parent);
                item.gameObject.SetActive(false);
                pool.Enqueue(item);
            }
        }

        public T Get(Vector3 position, Quaternion rotation)
        {
            if (pool.Count == 0)
            {
                return null;
            }

            T item = pool.Dequeue();
            item.transform.SetPositionAndRotation(position, rotation);
            item.gameObject.SetActive(true);
            return item;
        }

        public void Release(T item)
        {
            if (item == null || !item.gameObject.activeSelf)
            {
                return;
            }

            item.gameObject.SetActive(false);
            pool.Enqueue(item);
        }
    }
}

