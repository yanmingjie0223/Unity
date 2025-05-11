using UnityEngine;

/// <summary>
/// ���� ����������פ�ڵ㲢�����ڳ�פ�ڵ��ϵ�
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
