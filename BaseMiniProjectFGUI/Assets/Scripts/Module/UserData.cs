using Assets.Scripts.Platform;
using WeChatWASM;

[System.Serializable]
public class UserData : Singleton<UserData>
{

    private Msg.User.UserData proto;

    private UserInfo _userBodyInfo;
    private string _openId;
    private string _token;
    private bool _dirty;

    public void Deserializable(Msg.User.S2C_Login data)
    {
        var dUser = data.User;
        _openId = data.OpenId;
        _token = data.Token;
        proto = dUser;
        if (_userBodyInfo != null)
        {
            proto.AvatarUrl = _userBodyInfo.avatarUrl;
            proto.Nickname = _userBodyInfo.nickName;
            proto.Province = _userBodyInfo.province;
            proto.City = _userBodyInfo.city;
        }
        PlatformSDK.GetInstance().GetOpenContext().SendUserData();
    }

    public Msg.User.C2S_SaveData Serializable()
    {
        var c2s = new Msg.User.C2S_SaveData
        {
            Token = _token,
            OpenId = _openId,
            User = proto
        };
        return c2s;
    }

    public int GetLevel()
    {
        return 0;
    }

    public int GetRecord()
    {
        return GetLevel();
    }

    public void SetUserBodyInfo(UserInfo userInfo)
    {
        _dirty = true;
        _userBodyInfo = userInfo;
        if (proto != null)
        {
            proto.AvatarUrl = _userBodyInfo.avatarUrl;
            proto.Nickname = _userBodyInfo.nickName;
            proto.Province = _userBodyInfo.province;
            proto.City = _userBodyInfo.city;
        }
    }

    public UserInfo GetUserBodyInfo()
    {
        return _userBodyInfo;
    }

    public void SetOpenId(string openId)
    {
        _openId = openId;
    }

    public string GetOpenId()
    {
        return _openId;
    }

    public void SetToken(string token)
    {
        _token = token;
    }

    public string GetToken()
    {
        return _token;
    }

    public void RestartGame()
    {
    }

}
