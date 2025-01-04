using System;
using UnityEngine;
using YooAsset;
using Object = UnityEngine.Object;

public class ResManager : Singleton<ResManager>
{

    public T GetAssetSync<T>(string pkgName, GroupType groupType, string resName) where T : Object
    {
        var package = YooAssets.TryGetPackage(pkgName);
        if (package == null)
        {
            Debug.LogError("not download package!");
        }

        string groupName = PathUtils.GetGroupName(groupType);
        var handler = package.LoadAssetSync($"{groupName}_{resName}");
        var obj = handler.AssetObject;
        handler.Release();
        return obj as T;
    }

    public Object GetAssetSync(string pkgName, GroupType groupType, string resName)
    {
        var package = YooAssets.TryGetPackage(pkgName);
        if (package == null)
        {
            Debug.LogError("not download package!");
        }

        string groupName = PathUtils.GetGroupName(groupType);
        string[] resArr = resName.Split("/");
        if (resArr.Length <= 0)
        {
            return null;
        }

        string rName = resArr[resArr.Length - 1];
        var handler = package.LoadAssetSync($"{groupName}_{rName}");
        var obj = handler.AssetObject;
        handler.Release();

        return obj;
    }

    public void GetAssetAsync<T>(
        string pkgName,
        GroupType groupType,
        string resName,
        Action<string> start,
        Action<float> progress,
        Action<bool, T> end
    ) where T : Object
    {
        string groupName = PathUtils.GetGroupName(groupType);
        LoadManager.GetInstance().Load(pkgName, $"{groupName}_{resName}", start, progress, (bool isError) =>
        {
            if (!isError)
            {
                var asset = GetAssetSync<T>(pkgName, groupType, resName);
                end?.Invoke(isError, asset);
            }
            else
            {
                end?.Invoke(isError, null);
            }
        });
    }

    public void GetFpkgAssetAsync<T>(
        string pkgName,
        GroupType groupType,
        string fpkgName,
        Action<string> start,
        Action<float> progress,
        Action<bool, T> end
    ) where T : Object
    {
        string groupName = PathUtils.GetGroupName(groupType);
        string bResName = $"{fpkgName}_fui";
        LoadManager.GetInstance().Load(pkgName, $"{groupName}_{bResName}", start, progress, (bool isError) =>
        {
            if (!isError)
            {
                var asset = GetAssetSync<T>(pkgName, groupType, bResName);
                string tResName = $"{fpkgName}_atlas0";
                LoadManager.GetInstance().Load(pkgName, $"{groupName}_{tResName}", start, progress, (bool isError) =>
                {
                    if (!isError)
                    {
                        var asset = GetAssetSync<T>(pkgName, groupType, tResName);
                        end?.Invoke(isError, asset);
                    }
                    else
                    {
                        end?.Invoke(isError, null);
                    }
                });
            }
            else
            {
                end?.Invoke(isError, null);
            }
        });
    }

    public void Release()
    {
        // todo
    }

}
