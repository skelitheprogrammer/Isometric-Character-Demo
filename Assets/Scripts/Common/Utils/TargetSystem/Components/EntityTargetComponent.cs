using Leopotam.EcsLite;

namespace Common.Utils.TargetSystem.Components
{
    [System.Serializable]
    public struct EntityTargetComponent
    {
        public EcsPackedEntity LinkedEntity;
    }
}