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
        public string nickName;
        public string avatarUrl;
        public string country;
        public string province;
        public string city;
        public string language;
        public double gender;
    }

}
