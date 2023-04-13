using System;
using Leopotam.EcsLite;
using Skillitronic.LeoECSLite.EntityDescriptors.Factory;
using Object = UnityEngine.Object;

namespace Content.SpawnPoints.Systems
{
    public class SpawnPointGatherSystem : IEcsInitSystem, IEcsDestroySystem
    {
        private readonly IDescriptorEntityFactory _entityFactory;
        private SpawnPointFactory _factory;

        public SpawnPointGatherSystem(IDescriptorEntityFactory entityFactory)
        {
            _entityFactory = entityFactory;
        }

        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            
            _factory = new SpawnPointFactory(_entityFactory, world);
            
            CreateSpawnPointEntities();
        }
        
        public void Destroy(IEcsSystems systems)
        {
            _factory = null; 
        }

        private void CreateSpawnPointEntities()
        {
            SpawnPointView[] spawnPointViews = Object.FindObjectsOfType<SpawnPointView>();
            
            for (int i = 0; i < spawnPointViews.Length; i++)
            {
                SpawnPointView spawnPointView = spawnPointViews[i];

                if (spawnPointView.SpawnType == SpawnType.NONE)
                {
                    throw new InvalidOperationException("Set Spawn Type");
                }
                
                _factory.CreateSpawnPointEntity(spawnPointView.Position, spawnPointView.Rotation,spawnPointView.SpawnType);
            }
        }
        
    }
}