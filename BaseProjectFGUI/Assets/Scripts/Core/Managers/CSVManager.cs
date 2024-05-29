using System;
using System.Collections.Generic;

public class CSVManager : Singleton<CSVManager>
{
    private readonly Dictionary<string, ICSVConfig> _csvDic = new();

    public void Initialize(Action<bool> end)
    {
        var resManager = ResManager.GetInstance();
        LoadManager.GetInstance().LoadBundle(
            BundleType.Config,
            (string start) => { },
            (float progress) => { },
            (bool isError) =>
            {
                if (!isError)
                {
                    InitializeCSV();
                }
                end?.Invoke(isError);
            }
        );
    }

    public T GetTable<T>() where T : ICSVConfig, new()
    {
        string name = typeof(T).Name;
        _csvDic.TryGetValue(name, out ICSVConfig table);
        return (T)table;
    }

    private void InitializeCSV()
    {
        InitializeCSVTable<TestTable>();
        InitializeCSVTable<LanguageTable>();
    }

    private void InitializeCSVTable<T>() where T : ICSVConfig, new()
    {
        var t = new T();
        t.Load();
        _csvDic[typeof(T).Name] = t;
    }
}
