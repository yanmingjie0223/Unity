using System;
using System.Collections.Generic;
using YooAsset;
using Object = UnityEngine.Object;

public class ResManager : Singleton<ResManager>
{

    public T GetAssetSync<T>(string pkgName, GroupType groupType, string resName) where T : Object
    {
        return LoadManager.GetInstance().LoadSync<T>(pkgName, groupType, resName);
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

    public void Release(string pkgName, string groupAndResName)
    {
        var package = YooAssets.TryGetPackage(pkgName);
        if (package == null)
        {
            return;
        }

        var loadManager = LoadManager.GetInstance();
        loadManager.resDic.TryGetValue(groupAndResName, out AssetHandle handle);
        if (handle != null)
        {
            // 检测是否有依赖关系，如果有依赖关系，则不释放资源
            ResourceConfig.groupData.TryGetValue("relyon", out var relyonList);
            if (relyonList != null)
            {
                string resName = groupAndResName.Replace($"{GroupTypeName.UI}_", "");
                if (relyonList.Contains(resName))
                {
                    return;
                }
            }
            // 检测引用计数
            // 当引用计数大于等于1时，减少引用计数
            // 当引用计数为1或者小于1时，释放资源并尝试卸载未使用的资源
            if (loadManager.referenceCountDic.TryGetValue(groupAndResName, out int refCount) && refCount >= 1)
            {
                if (refCount > 1)
                {
                    loadManager.referenceCountDic[groupAndResName] = refCount - 1;
                }
                else
                {
                    loadManager.referenceCountDic.Remove(groupAndResName);
                    loadManager.resDic.Remove(groupAndResName);
                    handle.Release();
                    package.TryUnloadUnusedAsset(groupAndResName);
                }
            }
            else
            {
                loadManager.referenceCountDic.Remove(groupAndResName);
                loadManager.resDic.Remove(groupAndResName);
                handle.Release();
                package.TryUnloadUnusedAsset(groupAndResName);
            }
        }
    }

}
