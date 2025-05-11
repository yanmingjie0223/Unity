public class DateUtils
{
    /// <summary>
    /// 获取时间(s)
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static float GetSecondByStr(string str)
    {
        if (str == null || str.Length == 0) return 0;

        var arrStr = str.Split(':');
        var hour = arrStr[0];
        var minute = arrStr[1];
        var second = arrStr[2];
        float _sec = 0;
        _sec += float.Parse(second);
        _sec += float.Parse(minute) * 60;
        _sec += float.Parse(hour) * 3600;

        return _sec;
    }
}
