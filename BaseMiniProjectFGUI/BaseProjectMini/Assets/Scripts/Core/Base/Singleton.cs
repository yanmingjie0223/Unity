public abstract class Singleton<T> where T : class, new()
{
    private static readonly object lockObject = new();
    public static T _instance;

    public static T GetInstance()
    {
        if (_instance == null)
        {
            lock (lockObject)
            {
                _instance ??= new T();
            }
        }
        return _instance;
    }

    public static void DeleteInstance()
    {
        if (_instance != null)
        {
            _instance = null;
        }
    }
}
