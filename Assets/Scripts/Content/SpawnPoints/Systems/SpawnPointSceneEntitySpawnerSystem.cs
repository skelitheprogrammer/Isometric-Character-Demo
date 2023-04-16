using Leopotam.EcsLite;
using Skillitronic.LeoECSLite.EntityDescriptors.Factory;
using UnityEngine;

namespace Content.SpawnPoints.Systems
{
    public class SpawnPointSceneEntitySpawnerSystem : IEcsInitSystem
    {
        private readonly IDescriptorEntityFactory _descriptorEntityFactory;

        public SpawnPointSceneEntitySpawnerSystem(IDescriptorEntityFactory descriptorEntityFactory)
        {
            _descriptorEntityFactory = descriptorEntityFactory;
        }

        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            SpawnPointEntityFactory spawnPointEntityFactory = new(_descriptorEntityFactory, world);
            CreateSpawnPointEntities(spawnPointEntityFactory);
        }
        
        private void CreateSpawnPointEntities(in SpawnPointEntityFactory spawnPointEntityFactory)
        {
            SpawnPointBehaviour[] views = Object.FindObjectsOfType<SpawnPointBehaviour>();
            
            for (int index = 0; index < views.Length; index++)
            {
                SpawnPointBehaviour spawnPointBehaviour = views[index];
                spawnPointEntityFactory.CreateAndSyncSpawnPointEntity(spawnPointBehaviour);
            }
        }
    }
}