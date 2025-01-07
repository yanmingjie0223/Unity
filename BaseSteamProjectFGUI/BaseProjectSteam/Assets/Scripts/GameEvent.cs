public enum GameEvent
{
    /// <summary>
    /// view显示
    /// </summary>
    VIEW_SHOW,
    /// <summary>
    /// view界面资源加载
    /// </summary>
    VIEW_LOAD,
    /// <summary>
    /// view界面完全展示
    /// </summary>
    VIEW_SHOWN,
    /// <summary>
    /// view界面关闭
    /// </summary>
    VIEW_CLOSE,
    /// <summary>
    /// window弹窗界面全部关闭
    /// </summary>
    WINDOW_CLOSE,

    /// <summary>
    /// 开始第几场
    /// </summary>
    SCENE_START_ID,
    /// <summary>
    /// 场景停止
    /// </summary>
    SCENE_STOP,
    /// <summary>
    /// 场景重启
    /// </summary>
    SCENE_RESUME,

    /// <summary>
    /// 上键事件
    /// </summary>
    GAME_UP_DOWN,
    GAME_UP_UP,
    /// <summary>
    /// 下键事件
    /// </summary>
    GAME_DOWN_DOWN,
    GAME_DOWN_UP,
    /// <summary>
    /// 左键事件
    /// </summary>
    GAME_LEFT_DOWN,
    GAME_LEFT_UP,
    /// <summary>
    /// 右键事件
    /// </summary>
    GAME_RIGHT_DOWN,
    GAME_RIGHT_UP,
}
