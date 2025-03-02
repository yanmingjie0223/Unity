public class GameConfig
{

    /// <summary>
    /// yoo包名字 改字段毕竟不变
    /// </summary>
    public static readonly string yooPackageName = "DynamicAssets";
    /// <summary>
    /// 当前yoo资源加载类型
    /// </summary>
#if UNITY_EDITOR
    public static ServerType serverType = ServerType.Local;
#else
    public static ServerType serverType = ServerType.Web;
#endif

    /// <summary>
    /// view window背后黑色透明层
    /// </summary>
    public static readonly string matteUrl = "ui://preload/wBg";
    /// <summary>
    /// 屏幕宽度
    /// </summary>
    public static readonly int initWidth = 750;
    /// <summary>
    /// 屏幕高度
    /// </summary>
    public static readonly int initHeight = 1334;

    /// <summary>
    /// http请求根地址
    /// </summary>
#if UNITY_EDITOR
    public static string httpRoot = "http://127.0.0.1:3080";
#else
    public static string httpRoot = "";
#endif

}
