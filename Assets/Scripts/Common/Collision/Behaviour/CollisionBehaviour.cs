using System;
using Common.Utils;
using UnityEngine;

namespace Common.Collision.Behaviour
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(EntityReferenceHolder))]
    public abstract class CollisionBehaviour : MonoBehaviour
    {
        protected EntityReferenceHolder EntityReference { get; private set; }
        protected Action<int, CollisionData> _onCollision;
    
    
        public int Entity => EntityReference.Entity;

        private void Awake()
        {
            EntityReference = GetComponent<EntityReferenceHolder>();
        }
    
        public void Register(Action<int, CollisionData> onTrigger)
        {
            _onCollision = onTrigger;
        }
    }
}