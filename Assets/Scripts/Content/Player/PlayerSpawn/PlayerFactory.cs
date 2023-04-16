using System.Threading.Tasks;
using Common.Utils;
using Common.Utils.Components;
using Content.Player.PlayerCamera;
using Content.Player.PlayerCamera.Components;
using Content.Unit.Components;
using Leopotam.EcsLite;
using Skillitronic.LeoECSLite.EntityDescriptors;
using Skillitronic.LeoECSLite.EntityDescriptors.Factory;
using Skillitronic.LeoECSLite.GameObjectResourceManager;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AI;

namespace Content.Player.PlayerSpawn
{
    public class PlayerFactory
    {
        private readonly IDescriptorEntityFactory _entityFactory;
        private readonly GameObjectResourceManager _resourceManager;
        private readonly EcsWorld _world;

        private readonly PlayerData _playerData;

        public PlayerFactory(IDescriptorEntityFactory entityFactory, EcsWorld world, PlayerData playerData, GameObjectResourceManager resourceManager)
        {
            _entityFactory = entityFactory;
            _world = world;
            _playerData = playerData;
            _resourceManager = resourceManager;
        }

        public async Task<(int playerEntity, int cameraEntity)> Build(Vector3 playerPosition, Quaternion playerRotation)
        {
            Task<int> playerTask = CreatePlayer(playerPosition, playerRotation);
            Task<int> cameraTask = CreateCamera(playerPosition);

            await Task.WhenAll(playerTask, cameraTask);
            return (playerTask.Result, cameraTask.Result);
        }

        private async Task<int> CreatePlayer(Vector3 position, Quaternion rotation)
        {
            GameObject playerModel = await CreatePlayerModel(_playerData.PlayerPrefab);
            int playerEntity = CreatePlayerEntity();

            int CreatePlayerEntity()
            {
                EntityInitializer playerInit = _entityFactory.Create<PlayerEntityDescriptor>(_world);

                playerInit.InitComponent(new AnimatorComponent()
                {
                    Animator = playerModel.GetComponent<Animator>(),
                });

                playerInit.InitComponent(new NavMeshComponent()
                {
                    Agent = playerModel.GetComponent<NavMeshAgent>(),
                });
                
                playerInit.InitComponent(new PositionComponent
                {
                    Position = position,
                });

                playerInit.InitComponent(new RotationComponent
                {
                    Rotation = rotation,
                });
                
                playerInit.InitComponent(new GameObjectReferenceComponent
                {
                    GameObject = playerModel,
                });
                
                playerInit.InitComponent(_playerData.RotationData);
                
                playerInit.InitComponent(_playerData.MovementData);

                return playerInit.Entity;
            }

            async Task<GameObject> CreatePlayerModel(AssetReference reference)
            {
                GameObject gameObject = await _resourceManager.Build(reference.GetAddressFromAssetReference());
                gameObject.transform.SetPositionAndRotation(position, rotation);

                return gameObject;
            }

            return playerEntity;
        }

        private async Task<int> CreateCamera(Vector3 playerPosition)
        {
            GameObject cameraModel = await CreateCameraModel(_playerData.CameraPrefab);
            
            Vector3 initialCameraPosition = cameraModel.transform.position;
            Vector3 finalPosition = initialCameraPosition + playerPosition;
            cameraModel.transform.position = finalPosition;

            int cameraEntity = CreateCameraEntity();

            int CreateCameraEntity()
            {
                EntityInitializer cameraInit = _entityFactory.Create<PlayerCameraEntityDescriptor>(_world);

                cameraInit.InitComponent(new PlayerCameraData
                {
                    CameraOffset = initialCameraPosition,
                    CameraTravelSpeed = _playerData.PlayerCameraData.CameraTravelSpeed,
                });

                cameraInit.InitComponent(new PositionComponent
                {
                    Position = finalPosition,
                });
                
                cameraInit.InitComponent(new RotationComponent
                {
                    Rotation = cameraModel.transform.rotation,
                });

                cameraInit.InitComponent(new GameObjectReferenceComponent
                {
                    GameObject = cameraModel,
                });
                
                return cameraInit.Entity;
            }

            async Task<GameObject> CreateCameraModel(AssetReference reference)
            {
                return await _resourceManager.Build(reference.GetAddressFromAssetReference());
            }

            return cameraEntity;
        }
    }
}