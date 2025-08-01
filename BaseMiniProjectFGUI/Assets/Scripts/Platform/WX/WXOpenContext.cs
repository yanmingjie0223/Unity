using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
#if !UNITY_EDITOR && WEIXINMINIGAME
using WeChatWASM;
#endif

namespace Assets.Scripts.Platform
{

    public class WXOpenContext : SingletonMono<WXOpenContext>, IOpenContext
    {

        public RawImage RankBody;

        public void Show()
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

        public void Hide()
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
            var messageStr = JsonUtility.ToJson(message);
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
            var messageStr = JsonUtility.ToJson(message);
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
            var messageStr = JsonUtility.ToJson(message);
            openDataContext.PostMessage(messageStr);
#endif
        }

        public void SendRecord(int record)
        {
#if !UNITY_EDITOR && WEIXINMINIGAME
            var openDataContext = new WXOpenDataContext();
            var message = new WxMessageRecordData
            {
                type = "setUserRecord",
                record = record
            };
            var messageStr = JsonUtility.ToJson(message);
            openDataContext.PostMessage(messageStr);
#endif
        }

        public void SendUserData()
        {
#if !UNITY_EDITOR && WEIXINMINIGAME
            var openDataContext = new WXOpenDataContext();
            var userData = UserData.GetInstance();
            var wxUserInfo = userData.GetUserBodyInfo();
            var message = new WxMessageUserData
            {
                type = "setUserData",
                record = userData.GetRecord(),
                avatarUrl = wxUserInfo != null ? wxUserInfo.avatarUrl : "",
                nickname = wxUserInfo != null ? wxUserInfo.nickName : "",
                version = GameConfig.rankVersion,
                openId = userData.GetOpenId(),
            };
            var messageStr = JsonUtility.ToJson(message);
            openDataContext.PostMessage(messageStr);
#endif
        }

    }

}
