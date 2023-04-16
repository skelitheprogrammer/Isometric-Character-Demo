using System;
using Common.Collision.Behaviour;
using Common.Collision.Components;
using Common.Utils;
using Leopotam.EcsLite;

namespace Common.Collision.Systems
{
    public class SyncCollisionsSystem : IEcsInitSystem, IEcsPostRunSystem
    {
        private EcsFilter _filter;
        private EcsPool<CollisionComponent> _collisionPool;
        private EcsPool<GameObjectReferenceComponent> _referencePool;
        readonly Action<int, CollisionData> _onCollidedWithTarget;

        public SyncCollisionsSystem()
        {
            _onCollidedWithTarget = OnCollidedWithTarget;
        }

        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            _filter = world.Filter<CollisionComponent>().Inc<GameObjectReferenceComponent>().End();
            _collisionPool = world.GetPool<CollisionComponent>();
            _referencePool = world.GetPool<GameObjectReferenceComponent>();
        }

        public void PostRun(IEcsSystems systems)
        {
            foreach (int i in _filter)
            {
                if (_referencePool.Get(i).GameObject.TryGetComponent(out CollisionBehaviour trigger))
                {
                    trigger.Register(_onCollidedWithTarget);
                }
            }
        }
        
        private void OnCollidedWithTarget(int sender, CollisionData collisionData)
        {
            _collisionPool.Get(sender).CollisionData = collisionData;
        }
        
    }
}