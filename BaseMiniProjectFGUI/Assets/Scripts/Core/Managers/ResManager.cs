﻿using System;
using System.Collections.Generic;
using UnityEngine;
using YooAsset;
using Object = UnityEngine.Object;

public class ResManager : Singleton<ResManager>
{

    public T GetAssetSync<T>(string pkgName, GroupType groupType, string resName) where T : Object
    {
        return LoadManager.GetInstance().LoadSync<T>(pkgName, groupType, resName);
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
        LoadManager.GetInstance().Load(pkgName, $"{groupName}_{resName}", start, progress, (bool isError, Object assetObject) =>
        {
            if (!isError)
            {
                end?.Invoke(isError, assetObject as T);
            }
            else
            {
                end?.Invoke(isError, null);
            }
        });
    }

    public void GetResAssetAsync(
        string pkgName,
        List<string> resConfigNames,
        Action<string> start,
        Action<float> progress,
        Action<bool> end
    )
    {
        List<string> resList = new();
        resConfigNames.ForEach(name =>
        {
            ResourceConfig.groupData.TryGetValue(name, out var curResList);
            if (curResList != null)
            {
                for (int i = 0; i < curResList.Count; i++)
                {
                    resList.Add(curResList[i]);
                }
            }
        });
        if (resList.Count <= 0)
        {
            end?.Invoke(false);
        }
        else
        {
            start?.Invoke("loading start");

            var completeCount = 0;
            var maxCount = resList.Count;
            var isErr = false;
            for (int i = 0; i < resList.Count; i++)
            {
                if (isErr)
                {
                    break;
                }

                var bResName = resList[i];
                LoadManager.GetInstance().Load(pkgName, $"{GroupTypeName.UI}_{bResName}", null, null, (bool isError, Object assetObject) =>
                {
                    if (isError)
                    {
                        isErr = true;
                        end?.Invoke(true);
                    }
                    else
                    {
                        completeCount++;
                        progress?.Invoke((float)completeCount / maxCount);
                        if (completeCount == maxCount)
                        {
                            end?.Invoke(false);
                        }
                    }
                });
            }
        }
    }

    public void Release(string pkgName, string resName)
    {
        var package = YooAssets.TryGetPackage(pkgName);
        if (package == null)
        {
            return;
        }

        var loadManager = LoadManager.GetInstance();
        loadManager.resDic.TryGetValue(resName, out AssetHandle handle);
        if (handle != null)
        {
            handle.Release();
            package.TryUnloadUnusedAsset(resName);
        }
    }

    public void Relase(string pkgName, string groupName, string resName)
    {
        var package = YooAssets.TryGetPackage(pkgName);
        if (package == null)
        {
            return;
        }

        var loadManager = LoadManager.GetInstance();
        loadManager.groupResDic.TryGetValue(groupName, out Dictionary<string, AssetHandle> curResDic);
        if (curResDic != null)
        {
            string resKey = $"{groupName}_{resName}";
            curResDic.TryGetValue(resKey, out AssetHandle handle);
            if (handle != null)
            {
                handle.Release();
                package.TryUnloadUnusedAsset(resKey);
            }
        }
    }

}
