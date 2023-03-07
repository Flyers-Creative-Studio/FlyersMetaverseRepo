using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

public class GenerateObjectsManager : MonoBehaviour
{
    private void Start()
    {
        AsyncOperationHandle<GameObject> obj = Addressables.LoadAssetAsync<GameObject>("EnvironmentScene");
        obj.Completed += (obj) =>
        {
            Debug.Log(obj.Result.name);
            Instantiate(obj.Result);
        };
        while (obj.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log("time : " + obj.PercentComplete);
        }
    }
}
