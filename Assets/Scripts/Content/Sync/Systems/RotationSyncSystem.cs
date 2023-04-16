using Common.Utils.Components;
using Leopotam.EcsLite;

namespace Common.Utils.Sync.Systems
{
    public class RotationSyncSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsPool<RotationComponent> _positionPool;
        private EcsPool<GameObjectReferenceComponent> _referencePool;

        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            _filter = world.Filter<RotationComponent>().Inc<GameObjectReferenceComponent>().End();
            _positionPool = world.GetPool<RotationComponent>();
            _referencePool = world.GetPool<GameObjectReferenceComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int i in _filter)
            {
                _referencePool.Get(i).GameObject.transform.rotation = _positionPool.Get(i).Rotation;
            }
        }
    }
}