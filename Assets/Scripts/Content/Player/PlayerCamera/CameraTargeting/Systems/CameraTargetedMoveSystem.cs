using Common.Utils.Components;
using Common.Utils.TargetSystem.Components;
using Content.Cameras.Components;
using Content.Player.Components;
using Content.Player.PlayerCamera.Components;
using Content.Unit.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Content.Player.PlayerCamera.CameraTargeting.Systems
{
    public class CameraTargetedMoveSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;

        private EcsFilter _cameraFilter;

        private EcsPool<PositionComponent> _positionPool;
        private EcsPool<EntityTargetComponent> _targetPool;
        private EcsPool<CameraDataComponent> _cameraDataPool;
        private EcsPool<UnitVelocity> _unitVelocityPool;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _cameraFilter = _world.Filter<PlayerCameraTag>().Inc<CameraTag>().Inc<UnitVelocity>().Inc<EntityTargetComponent>().Exc<IgnoreEntityTarget>().End();
            _targetPool = _world.GetPool<EntityTargetComponent>();
            _cameraDataPool = _world.GetPool<CameraDataComponent>();
            _positionPool = _world.GetPool<PositionComponent>();
            _unitVelocityPool = _world.GetPool<UnitVelocity>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int i in _cameraFilter)
            {
                if (!_targetPool.Get(i).LinkedEntity.Unpack(_world, out int targetEntity))
                {
                    continue;
                }

                ref Vector3 position = ref _positionPool.Get(i).Position;

                CameraDataComponent cameraDataComponent = _cameraDataPool.Get(i);
                Vector3 cameraOffset = cameraDataComponent.CameraOffset;
                float cameraSpeed = cameraDataComponent.CameraTravelSpeed;
                
                ref Vector3 cameraVelocity = ref _unitVelocityPool.Get(i).Velocity;

                Vector3 targetPosition = _positionPool.Get(targetEntity).Position + cameraOffset;

                position = Vector3.SmoothDamp(position, targetPosition, ref cameraVelocity, cameraSpeed);
            }
        }
    }
}