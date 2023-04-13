using Leopotam.EcsLite;
using Reflex.Scripts.Attributes;
using UnityEngine;

namespace Content
{
    public class SystemRunner : MonoBehaviour
    {
        private IEcsSystems _systems;

        [Inject]
        private void Inject(IEcsSystems systems)
        {
            _systems = systems;
        }

        private void Start()
        {
            _systems.Init();
        }

        private void Update()
        {
            _systems.Run();
        }
    }
}
