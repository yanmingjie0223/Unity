using UnityEngine;

/// <summary>
/// 单例 主动创建常驻节点并挂载在常驻节点上的
/// </summary>
/// <typeparam name="T"></typeparam>
public class SingletonDontDestroyMono<T> : MonoBehaviour where T : SingletonDontDestroyMono<T>
{

    private static T instance;

    public static T GetInstance()
    {
        if (instance == null)
        {
            new GameObject($"{typeof(T).Name}").AddComponent<T>();
        }
        return instance;
    }

    public static void DeleteInstance()
    {
        if (instance != null)
        {
            instance = null;
        }
    }

    protected virtual void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this as T;
        DontDestroyOnLoad(gameObject);
    }

}
