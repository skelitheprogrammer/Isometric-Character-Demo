using Common.TimeService;
using Common.Utils.Components;
using Content.Player.Components;
using Content.Player.PlayerMovement.Components;
using Content.Unit.Components;
using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.AI;

namespace Content.Player.PlayerMovement.Systems
{
    public class PlayerMoveSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly TimeService _timeService;

        private EcsFilter _playerFilter;
        private EcsPool<NavMeshComponent> _agentPool;
        private EcsPool<UnitVelocity> _velocityPool;
        private EcsPool<PositionComponent> _positionPool;

        public PlayerMoveSystem(TimeService timeService)
        {
            _timeService = timeService;
        }
        
        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            _playerFilter = world.Filter<PlayerTag>().Inc<NavMeshComponent>().Inc<UnitVelocity>().End();
            _agentPool = world.GetPool<NavMeshComponent>();
            _velocityPool = world.GetPool<UnitVelocity>();
            _positionPool = world.GetPool<PositionComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int i in _playerFilter)
            {
                float deltaTime = _timeService.DeltaTime;

                NavMeshAgent agent = _agentPool.Get(i).Agent;

                Vector3 unitVelocity = _velocityPool.Get(i).Velocity;
                
                agent.Move(unitVelocity * deltaTime);
                _positionPool.Get(i).Position = agent.transform.position;
            }
        }
    }
}