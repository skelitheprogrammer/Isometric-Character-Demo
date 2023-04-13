using Common.Utils.Components;
using Common.Utils.TargetSystem;
using Content.Player.PlayerCamera;
using Content.Player.PlayerCamera.CameraTargeting;
using Content.SpawnPoints;
using Content.SpawnPoints.Components;
using Leopotam.EcsLite;
using Skillitronic.LeoECSLite.EntityDescriptors.Factory;
using UnityEngine;

namespace Content.Player.PlayerSpawn.Systems
{
    public class PlayerSpawnerSystem : IEcsInitSystem
    {
        private readonly PlayerData _playerData;
        private readonly IDescriptorEntityFactory _entityFactory;
        private readonly PlayerCameraData _playerCameraData;
        private readonly EntityTargetService _entityTargetService;

        private PlayerFactory _playerFactory;
        private EcsFilter _spawnPoints;

        private EcsPool<PositionComponent> _positionPool;
        private EcsPool<RotationComponent> _rotationPool;
        private EcsPool<SpawnTypeComponent> _spawnTypePool;

        public PlayerSpawnerSystem(PlayerData playerData, IDescriptorEntityFactory entityFactory, PlayerCameraData playerCameraData, EntityTargetService entityTargetService)
        {
            _entityFactory = entityFactory;
            _playerData = playerData;
            _playerCameraData = playerCameraData;
            _entityTargetService = entityTargetService;
        }

        public async void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            _playerFactory = new PlayerFactory(_entityFactory, world, _playerData, _playerCameraData);

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
                (int playerEntity, int cameraEntity) = await _playerFactory.Build(position, rotation);
                _entityTargetService.SetTarget(cameraEntity,playerEntity);
                _entityTargetService.SetTarget(playerEntity, cameraEntity);
            }
        }
    }
}