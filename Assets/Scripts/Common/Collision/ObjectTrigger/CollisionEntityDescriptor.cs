using Common.Collision.Components;
using Common.Utils;
using Skillitronic.LeoECSLite.EntityDescriptors;
using Skillitronic.LeoECSLite.EntityDescriptors.ComponentProviders;

namespace Common.Collision.ObjectTrigger
{
    public class CollisionEntityDescriptor : IEntityDescriptor
    {
        private static readonly IComponentProvider[] providers =
        {
            new ComponentProvider<GameObjectReferenceComponent>(),
            new ComponentProvider<CollisionComponent>(),
        };

        public IComponentProvider[] Components => providers;
    }
}