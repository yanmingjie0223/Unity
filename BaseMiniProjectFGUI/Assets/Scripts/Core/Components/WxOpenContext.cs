using System.Collections.Generic;
using UnityEngine.UI;
using Newtonsoft.Json;
using UnityEngine;
#if !UNITY_EDITOR && WEIXINMINIGAME
using WeChatWASM;
#endif

[System.Serializable]
public class WxMessageData
{
    public string type;
}

[System.Serializable]
public class WxMessageWorldData
{
    public string type;
    public List<WxMessageRankData> data;
}

[System.Serializable]
public class WxMessageRankData
{
    public int rank;
    public string nickname;
    public string avatarUrl;
    public int level;

    public void Initialize(Msg.User.RankData rankData)
    {
        rank = rankData.Rank;
        nickname = rankData.Nickname;
        avatarUrl = rankData.AvatarUrl;
        level = rankData.Level;
    }
}

[System.Serializable]
public class WxMessageRecordData
{
    public string type;
    public int level;
}

[System.Serializable]
public class WxMessageUserData
{
    public string type;
    public int level;
    public string avatarUrl;
    public string nickname;
    public string version;
    public string openId;
}

public class WxOpenContext : SingletonMono<WxOpenContext>
{

    public RawImage RankBody;

    public void ShowOpenData()
    {
        var canvasScaler = gameObject.GetComponent<CanvasScaler>();
        if (canvasScaler == null)
        {
            return;
        }

        var referenceResolution = canvasScaler.referenceResolution;
        Vector2 screenSize = new(Screen.width, Screen.height);
        var ratioX = screenSize.x / referenceResolution.x;
        var ratioY = screenSize.y / referenceResolution.y;
        if (ratioX > ratioY)
        {
            canvasScaler.matchWidthOrHeight = 1.0f;
        }
        else
        {
            canvasScaler.matchWidthOrHeight = 0f;
        }
        var p = RankBody.transform.position;
        RankBody.gameObject.SetActive(true);

#if !UNITY_EDITOR && WEIXINMINIGAME
        WX.ShowOpenData(RankBody.texture, (int)p.x, Screen.height - (int)p.y, (int)((Screen.width / referenceResolution.x) * RankBody.rectTransform.rect.width), (int)((Screen.width / referenceResolution.x) * RankBody.rectTransform.rect.height));
#endif
    }

    public void HideOpenData()
    {
#if !UNITY_EDITOR && WEIXINMINIGAME
        WX.HideOpenData();
#endif
        RankBody.gameObject.SetActive(false);
    }

    public void SendFriendMessage()
    {
#if !UNITY_EDITOR && WEIXINMINIGAME
        var openDataContext = new WXOpenDataContext();
        var message = new WxMessageData
        {
            type = "showFriendsRank"
        };
        var messageStr = JsonConvert.SerializeObject(message, Formatting.Indented);
        openDataContext.PostMessage(messageStr);
#endif
    }

    public void SendGroupFriendMessage()
    {
#if !UNITY_EDITOR && WEIXINMINIGAME
        var openDataContext = new WXOpenDataContext();
        var message = new WxMessageData
        {
            type = "showGroupFriendsRank"
        };
        var messageStr = JsonConvert.SerializeObject(message, Formatting.Indented);
        openDataContext.PostMessage(messageStr);
#endif
    }

    public void SendWorldMessage(List<Msg.User.RankData> ranks)
    {
#if !UNITY_EDITOR && WEIXINMINIGAME
        var openDataContext = new WXOpenDataContext();
        List<WxMessageRankData> datas = new();
        foreach (var rank in ranks)
        {
            var item = new WxMessageRankData();
            item.Initialize(rank);
            datas.Add(item);
        }
        var message = new WxMessageWorldData
        {
            type = "showWorldRank",
            data = datas
        };
        var messageStr = JsonConvert.SerializeObject(message, Formatting.Indented);
        openDataContext.PostMessage(messageStr);
#endif
    }

    public void SendTime(int time)
    {
#if !UNITY_EDITOR && WEIXINMINIGAME
        var openDataContext = new WXOpenDataContext();
        var message = new WxMessageRecordData
        {
            type = "setUserRecord",
            level = time
        };
        var messageStr = JsonConvert.SerializeObject(message, Formatting.Indented);
        openDataContext.PostMessage(messageStr);
#endif
    }

    public void SendUserData()
    {
#if !UNITY_EDITOR && WEIXINMINIGAME
        var openDataContext = new WXOpenDataContext();
        var userData = UserData.GetInstance();
        var wxUserInfo = userData.GetWxUserInfo();
        var message = new WxMessageUserData
        {
            type = "setUserData",
            level = userData.GetLevel(),
            avatarUrl = wxUserInfo != null ? wxUserInfo.avatarUrl : "",
            nickname = wxUserInfo != null ? wxUserInfo.nickName : "",
            version = GameConfig.rankVersion,
            openId = userData.GetOpenId(),
        };
        var messageStr = JsonConvert.SerializeObject(message, Formatting.Indented);
        openDataContext.PostMessage(messageStr);
#endif
    }

}
