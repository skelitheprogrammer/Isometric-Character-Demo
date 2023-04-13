using Common.Utils.TargetSystem.Components;
using Content.Cameras;
using Content.Player.Components;
using Content.Player.PlayerCamera.Components;
using Content.Unit.Components;
using Skillitronic.LeoECSLite.EntityDescriptors;
using Skillitronic.LeoECSLite.EntityDescriptors.ComponentProviders;

namespace Content.Player.PlayerCamera
{
    public class PlayerCameraEntityDescriptor : IEntityDescriptor
    {
        private static readonly IComponentProvider[] providers =
        {
            new EntityDescriptorExtender<CameraEntityDescriptor>(),
            new ComponentProvider<PlayerCameraTag>(),
            new ComponentProvider<CameraDataComponent>(),
            new ComponentProvider<UnitVelocity>(),
        };

        public IComponentProvider[] Components => providers;
    }
}