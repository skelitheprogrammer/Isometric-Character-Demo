using Common.Utils.Components;
using Content.Unit.Components;
using Skillitronic.LeoECSLite.EntityDescriptors;
using Skillitronic.LeoECSLite.EntityDescriptors.ComponentProviders;

namespace Content.Unit
{
    public class UnitEntityDescriptor : IEntityDescriptor
    {
        private static readonly IComponentProvider[] providers =
        {
            new ComponentProvider<AnimatorComponent>(),
            new ComponentProvider<NavMeshComponent>(),
            new ComponentProvider<GameObjectReferenceComponent>()
        };

        public IComponentProvider[] Components => providers;
    }
}