using Common.Utils.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Common.Utils.Sync.Systems
{
    public class DirectionSyncSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsPool<GameObjectReferenceComponent> _referencePool;
        private EcsPool<DirectionComponent> _directionPool;
        
        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            _filter = world.Filter<DirectionComponent>().Inc<GameObjectReferenceComponent>().End();
            _referencePool = world.GetPool<GameObjectReferenceComponent>();
            _directionPool = world.GetPool<DirectionComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int i in _filter)
            {
                DirectionComponent direction = _directionPool.Get(i);
                Transform referenceTransform = _referencePool.Get(i).GameObject.transform;
                
                referenceTransform.forward = direction.ForwardDirection;
                referenceTransform.right = direction.RightDirection;
                referenceTransform.up = direction.UpDirection;
            }
        }
    }
}