using Common.EcsSystemGroups;
using Common.TimeService;
using Content.Player.PlayerMovement.Systems;
using Leopotam.EcsLite;

namespace Content.Player.PlayerMovement
{
    public class PlayerMovementSystemGroup : IEcsSystemGroup
    {
        public IEcsSystem[] Systems { get; }

        public PlayerMovementSystemGroup(TimeService timeService, PlayerMovementData movementData, PlayerRotationData rotationData)
        {
            Systems = new IEcsSystem[]
            {
                new PlayerVelocityApplierSystem(movementData),
                new PlayerMoveSystem(timeService),
                new PlayerRotationSystem(timeService, rotationData),
            };
        }
    }
}