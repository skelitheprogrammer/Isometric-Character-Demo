using System.Threading.Tasks;
using Common.Utils.Components;
using Content.Player.PlayerCamera;
using Content.Player.PlayerCamera.Components;
using Content.Unit.Components;
using Leopotam.EcsLite;
using Skillitronic.LeoECSLite.EntityDescriptors;
using Skillitronic.LeoECSLite.EntityDescriptors.Factory;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AI;

namespace Content.Player.PlayerSpawn
{
    public class PlayerFactory
    {
        private readonly IDescriptorEntityFactory _entityFactory;
        private readonly EcsWorld _world;

        private readonly PlayerData _playerData;
        private readonly PlayerCameraData _playerCameraData;

        public PlayerFactory(IDescriptorEntityFactory entityFactory, EcsWorld world, PlayerData playerData, PlayerCameraData playerCameraData)
        {
            _entityFactory = entityFactory;
            _world = world;
            _playerData = playerData;
            _playerCameraData = playerCameraData;
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

                return playerInit.Entity;
            }

            async Task<GameObject> CreatePlayerModel(AssetReference reference)
            {
                GameObject gameObject = await Addressables.InstantiateAsync(reference).Task;
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

                cameraInit.InitComponent(new CameraDataComponent
                {
                    CameraOffset = initialCameraPosition,
                    CameraTravelSpeed = _playerCameraData.CameraSpeed,
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
                return await Addressables.InstantiateAsync(reference).Task;
            }

            return cameraEntity;
        }
    }
}