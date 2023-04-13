using Common.Utils.Components;
using Content.SpawnPoints.Components;
using Leopotam.EcsLite;
using Skillitronic.LeoECSLite.EntityDescriptors;
using Skillitronic.LeoECSLite.EntityDescriptors.Factory;
using UnityEngine;

namespace Content.SpawnPoints
{
    public class SpawnPointFactory
    {
        private readonly IDescriptorEntityFactory _descriptorEntityFactory;
        private readonly EcsWorld _world;
        
        public SpawnPointFactory(IDescriptorEntityFactory descriptorEntityFactory, EcsWorld world)
        {
            _descriptorEntityFactory = descriptorEntityFactory;
            _world = world;
        }

        public void CreateSpawnPointEntity(Vector3 position,Quaternion rotation, SpawnType spawnType)
        {
            EntityInitializer spawnPointInit = _descriptorEntityFactory.Create<SpawnPointDescriptor>(_world);
            
            spawnPointInit.InitComponent(new PositionComponent()
            {
                Position = position,
            });
            
            spawnPointInit.InitComponent(new RotationComponent()
            {
                Rotation = rotation,
            });
            
            spawnPointInit.InitComponent(new SpawnTypeComponent()
            {
                SpawnType = spawnType,
            });
        }
    }
}