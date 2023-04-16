using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Skillitronic.LeoECSLite.GameObjectResourceManager.Factory
{
    public sealed class GameObjectFactory : IGameObjectFactory
    {
        public async Task<GameObject> Create(string reference)
        {
            Task<GameObject> load = Addressables.LoadAssetAsync<GameObject>(reference).Task;

            await load;

            GameObject result = Object.Instantiate(load.Result);

            return result;
        }
    }
}