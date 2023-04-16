using Common.EcsSystemGroups;
using Common.TimeService;
using Content.Player.PlayerCamera;
using Content.Player.PlayerMovement;
using Leopotam.EcsLite;

namespace Content.Player
{
    public class PlayerModule : IEcsSystemGroup
    {
        public IEcsSystem[] Systems { get; }

        public PlayerModule(TimeService timeService)
        {
            Systems = new IEcsSystem[]
            {
                new PlayerMovementSystemGroup(timeService),
                new PlayerCameraModule()
            };
        }
    }
}