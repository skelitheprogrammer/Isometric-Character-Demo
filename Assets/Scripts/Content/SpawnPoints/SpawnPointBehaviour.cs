using Common.Utils;
using UnityEngine;

namespace Content.SpawnPoints
{
    [RequireComponent(typeof(EntityReferenceHolder))]
    public class SpawnPointBehaviour : MonoBehaviour
    {
        [field: SerializeField] public SpawnType SpawnType { get; private set; }
        public Vector3 Position => transform.position;
        public Quaternion Rotation => transform.rotation;

        private void OnDrawGizmos()
        {
            Vector3 position = transform.position;
            
            Gizmos.color = Color.green;
            Gizmos.DrawRay(position, Vector3.up);
            Gizmos.DrawRay(position + Vector3.up, transform.forward);
        }
    }
}