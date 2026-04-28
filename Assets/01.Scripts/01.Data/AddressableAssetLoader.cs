using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressableAssetLoader : MonoBehaviour
{
    [SerializeField] 
    private AssetReference Prefab;

    public void LoadAddressable()
    {
        // Load the asset asynchronously.
        Addressables.LoadAssetAsync<GameObject>(Prefab).Completed += OnLoadDone;
    }

    private void OnLoadDone(AsyncOperationHandle<GameObject> obj)
    {
        // obj.Result contains the loaded asset.
        // You can now instantiate it or use it as needed.
        Instantiate(obj.Result, transform);
    }
}
