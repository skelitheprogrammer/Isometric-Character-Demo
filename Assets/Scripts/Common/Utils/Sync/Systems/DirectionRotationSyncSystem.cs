using Common.Utils.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Common.Utils.Sync.Systems
{
    public class DirectionRotationSyncSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsPool<RotationComponent> _rotationPool;
        private EcsPool<DirectionComponent> _directionPool;
        
        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            _filter = world.Filter<DirectionComponent>().Inc<GameObjectReferenceComponent>().End();
            _rotationPool = world.GetPool<RotationComponent>();
            _directionPool = world.GetPool<DirectionComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int i in _filter)
            {
                ref DirectionComponent direction = ref _directionPool.Get(i);
                RotationComponent rotation = _rotationPool.Get(i);

                direction.ForwardDirection = rotation.Rotation * Vector3.forward;
                direction.RightDirection = rotation.Rotation * Vector3.right;
                direction.UpDirection = rotation.Rotation * Vector3.up;

            }
        }
    }
}