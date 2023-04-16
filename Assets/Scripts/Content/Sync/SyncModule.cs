using Common.EcsSystemGroups;
using Common.Utils.Sync.Systems;
using Leopotam.EcsLite;

namespace Common.Utils.Sync
{
    public class SyncModule : IEcsSystemGroup
    {
        public IEcsSystem[] Systems { get; }

        public SyncModule()
        {
            Systems = new IEcsSystem[]
            {
                new DirectionRotationSyncSystem(),
                new PositionSyncSystem(),
                new RotationSyncSystem(),
            };
        }
    }
}