using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace Skillitronic.LeoECSLite.GameObjectResourceManager
{
    public static class AddressablesUtility
    {
        public static string GetAddressFromAssetReference(this AssetReference reference)
        {
            AsyncOperationHandle<IList<IResourceLocation>> loadResourceLocationsTask = Addressables.LoadResourceLocationsAsync(reference);
        
            IList<IResourceLocation> result = loadResourceLocationsTask.WaitForCompletion();
      
            if (result.Count > 0)
            {
                string key = result[0].PrimaryKey;
                Addressables.Release(loadResourceLocationsTask);
                return key;
            }

            Addressables.Release(loadResourceLocationsTask);
            return string.Empty;
        }
    }
}
