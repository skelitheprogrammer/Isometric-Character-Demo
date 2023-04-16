using Common.Utils.Components;
using Content.SpawnPoints.Components;
using Skillitronic.LeoECSLite.EntityDescriptors;
using Skillitronic.LeoECSLite.EntityDescriptors.ComponentProviders;

namespace Content.SpawnPoints
{
    public class SpawnPointEntityDescriptor : IEntityDescriptor
    {
        private static readonly IComponentProvider[] providers =
        {
            new ComponentProvider<PositionComponent>(),
            new ComponentProvider<RotationComponent>(),
            new ComponentProvider<SpawnTypeComponent>(),
        };

        public IComponentProvider[] Components => providers;
    }
}