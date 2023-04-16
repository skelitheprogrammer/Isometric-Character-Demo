using Common.Collision.Systems;
using Common.EcsSystemGroups;
using Common.Input;
using Common.TimeService;
using Common.Utils.Sync;
using Common.Utils.Systems;
using Common.Utils.TargetSystem;
using Content.Input;
using Content.Player;
using Content.Player.PlayerCamera;
using Content.Player.PlayerMovement;
using Content.Player.PlayerSpawn.Systems;
using Content.SpawnPoints.Systems;
using Leopotam.EcsLite;
using Reflex;
using Reflex.Scripts;
using Skillitronic.LeoECSLite.EntityDescriptors.Factory;
using Skillitronic.LeoECSLite.GameObjectResourceManager;
using UnityEngine;

namespace Content.Installers
{
    public class DemoSceneInstaller : Installer
    {
        [SerializeField] private SystemRunner _systemRunner;

        public override void InstallBindings(Container container)
        {
            EcsWorld world = new();
            IEcsSystems systems = new EcsSystems(world);
            
            PlayerData playerData = container.Resolve<PlayerData>();
            TimeService timeService = container.Resolve<TimeService>();
            GameplayActionsRegistrar gameplayActionsRegistrar = container.Resolve<GameplayActionsRegistrar>();
            EntityTargetService entityTargetService = new(world);
            GameObjectResourceManager resourceManager = container.Resolve<GameObjectResourceManager>();
            IDescriptorEntityFactory descriptorEntityFactory = container.Resolve<IDescriptorEntityFactory>();
            
            systems
                .Add(new TimeServiceSystem(timeService))
                .Add(new SpawnPointSceneEntitySpawnerSystem(descriptorEntityFactory))
                .Add(new GameplayInputInitSystem(gameplayActionsRegistrar))
                .Add(new PlayerSpawnerSystem(playerData, descriptorEntityFactory, entityTargetService, resourceManager))
                .AddGroup(new PlayerModule(timeService))
                .AddGroup(new SyncModule())
                .Add(new SyncCollisionsSystem())
#if UNITY_EDITOR
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
#endif
                ;

            container.BindInstance(world);
            container.BindInstanceAs(systems, typeof(IEcsSystems));

            container.Instantiate(_systemRunner);
        }
    }
}