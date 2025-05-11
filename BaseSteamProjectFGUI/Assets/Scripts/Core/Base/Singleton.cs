public abstract class Singleton<T> where T : class, new()
{
    private static readonly object lockObject = new();
    private static T instance;

    public static T GetInstance()
    {
        if (instance == null)
        {
            lock (lockObject)
            {
                instance ??= new T();
            }
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
}
