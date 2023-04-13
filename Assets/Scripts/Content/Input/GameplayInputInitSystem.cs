using Common.Input;
using Content.Player.Components;
using Content.Player.PlayerMovement.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Content.Input
{
    public class GameplayInputInitSystem : IEcsInitSystem, IEcsDestroySystem
    {
        private readonly GameplayActionsRegistrar _registrar;
        private EcsFilter _playerFilter;
        private EcsPool<MoveInputComponent> _inputPool;
        
        public GameplayInputInitSystem(GameplayActionsRegistrar registrar)
        {
            _registrar = registrar;
        }

        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            _playerFilter = world.Filter<PlayerTag>().Inc<MoveInputComponent>().End();
            _inputPool = world.GetPool<MoveInputComponent>();

            _registrar.Move += OnMove;

        }

        public void Destroy(IEcsSystems systems)
        {
            _registrar.Move -= OnMove;
        }

        private void OnMove(Vector2 input)
        {
            foreach (int i in _playerFilter)
            {
                _inputPool.Get(i).MoveInput = input;
            }
        }
    }
}