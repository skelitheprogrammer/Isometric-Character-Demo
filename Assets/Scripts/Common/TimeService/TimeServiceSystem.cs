using Leopotam.EcsLite;
using UnityEngine;

namespace Common.Utils.Systems
{
    public class TimeServiceSystem : IEcsRunSystem
    {
        private readonly TimeService.TimeService _timeService;

        public TimeServiceSystem(TimeService.TimeService timeService)
        {
            _timeService = timeService;
        }

        public void Run(IEcsSystems systems)
        {
            _timeService.DeltaTime = Time.deltaTime;
        }
    }
}