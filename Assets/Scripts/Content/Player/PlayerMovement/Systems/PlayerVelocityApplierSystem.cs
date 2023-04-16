using Common.Utils.Components;
using Common.Utils.TargetSystem.Components;
using Content.Player.Components;
using Content.Player.PlayerMovement.Components;
using Content.Unit.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Content.Player.PlayerMovement.Systems
{
    public class PlayerVelocityApplierSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;

        private EcsFilter _filter;
        private EcsPool<UnitVelocity> _velocityPool;
        private EcsPool<MoveInputComponent> _moveInputPool;
        private EcsPool<DirectionComponent> _directionPool;
        private EcsPool<EntityTargetComponent> _targetPool;
        private EcsPool<PlayerMovementData> _movementDataPool;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _filter = _world.Filter<PlayerTag>().Inc<UnitVelocity>().Inc<EntityTargetComponent>().Inc<MoveInputComponent>().Inc<PlayerMovementData>().End();
            _targetPool = _world.GetPool<EntityTargetComponent>();
            _velocityPool = _world.GetPool<UnitVelocity>();
            _directionPool = _world.GetPool<DirectionComponent>();
            _moveInputPool = _world.GetPool<MoveInputComponent>();
            _movementDataPool = _world.GetPool<PlayerMovementData>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int i in _filter)
            {
                if (!_targetPool.Get(i).LinkedEntity.Unpack(_world, out int targetEntity))
                {
                    continue;
                }
                
                float speed = _movementDataPool.Get(i).MoveSpeed;
                DirectionComponent directionComponent = _directionPool.Get(targetEntity);
                Vector2 moveInput = _moveInputPool.Get(i).MoveInput;
                Vector3 currentDirection = directionComponent.RightDirection * moveInput.x + directionComponent.ForwardDirection * moveInput.y;
                
                ref Vector3 velocity = ref _velocityPool.Get(i).Velocity;
                velocity = currentDirection * speed;
            }
        }
    }
}