using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Skillitronic.LeoECSLite.GameObjectResourceManager.Factory;
using Skillitronic.LeoECSLite.GameObjectResourceManager.Pool;
using UnityEngine;

namespace Skillitronic.LeoECSLite.GameObjectResourceManager
{
    public sealed class GameObjectResourceManager : IDisposable
    {
        private readonly Dictionary<string, GameObjectPool> _objectPools;
        private readonly IGameObjectFactory _gameObjectFactory;

        public GameObjectResourceManager(IGameObjectFactory gameObjectFactory)
        {
            _objectPools = new();
            _gameObjectFactory = gameObjectFactory;
        }

        public async Task<GameObject> Build(string key, bool startActive = true)
        {
            GameObject go = await _gameObjectFactory.Create(key);
            go.SetActive(startActive);

            return go;
        }

        public async Task<T> Get<T>(string key)
        {
            GameObject result = await Get(key);
            return result.GetComponent<T>();
        }

        public async Task<GameObject> Get(string key)
        {
            GameObject result = await _objectPools[key].Get();
            return result;
        }
        
        public async Task<GameObject[]> PreAllocate(string key, int size)
        {
            return await _objectPools[key].PreAllocate(size);
        }

        public void Return(GameObject gameObject, string id)
        {
            if (_objectPools.ContainsKey(id))
            {
                _objectPools.Add(id, new GameObjectPool(() => _gameObjectFactory.Create(id)));
            }
            
            _objectPools[id].Return(gameObject);
        }

        public void Clear()
        {
            _objectPools.Clear();
        }

        public void Dispose()
        {
            Clear();
        }
    }
}