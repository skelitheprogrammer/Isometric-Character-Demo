using Common.TimeService;
using Common.Utils.Components;
using Common.Utils.TargetSystem.Components;
using Content.Player.Components;
using Content.Player.PlayerMovement.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Content.Player.PlayerMovement.Systems
{
    public class PlayerRotationSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly TimeService _timeService;

        private EcsWorld _world;
        private EcsFilter _playerFilter;
        private EcsPool<RotationComponent> _rotationPool;
        private EcsPool<EntityTargetComponent> _targetPool;
        private EcsPool<MoveInputComponent> _moveInputPool;
        private EcsPool<DirectionComponent> _directionPool;
        private EcsPool<PlayerRotationData> _rotationDataPool;

        public PlayerRotationSystem(TimeService timeService)
        {
            _timeService = timeService;
        }

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _playerFilter = _world.Filter<PlayerTag>().Inc<RotationComponent>().Inc<EntityTargetComponent>().Inc<PlayerRotationData>().Exc<IgnoreEntityTarget>().End();
            _rotationPool = _world.GetPool<RotationComponent>();
            _targetPool = _world.GetPool<EntityTargetComponent>();
            _directionPool = _world.GetPool<DirectionComponent>();
            _moveInputPool = _world.GetPool<MoveInputComponent>();
            _rotationDataPool = _world.GetPool<PlayerRotationData>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int i in _playerFilter)
            {
                if (!_targetPool.Get(i).LinkedEntity.Unpack(_world, out int targetEntity))
                {
                    continue;
                }

                float deltaTime = _timeService.DeltaTime;
                float rotationSpeed = _rotationDataPool.Get(i).RotationSpeed;

                ref Quaternion rotation = ref _rotationPool.Get(i).Rotation;

                Vector2 moveInput = _moveInputPool.Get(i).MoveInput;

                if (moveInput.magnitude == 0)
                {
                    continue;
                }

                DirectionComponent targetDirection = _directionPool.Get(targetEntity);
                Vector3 targetRotation = targetDirection.RightDirection * moveInput.x + targetDirection.ForwardDirection * moveInput.y;
                targetRotation.y = 0;

                Quaternion lookRotation = Quaternion.LookRotation(targetRotation);
                rotation = Quaternion.Slerp(rotation, lookRotation, deltaTime * rotationSpeed);
            }
        }
    }
}