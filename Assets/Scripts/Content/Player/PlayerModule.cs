using Common.EcsSystemGroups;
using Common.TimeService;
using Content.Player.PlayerMovement;
using Leopotam.EcsLite;

namespace Content.Player
{
    public class PlayerModule : IEcsSystemGroup
    {
        public IEcsSystem[] Systems { get; }

        public PlayerModule(TimeService timeService, PlayerMovementData movementData, PlayerRotationData rotationData)
        {
            Systems = new IEcsSystem[]
            {
                new PlayerMovementSystemGroup(timeService, movementData, rotationData),
            };
        }
    }
}