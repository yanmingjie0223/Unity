using UnityEngine;

public class SingletonMono<T> : MonoBehaviour where T : SingletonMono<T>
{
    private static T instance;

    public static T GetInstance()
    {
        return instance;
    }

    public static void DeleteInstance()
    {
        if (instance != null)
        {
            instance = null;
        }
    }

    virtual protected void Awake()
    {
        instance = (T)this;
    }
   
}
