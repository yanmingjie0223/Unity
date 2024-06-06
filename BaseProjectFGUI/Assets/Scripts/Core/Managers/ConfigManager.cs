using cfg;
using Luban;
using System;
using System.IO;

public class ConfigManager : Singleton<ConfigManager>
{
    public Tables tables;

    public void Initialize()
    {
        InitializeTables();
    }

    private void InitializeTables()
    {
        var tablesCtor = typeof(Tables).GetConstructors()[0];
        var loaderReturnType = tablesCtor.GetParameters()[0].ParameterType.GetGenericArguments()[1];
        Delegate loader = new Func<string, ByteBuf>(LoadByteBuf);
        tables = (Tables)tablesCtor.Invoke(new object[] { loader });
    }

    private ByteBuf LoadByteBuf(string file)
    {
        return new ByteBuf(File.ReadAllBytes($"Assets/Luban/Datas/{file}.bytes"));
    }
}

