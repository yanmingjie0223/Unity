
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
    public static string BACKSTAGE_LAYER = "backstage_layer";
    /// <summary>
    /// view底层类型
    /// </summary>
    public static string BOTTOM_LAYER = "bottom_layer";
    /// <summary>
    /// view中层类型
    /// </summary>
    public static string MIDDLE_LAYER = "middle_layer";
    /// <summary>
    /// view上层类型
    /// </summary>
    public static string TOP_LAYER = "top_layer";
    /// <summary>
    /// 弹窗层类型
    /// </summary>
    public static string WINDOW_LAYER = "window_layer";
    /// <summary>
    /// 引导层类型
    /// </summary>
    public static string GUIDE_LAYER = "guide_layer";
    /// <summary>
    /// 最外层类型
    /// </summary>
    public static string MAX_LAYER = "max_layer";
}