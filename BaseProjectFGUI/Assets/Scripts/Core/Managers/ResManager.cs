using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

public class ResManager : Singleton<ResManager>
{

    private readonly Dictionary<string, Dictionary<string, Object>> _bundleDic = new();

    /// <summary>
    /// 获取地址对应的资源对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="bundleType"></param>
    /// <param name="url"></param>
    /// <returns></returns>
    public T GetRes<T>(BundleType bundleType, string url) where T : Object
    {
        var resDic = GetBundleResDic(bundleType);
        if (resDic.TryGetValue(url, out Object obj))
        {
            return (T)obj;
        }
        return null;
    }

    /// <summary>
    /// 通过路径Prefabs/Terrain/下名字类型获取对于的资源
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="bundleType"></param>
    /// <param name="name"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public T GetResByName<T>(BundleType bundleType, string name, ResType type) where T : Object
    {
        var resDic = GetBundleResDic(bundleType);
        var url = GetResUrl(name, type);
        if (resDic.TryGetValue(url, out Object obj))
        {
            return (T)obj;
        }
        return null;
    }

    /// <summary>
    /// 根据名字和类型获取对于资源
    /// </summary>
    /// <param name="name"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public string GetResUrl(string name, ResType type)
    {
        string url = "Assets/DynamicAssets/" + name;
        string exeUrl = type switch
        {
            ResType.PNG => ".png",
            ResType.PREFAB => ".prefab",
            ResType.MP4 => ".mp4",
            _ => ""
        };
        return url + exeUrl;
    }

    public string GetBundleNmae(BundleType bundleType)
    {
        string name = bundleType switch
        {
            BundleType.Main => BundleTypeName.Main,
            BundleType.Preload => BundleTypeName.Preload,
            BundleType.Config => BundleTypeName.Config,
            _ => ""
        };
        return name;
    }

    public Dictionary<string, Object> GetBundleResDic(BundleType type)
    {
        var name = GetBundleNmae(type);
        _bundleDic.TryGetValue(name, out Dictionary<string, Object> dic);
        if (dic == null)
        {
            dic = new Dictionary<string, Object>();
            _bundleDic.Add(name, dic);
        }
        return dic;
    }

    public Dictionary<string, Object> GetBundleResDic(string name)
    {
        _bundleDic.TryGetValue(name, out Dictionary<string, Object> dic);
        if (dic == null)
        {
            dic = new Dictionary<string, Object>();
            _bundleDic.Add(name, dic);
        }
        return dic;
    }

    /// <summary>
    /// 销毁
    /// </summary>
    public void Destroy()
    {
        foreach (var obj in _bundleDic.Values)
        {
            if (obj != null)
            {
                foreach (var rObj in obj.Values)
                {
                    if (rObj is GameObject @robject)
                    {
                        Addressables.ReleaseInstance(@robject);
                    }
                }
            }
        }
        _bundleDic.Clear();
    }

}
