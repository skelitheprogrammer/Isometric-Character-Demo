using Common.Utils.Components;
using Content.Player.Components;
using Content.Player.PlayerMovement.Components;
using Content.Unit;
using Content.Unit.Components;
using Skillitronic.LeoECSLite.EntityDescriptors;
using Skillitronic.LeoECSLite.EntityDescriptors.ComponentProviders;

namespace Content.Player
{
    public class PlayerEntityDescriptor : IEntityDescriptor
    {
        private static readonly IComponentProvider[] providers =
        {
            new ComponentProvider<PlayerTag>(),
            new ComponentProvider<UnitVelocity>(),
            new ComponentProvider<PositionComponent>(),
            new ComponentProvider<RotationComponent>(),
            new ComponentProvider<DirectionComponent>(),
            new ComponentProvider<MoveInputComponent>(),
            new EntityDescriptorExtender<UnitEntityDescriptor>(),
        };

        public IComponentProvider[] Components => providers;
    }
}