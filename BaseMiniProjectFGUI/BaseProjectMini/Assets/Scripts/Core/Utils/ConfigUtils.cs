public class ConfigUtils
{

    /// <summary>
    /// 获取int的global数据
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static int GetGlobalInt(string key)
    {
        var tables = ConfigManager.GetInstance().tables;
        tables.TbGlobal.DataMap.TryGetValue(key, out var value);
        if (value == null)
        {
            return 0;
        }
        else
        {
            return int.Parse(value.Content);
        }
    }

    /// <summary>
    /// 获取float的global数据
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static float GetGlobalFloat(string key)
    {
        var tables = ConfigManager.GetInstance().tables;
        tables.TbGlobal.DataMap.TryGetValue(key, out var value);
        if (value == null)
        {
            return 0;
        }
        else
        {
            return float.Parse(value.Content);
        }
    }

    /// <summary>
    /// 获取string的global数据
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string GetGlobalString(string key)
    {
        var tables = ConfigManager.GetInstance().tables;
        tables.TbGlobal.DataMap.TryGetValue(key, out var value);
        if (value == null)
        {
            return "";
        }
        else
        {
            return value.Content;
        }
    }

}
