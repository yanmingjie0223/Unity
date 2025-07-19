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
    /// app名字
    /// </summary>
    public static readonly string appName = "MiniGame";

    /// <summary>
    /// cdn跟地址
    /// </summary>
#if UNITY_EDITOR
    public static readonly string cdnRoot = $"{appName}";
    public static readonly string shareUrlRoot = "";
#elif WEIXINMINIGAME
    public static readonly string cdnRoot = $"{appName}";
    public static readonly string shareUrlRoot = $"{cdnRoot}/Wx/Share/";
#elif DOUYINMINIGAME
    public static readonly string cdnRoot = $"{appName}";
    public static readonly string shareUrlRoot = $"{cdnRoot}/Dy/Share/";
#endif

    /// <summary>
    /// http请求根地址
    /// </summary>
#if UNITY_EDITOR
    public static string httpRoot = "http://127.0.0.1:3080";
#elif WEIXINMINIGAME
    public static string httpRoot = "http://127.0.0.1:3080";
#elif DOUYINMINIGAME
    public static string httpRoot = "http://127.0.0.1:3080";
#endif

    /// <summary>
    /// app版本号
    /// </summary>
    public static readonly string appVersion = "v0.1";
    /// <summary>
    /// 排行榜版本，用来清空排行榜
    /// </summary>
    public static readonly string rankVersion = "v1.0";

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

}
