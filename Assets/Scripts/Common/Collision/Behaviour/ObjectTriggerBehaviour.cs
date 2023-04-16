using Common.Utils;
using UnityEngine;

namespace Common.Collision.Behaviour
{
    public sealed class ObjectTriggerBehaviour : CollisionBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (!other.attachedRigidbody)
            {
                return;
            }

            if (other.TryGetComponent(out EntityReferenceHolder entityReferenceHolder))
            {
                _onCollision.Invoke(EntityReference.Entity, new CollisionData(entityReferenceHolder.Entity, true));
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.attachedRigidbody)
            {
                return;
            }

            if (other.TryGetComponent(out EntityReferenceHolder entityReferenceHolder))
            {
                _onCollision.Invoke(EntityReference.Entity, new CollisionData(entityReferenceHolder.Entity, false));
            }
        }
    
    }
}