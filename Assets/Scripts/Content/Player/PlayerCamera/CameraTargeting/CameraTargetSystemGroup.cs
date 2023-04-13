using Common.EcsSystemGroups;
using Content.Player.PlayerCamera.CameraTargeting.Systems;
using Leopotam.EcsLite;

namespace Content.Player.PlayerCamera.CameraTargeting
{
    public class CameraTargetSystemGroup : IEcsSystemGroup
    {
        public IEcsSystem[] Systems { get; }
        
        public CameraTargetSystemGroup()
        {
            Systems = new IEcsSystem[]
            {
                new CameraTargetedMoveSystem()
            };
        }
    }
}