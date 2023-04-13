using Leopotam.EcsLite;

namespace Common.EcsSystemGroups
{
    public interface IEcsSystemGroup : IEcsSystem
    {
        IEcsSystem[] Systems { get; }
    }
}