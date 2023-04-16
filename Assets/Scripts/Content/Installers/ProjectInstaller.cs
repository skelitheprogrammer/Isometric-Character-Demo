using Common.Input;
using Common.TimeService;
using Common.Utils;
using Reflex;
using Reflex.Scripts;
using Skillitronic.LeoECSLite.EntityDescriptors.Factory;
using Skillitronic.LeoECSLite.GameObjectResourceManager;
using Skillitronic.LeoECSLite.GameObjectResourceManager.Factory;
using UnityEngine;

namespace Content.Installers
{
    public class ProjectInstaller : Installer
    {
        [SerializeField] private GameData _gameData; 
    
        public override void InstallBindings(Container container)
        {
            PlayerActions playerActions = new();
            GameplayActionsRegistrar gameplayActionsRegistrar = new(playerActions.Gameplay);
            InputStateControlService inputStateControl = new(playerActions);
            TimeService timeService = new();
            IGameObjectFactory factory = new GameObjectFactory();
            GameObjectResourceManager resourceManager = new(factory);
            
            inputStateControl.Enable();
            container.BindInstance(resourceManager);
            container.BindInstance(inputStateControl);
            container.BindSingleton<IDescriptorEntityFactory, DescriptorEntityFactory>();
            container.BindInstance(gameplayActionsRegistrar);
            container.BindInstance(timeService);
            container.BindInstance(_gameData.PlayerData);
        }
    }
}
