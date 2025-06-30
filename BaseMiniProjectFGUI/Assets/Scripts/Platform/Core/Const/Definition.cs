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
        /// <summary>
        /// 微信小游戏
        /// </summary>
        WX,
        /// <summary>
        /// 抖音小游戏
        /// </summary>
        DY
    }

    public enum VibrateType
    {
        /// <summary>
        /// 轻微震动
        /// </summary>
        Light = 0,
        /// <summary>
        /// 中等震动
        /// </summary>
        Medium = 1,
        /// <summary>
        /// 重度震动
        /// </summary>
        Heavy = 2,
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

    public class ResultReferrerInfo
    {
        /// <summary>
        /// 来源小程序或公众号或App的 appId
        /// </summary>        
        public string appId;
        /// <summary>
        /// 来源小程序传过来的数据，scene=1037或1038时支持
        /// </summary>        
        public Dictionary<string, string> extraData;
    }

    public class ShowListenerResult
    {
        /// <summary>
        /// 查询参数
        /// </summary>
        public Dictionary<string, string> query;
        /// <summary>
        /// 当场景为由从另一个小程序或公众号或App打开时，返回此字段
        /// </summary>
        public ResultReferrerInfo referrerInfo;
        /// <summary>
        /// 场景值
        /// </summary>
        public double scene;
        /// <summary>
        /// 从微信群聊/单聊打开小程序时，chatType 表示具体微信群聊/单聊类型
        /// 1	微信联系人单聊
        /// 2	企业微信联系人单聊
        /// 3	普通微信群聊
        /// 4	企业微信互通群聊
        /// </summary>
        public double? chatType;
        /// <summary>
        /// shareTicket
        /// </summary>
        public string shareTicket;
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
