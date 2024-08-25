
public enum ViewType
{
    VIEW,
    WINDOW,
    X_WINDOW
}

public enum AreaType
{
    FULL,
    SAFE,
}

public enum ViewShowType
{
    /// <summary>
    /// 显示在单个弹窗中，该弹窗会逐个弹出
    /// </summary>
    SINGLETON_VIEW,
    /// <summary>
    /// 多个界面堆积显示，默认是该显示类型
    /// </summary>
    MULTI_VIEW
}

public enum ViewStatus
{
    /// <summary>
    /// 关闭状态
    /// </summary>
    CLOSE,
    /// <summary>
    /// 显示展开中
    /// </summary>
    SHOWING,
    /// <summary>
    /// 完全显示展开
    /// </summary>
    SHOW,
}

public class ViewLayerType
{
    /// <summary>
    /// 后台层
    /// </summary>
    public static string BACKSTAGE_LAYER = "Backstage_Layer";
    /// <summary>
    /// view底层类型
    /// </summary>
    public static string BOTTOM_LAYER = "Bottom_Layer";
    /// <summary>
    /// view中层类型
    /// </summary>
    public static string MIDDLE_LAYER = "Middle_Layer";
    /// <summary>
    /// view上层类型
    /// </summary>
    public static string TOP_LAYER = "Top_Layer";
    /// <summary>
    /// 弹窗层类型
    /// </summary>
    public static string WINDOW_LAYER = "Window_Layer";
    /// <summary>
    /// 引导层类型
    /// </summary>
    public static string GUIDE_LAYER = "Guide_Layer";
    /// <summary>
    /// 最外层类型
    /// </summary>
    public static string MAX_LAYER = "Max_Layer";
}