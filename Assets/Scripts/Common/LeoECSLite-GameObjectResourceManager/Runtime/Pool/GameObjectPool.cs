using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Skillitronic.LeoECSLite.GameObjectResourceManager.Pool
{
    internal sealed class GameObjectPool : IDisposable
    {
        private readonly Stack<GameObject> _pool;
        private Func<Task<GameObject>> _createFunc;

        public GameObjectPool(Func<Task<GameObject>> createFunc)
        {
            _createFunc = createFunc;
            _pool = new();
        }

        public async Task<GameObject[]> PreAllocate(int size)
        {
            GameObject[] array = new GameObject[size];
            
            for (int i = 0; i < size; i++)
            {
                array[i] = await _createFunc();
                array[i].SetActive(false);
                _pool.Push(array[i]);
            }

            return array;
        }
        
        public async Task<T> Get<T>() where T : Component
        {
            GameObject result = await Get();

            return result.GetComponent<T>();
        }

        public async Task<GameObject> Get()
        {
            if (!_pool.TryPop(out GameObject result))
            {
                result = await _createFunc();
            }

            return result;
        }
        
        public void Return(GameObject target)
        {
            target.SetActive(false);
            
            _pool.Push(target);
        }

        public void Dispose()
        {
            _createFunc = null;
            _pool.Clear();
        }
    }
}