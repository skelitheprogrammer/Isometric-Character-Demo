using Common.EcsSystemGroups;
using Content.Player.PlayerCamera.CameraTargeting;
using Leopotam.EcsLite;

namespace Content.Player.PlayerCamera
{
    public class PlayerCameraModule : IEcsSystemGroup
    {
        public IEcsSystem[] Systems { get; }

        public PlayerCameraModule()
        {
            Systems = new IEcsSystem[]
            {
                new CameraTargetSystemGroup(),
            };
        }

    }
}