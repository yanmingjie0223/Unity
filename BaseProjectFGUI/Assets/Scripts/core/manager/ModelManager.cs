
using System.Collections.Generic;
using UnityEngine;

public class ModelManager : Singleton<ModelManager>
{
    private readonly Dictionary<string, BaseModel> modelDic = new();

    public void Initialize()
    {
        foreach (var model in modelDic.Values)
        {
            model.Initialize();
        }
    }

    public BaseModel Register<T>(string key) where T : BaseModel, new()
    {
        if (modelDic[key] != null)
        {
            return modelDic[key];
        }

        if (key != "" && modelDic[key] == null)
        {
            modelDic[key] = new T();
        }
        else
        {
            if (key == "")
            {
                Debug.LogError("注册的该model不存在public static readonly key");
            }
            else
            {
                Debug.LogWarning("注册的该model已存在,请使用统一数据源!");
            }
        }

        return null;
    }

    public void Unregister<T>(string key) where T : BaseModel, new()
    {
        if (modelDic[key] != null)
        {
            modelDic[key] = null;
        }
    }

    public BaseModel GetModel(string key)
    {
        if (!modelDic.ContainsKey(key))
        {
            Debug.LogError($"获取model数据源对象,未在ModelManager中注册 key: {key}");
            return null;
        }
        return modelDic[key];
    }

    public void Destroy(string key)
    {
        var model = GetModel(key);
        if (model != null)
        {
            modelDic[key] = null;
            model.Destroy();
        }
    }

    public bool IsExist(string key)
    {
        if (!modelDic.ContainsKey(key))
        {
            return false;
        }
        return modelDic[key] != null;
    }
}
