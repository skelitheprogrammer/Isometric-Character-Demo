using Common.EcsSystemGroups;
using Common.Input;
using Common.TimeService;
using Common.Utils.Sync;
using Common.Utils.Systems;
using Common.Utils.TargetSystem;
using Content.Input;
using Content.Player;
using Content.Player.PlayerCamera;
using Content.Player.PlayerCamera.CameraTargeting;
using Content.Player.PlayerMovement;
using Content.Player.PlayerSpawn.Systems;
using Content.SpawnPoints.Systems;
using Leopotam.EcsLite;
using Reflex;
using Reflex.Scripts;
using Skillitronic.LeoECSLite.EntityDescriptors.Factory;
using UnityEngine;

namespace Content.Installers
{
    public class SceneInstaller : Installer
    {
        [SerializeField] private SystemRunner _systemRunner;

        public override void InstallBindings(Container container)
        {
            EcsWorld world = new();
            EcsWorld eventsWorld = new();
            IEcsSystems systems = new EcsSystems(world);

            IDescriptorEntityFactory descriptorEntityFactory = new DescriptorEntityFactory();
            PlayerData playerData = container.Resolve<PlayerData>();
            TimeService timeService = container.Resolve<TimeService>();
            GameplayActionsRegistrar gameplayActionsRegistrar = container.Resolve<GameplayActionsRegistrar>();
            PlayerMovementData movementData = container.Resolve<PlayerMovementData>();
            PlayerRotationData rotationData = container.Resolve<PlayerRotationData>();
            PlayerCameraData cameraData = container.Resolve<PlayerCameraData>();
            EntityTargetService entityTargetService = new(world);

            systems
                .AddWorld(eventsWorld, "events")
                .Add(new TimeServiceSystem(timeService))
                .Add(new SpawnPointGatherSystem(descriptorEntityFactory))
                .Add(new GameplayInputInitSystem(gameplayActionsRegistrar))
                .Add(new PlayerSpawnerSystem(playerData, descriptorEntityFactory, cameraData, entityTargetService))
                .AddGroup(new PlayerCameraModule())
                .AddGroup(new PlayerModule(timeService, movementData, rotationData))
                .AddGroup(new SyncModule())

#if UNITY_EDITOR
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem("events"))
#endif
                ;

            container.BindInstance(world);
            container.BindInstanceAs(systems, typeof(IEcsSystems));

            container.Instantiate(_systemRunner);
        }
    }
}