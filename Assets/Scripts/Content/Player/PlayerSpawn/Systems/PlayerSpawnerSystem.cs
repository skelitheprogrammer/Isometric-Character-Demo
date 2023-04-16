using Common.Utils.Components;
using Common.Utils.TargetSystem;
using Content.Player.PlayerCamera;
using Content.SpawnPoints;
using Content.SpawnPoints.Components;
using Leopotam.EcsLite;
using Skillitronic.LeoECSLite.EntityDescriptors.Factory;
using Skillitronic.LeoECSLite.GameObjectResourceManager;
using UnityEngine;

namespace Content.Player.PlayerSpawn.Systems
{
    public class PlayerSpawnerSystem : IEcsInitSystem
    {
        private readonly PlayerData _playerData;
        private readonly IDescriptorEntityFactory _entityFactory;
        private readonly EntityTargetService _entityTargetService;
        private readonly GameObjectResourceManager _resourceManager;

        private EcsFilter _spawnPoints;

        private EcsPool<PositionComponent> _positionPool;
        private EcsPool<RotationComponent> _rotationPool;
        private EcsPool<SpawnTypeComponent> _spawnTypePool;

        public PlayerSpawnerSystem(PlayerData playerData, IDescriptorEntityFactory entityFactory, EntityTargetService entityTargetService, GameObjectResourceManager resourceManager)
        {
            _entityFactory = entityFactory;
            _playerData = playerData;
            _entityTargetService = entityTargetService;
            _resourceManager = resourceManager;
        }

        public async void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            PlayerFactory playerFactory = new(_entityFactory, world, _playerData, _resourceManager);

            _spawnPoints = world.Filter<SpawnTypeComponent>().Inc<PositionComponent>().Inc<RotationComponent>().End();
            _positionPool = world.GetPool<PositionComponent>();
            _rotationPool = world.GetPool<RotationComponent>();
            _spawnTypePool = world.GetPool<SpawnTypeComponent>();

            foreach (int spawnPoint in _spawnPoints)
            {
                SpawnType spawnType = _spawnTypePool.Get(spawnPoint).SpawnType;

                if (spawnType != SpawnType.PLAYER)
                {
                    continue;
                }

                Vector3 position = _positionPool.Get(spawnPoint).Position;
                Quaternion rotation = _rotationPool.Get(spawnPoint).Rotation;
                (int playerEntity, int cameraEntity) = await playerFactory.Build(position, rotation);

                _entityTargetService.SetTarget(cameraEntity, playerEntity);
                _entityTargetService.SetTarget(playerEntity, cameraEntity);
            }
        }
    }
}