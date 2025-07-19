using System.Collections.Generic;
using UnityEngine.UI;
#if !UNITY_EDITOR && DOUYINMINIGAME
using TTSDK;
#endif

namespace Assets.Scripts.Platform
{

    public class DYOpenContext : SingletonMono<DYOpenContext>, IOpenContext
    {

        public RawImage RankBody;

        public void Show()
        {
            RankBody.gameObject.SetActive(false);
        }

        public void Hide()
        {
            RankBody.gameObject.SetActive(false);
        }

        public void SendFriendMessage()
        {
            SendAllMessage();
        }

        public void SendGroupFriendMessage()
        {
        }

        public void SendWorldMessage(List<Msg.User.RankData> ranks)
        {
            SendAllMessage();
        }

        public void SendRecord(int record)
        {
#if !UNITY_EDITOR && DOUYINMINIGAME
            var useData = UserData.GetInstance();
            var rankJson = new TTSDK.UNBridgeLib.LitJson.JsonData();
            rankJson["extra"] = GameConfig.rankVersion;
            rankJson["zoneId"] = "default";
            rankJson["value"] = useData.GetRecord();
            rankJson["dataType"] = 0;
            TT.SetImRankData(rankJson);
#endif
        }

        public void SendUserData()
        {
        }

        private void SendAllMessage()
        {
#if !UNITY_EDITOR && DOUYINMINIGAME
            var rankJson = new TTSDK.UNBridgeLib.LitJson.JsonData();
            rankJson["zoneId"] = "default";
            rankJson["suffix"] = "关";
            rankJson["rankTitle"] = "排行榜";
            rankJson["dataType"] = 0;
            rankJson["relationType"] = "default";
            rankJson["rankType"] = "all";
            TT.GetImRankList(rankJson);
#endif
        }

    }

}
