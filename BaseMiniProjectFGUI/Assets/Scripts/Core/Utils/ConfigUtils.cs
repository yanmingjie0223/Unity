public class ConfigUtils
{

    /// <summary>
    /// 获取global单例配置
    /// </summary>
    /// <returns></returns>
    public static cfg.ncb.Global GetGlobal()
    {
        var tables = ConfigManager.GetInstance().tables;
        cfg.ncb.Global global = tables.TbGlobal.DataList[0];
        return global;
    }

}
