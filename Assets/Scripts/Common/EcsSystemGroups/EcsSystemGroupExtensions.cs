using Leopotam.EcsLite;

namespace Common.EcsSystemGroups
{
    public static partial class EcsSystemGroupExtensions
    {
        public static IEcsSystems AddGroup(this IEcsSystems systems, IEcsSystemGroup systemGroup)
        {
            for (int i = 0; i < systemGroup.Systems.Length; i++)
            {
                IEcsSystem system = systemGroup.Systems[i];

                if (system is IEcsSystemGroup nestedSystemGroup)
                {
                    systems.AddGroup(nestedSystemGroup);
                    continue;
                }
                
                systems.Add(system);
            }

            return systems;
        }
    }
    
}