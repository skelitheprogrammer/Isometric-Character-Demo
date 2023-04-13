using Common.Utils.Components;
using Content.Cameras.Components;
using Content.Player.PlayerMovement.Components;
using Skillitronic.LeoECSLite.EntityDescriptors;
using Skillitronic.LeoECSLite.EntityDescriptors.ComponentProviders;

namespace Content.Cameras
{
    public class CameraEntityDescriptor : IEntityDescriptor
    {
        private static readonly IComponentProvider[] providers =
        {
            new ComponentProvider<CameraTag>(),
            new ComponentProvider<PositionComponent>(),
            new ComponentProvider<RotationComponent>(),
            new ComponentProvider<DirectionComponent>(),
            new ComponentProvider<GameObjectReferenceComponent>()
        };

        public IComponentProvider[] Components => providers;
    }
}