using Common.Utils.Components;
using Leopotam.EcsLite;

namespace Common.Utils.Sync.Systems
{
    public class PositionSyncSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsPool<PositionComponent> _positionPool;
        private EcsPool<GameObjectReferenceComponent> _referencePool;

        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            _filter = world.Filter<PositionComponent>().Inc<GameObjectReferenceComponent>().End();
            _positionPool = world.GetPool<PositionComponent>();
            _referencePool = world.GetPool<GameObjectReferenceComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int i in _filter)
            {
                _referencePool.Get(i).GameObject.transform.position = _positionPool.Get(i).Position;
            }
        }
    }
}