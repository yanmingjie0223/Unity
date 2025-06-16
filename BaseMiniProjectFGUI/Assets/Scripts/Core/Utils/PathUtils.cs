using UnityEngine;
using YooAsset;

public class PathUtils
{

    /// <summary>
    /// 获取资源服务器地址
    public static string GetHostServerURL()
    {
        string hostServerIP = GetHostServer(); ;

#if UNITY_EDITOR
        if (UnityEditor.EditorUserBuildSettings.activeBuildTarget == UnityEditor.BuildTarget.Android)
            return $"{hostServerIP}/CDN/Android/{GameConfig.appVersion}";
        else if (UnityEditor.EditorUserBuildSettings.activeBuildTarget == UnityEditor.BuildTarget.iOS)
            return $"{hostServerIP}/CDN/IPhone/{GameConfig.appVersion}";
        else if (UnityEditor.EditorUserBuildSettings.activeBuildTarget == UnityEditor.BuildTarget.WebGL)
            return $"{hostServerIP}/CDN/WebGL/{GameConfig.appVersion}";
        else
            return $"{hostServerIP}/CDN/PC/{GameConfig.appVersion}";
#else
        if (Application.platform == RuntimePlatform.Android)
            return $"{hostServerIP}/CDN/Android/{GameConfig.appVersion}";
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
            return $"{hostServerIP}/CDN/IPhone/{GameConfig.appVersion}";
        else if (Application.platform == RuntimePlatform.WebGLPlayer)
            return $"{hostServerIP}/CDN/WebGL/{GameConfig.appVersion}";
        else
            return $"{hostServerIP}/CDN/PC/{GameConfig.appVersion}";
#endif

    }

    public static string GetHostServer()
    {
        string hostServerIP = "";
        switch (GameConfig.serverType)
        {
            case ServerType.Local:
            case ServerType.Local_Web:
                hostServerIP = "http://192.168.1.8:8080/ResServer/BaseProjectFGUI";
                break;
            case ServerType.Web:
                hostServerIP = $"{GameConfig.cdnRoot}";
                break;
        }
        return hostServerIP;
    }

    public static EPlayMode GetPlayMode()
    {
        EPlayMode mode = EPlayMode.EditorSimulateMode;
        switch (GameConfig.serverType)
        {
            case ServerType.Local:
                mode = EPlayMode.EditorSimulateMode;
                break;
            case ServerType.Local_Web:
                break;
            case ServerType.Web:
                mode = EPlayMode.WebPlayMode;
                break;
        }
        return mode;
    }

    public static string GetGroupName(GroupType groupType)
    {
        string groupName = "";
        switch (groupType)
        {
            case GroupType.Config: groupName = GroupTypeName.Config; break;
            case GroupType.UI: groupName = GroupTypeName.UI; break;
            case GroupType.Sound: groupName = GroupTypeName.Sound; break;
        }
        return groupName;
    }

}
