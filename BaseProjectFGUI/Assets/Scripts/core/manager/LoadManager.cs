using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using Object = UnityEngine.Object;

public class LoadManager : MonoBehaviour
{
    private static LoadManager instance;

    public static LoadManager GetInstance()
    {
        return instance;
    }

    public static void DeleteInstance()
    {
        if (instance != null)
        {
            instance = null!;
        }
    }

    public void LoadBundle(BundleType bundleType, Action<string> start, Action<float> progress, Action<bool> end)
    {
        var resManager = ResManager.GetInstance();
        string bundleName = resManager.GetBundleNmae(bundleType);
        StartCoroutine(LoadRes(bundleName, start, progress, end));
    }

    public void LoadBundle(string bundleName, Action<string> start, Action<float> progress, Action<bool> end)
    {
        StartCoroutine(LoadRes(bundleName, start, progress, end));
    }

    protected void Awake()
    {
        instance = this;
    }

    private IEnumerator LoadRes(string bundleName, Action<string> start, Action<float> progress, Action<bool> end)
    {
        AsyncOperationHandle<IList<IResourceLocation>> handle = Addressables.LoadResourceLocationsAsync(bundleName);
        start?.Invoke("加载资源目录");
        while (handle.PercentComplete < 1 && !handle.IsDone)
        {
            progress?.Invoke(handle.PercentComplete);
            yield return null;
        }

        yield return handle;
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            bool isError = false;
            var length = handle.Result.Count;
            for (int i = 0; i < length; i++)
            {
                var res = handle.Result[i];
                var key = res.PrimaryKey;
                start("加载资源 " + key);
                AsyncOperationHandle<Object> assetHandle = Addressables.LoadAssetAsync<Object>(key);
                yield return assetHandle;
                progress?.Invoke(((float)i + 1) / length);
                if (assetHandle.Status == AsyncOperationStatus.Succeeded)
                {
                    var resManager = ResManager.GetInstance();
                    var resDic = resManager.GetBundleResDic(bundleName);
                    if (!resDic.ContainsKey(key))
                    {
                        resDic.Add(key, assetHandle.Result);
                    }
                }
                else
                {
                    isError = true;
                    break;
                }
            }
            end?.Invoke(isError);
        }
        else
        {
            end?.Invoke(true);
        }
        Addressables.Release(handle);
    }
}
