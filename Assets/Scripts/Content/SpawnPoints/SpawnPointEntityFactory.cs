using Common.Utils;
using Common.Utils.Components;
using Content.SpawnPoints.Components;
using Leopotam.EcsLite;
using Skillitronic.LeoECSLite.EntityDescriptors;
using Skillitronic.LeoECSLite.EntityDescriptors.Factory;

namespace Content.SpawnPoints
{
    public class SpawnPointEntityFactory
    {
        private readonly IDescriptorEntityFactory _descriptorEntityFactory;
        private readonly EcsWorld _world;

        public SpawnPointEntityFactory(IDescriptorEntityFactory descriptorEntityFactory, EcsWorld world)
        {
            _descriptorEntityFactory = descriptorEntityFactory;
            _world = world;
        }
        
        public void CreateAndSyncSpawnPointEntity(SpawnPointBehaviour behaviour)
        {
            EntityInitializer spawnPointInit = _descriptorEntityFactory.Create<SpawnPointEntityDescriptor>(_world);

            spawnPointInit.InitComponent(new PositionComponent()
            {
                Position = behaviour.Position,
            });

            spawnPointInit.InitComponent(new RotationComponent()
            {
                Rotation = behaviour.Rotation,
            });

            spawnPointInit.InitComponent(new SpawnTypeComponent()
            {
                SpawnType = behaviour.SpawnType,
            });

            behaviour.GetComponent<EntityReferenceHolder>().Entity = spawnPointInit.Entity;
        }
    }
}