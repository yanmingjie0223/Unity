using System.Collections.Generic;

namespace Assets.Scripts.Platform
{

    public enum CodeStatus
    {
        OK = 0,
        ERROR = -1,
    }

    public enum PlatformType
    {
        H5 = 0,
        WX,
    }

    public class UserBody
    {
        public string nickName = "";
        public string avatarUrl = "";
        public string country = "";
        public string province = "";
        public string city = "";
        public string language = "zh_CN";
        public double gender = 0;
    }

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
        public int record;

        public void Initialize(Msg.User.RankData rankData)
        {
            rank = rankData.Rank;
            nickname = rankData.Nickname;
            avatarUrl = rankData.AvatarUrl;
            record = rankData.Record;
        }
    }

    [System.Serializable]
    public class WxMessageRecordData
    {
        public string type;
        public int record;
    }

    [System.Serializable]
    public class WxMessageUserData
    {
        public string type;
        public int record;
        public string avatarUrl;
        public string nickname;
        public string version;
        public string openId;
    }

}
