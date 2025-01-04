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

    public T Register<T>() where T : BaseModel, new()
    {
        var key = typeof(T).Name;
        if (modelDic.ContainsKey(key))
        {
            return (T)modelDic[key];
        }

        if (key != "" && !modelDic.ContainsKey(key))
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

    public void Unregister<T>() where T : BaseModel, new()
    {
        var key = typeof(T).Name;
        if (modelDic[key] != null)
        {
            modelDic[key] = null;
        }
    }

    public T GetModel<T>() where T : BaseModel, new()
    {
        var key = typeof(T).Name;
        if (!modelDic.ContainsKey(key))
        {
            Debug.LogError($"获取model数据源对象,未在ModelManager中注册 key: {key}");
            return null;
        }
        return (T)modelDic[key];
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

    public void Destroy<T>() where T : BaseModel, new()
    {
        var key = typeof(T).Name;
        var model = GetModel<T>();
        if (model != null)
        {
            modelDic[key] = null;
            model.Destroy();
        }
    }

    public bool IsExist<T>() where T : BaseModel, new()
    {
        var key = typeof(T).Name;
        if (!modelDic.ContainsKey(key))
        {
            return false;
        }
        return modelDic[key] != null;
    }
}
