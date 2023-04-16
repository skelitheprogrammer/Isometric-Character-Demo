using Common.Utils;

namespace Common.Collision.Behaviour
{
    public sealed class ObjectCollisionBehaviour : CollisionBehaviour
    {
        private void OnCollisionEnter(UnityEngine.Collision other)
        {
            if (!other.collider.attachedRigidbody)
            {
                return;
            }

            if (other.gameObject.TryGetComponent(out EntityReferenceHolder entityReferenceHolder))
            {
                _onCollision.Invoke(EntityReference.Entity, new CollisionData(entityReferenceHolder.Entity, true));
            }
        }

        private void OnCollisionExit(UnityEngine.Collision other)
        {
            if (!other.collider.attachedRigidbody)
            {
                return;
            }

            if (other.gameObject.TryGetComponent(out EntityReferenceHolder entityReferenceHolder))
            {
                _onCollision.Invoke(EntityReference.Entity, new CollisionData(entityReferenceHolder.Entity, false));
            }
        }
    }
}