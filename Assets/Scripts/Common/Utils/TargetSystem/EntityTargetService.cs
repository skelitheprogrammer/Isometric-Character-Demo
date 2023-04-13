using Common.Utils.TargetSystem.Components;
using Leopotam.EcsLite;

namespace Common.Utils.TargetSystem
{
    public class EntityTargetService
    {
        private readonly EcsWorld _world;
        private readonly EcsPool<EntityTargetComponent> _targetPool;

        public EntityTargetService(EcsWorld world)
        {
            _world = world;
            _targetPool = world.GetPool<EntityTargetComponent>();
        }

        public void SetTarget(int authorEntity, int targetEntity)
        {
            EcsPackedEntity packedEntity = _world.PackEntity(targetEntity);

            if (!_targetPool.Has(authorEntity))
            {
                _targetPool.Add(authorEntity);
            }
            
            _targetPool.Get(authorEntity).LinkedEntity = packedEntity;
        }
    }
}