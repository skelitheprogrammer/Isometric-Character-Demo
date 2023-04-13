using Common.Input;
using Common.TimeService;
using Common.Utils;
using Reflex;
using Reflex.Scripts;
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
        
            inputStateControl.Enable();
            container.BindInstance(inputStateControl);
            container.BindInstance(gameplayActionsRegistrar);
            container.BindInstance(timeService);
            container.BindInstance(_gameData.PlayerData);
            container.BindInstance(_gameData.PlayerData.MovementData);
            container.BindInstance(_gameData.PlayerData.RotationData);
            container.BindInstance(_gameData.PlayerData.CameraData);
        }
    }
}
