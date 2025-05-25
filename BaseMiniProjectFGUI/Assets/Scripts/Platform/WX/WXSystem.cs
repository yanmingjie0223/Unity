using System;
using WeChatWASM;

namespace Assets.Scripts.Platform
{

    public class WXSystem : ISystem
    {

        public void Exit()
        {
            WX.ExitMiniProgram(null);
        }

        public void KeepScreenOn(bool isOn)
        {
            WX.SetKeepScreenOn(null);
        }

        public void RequestSubscribeMessage()
        {
            WX.RequestSubscribeMessage(null);
        }

        public void Restart()
        {
            WX.RestartMiniProgram(null);
        }

        public void VibrateShort()
        {
            WX.VibrateShort(new VibrateShortOption() { type = "medium" });
        }

        public bool CheckRunVersionIsOrHigher(string version)
        {
            string curVersion = WX.GetAppBaseInfo().SDKVersion;
            string[] curVerArr = curVersion.Split('.');
            string[] verArr = version.Split('.');
            int curLen = curVerArr.Length;
            int len = verArr.Length;
            int maxLen = Math.Max(curLen, len);
            for (int i = 0; i < maxLen; i++)
            {
                int curVer;
                int ver;
                if (i < curLen)
                {
                    curVer = int.Parse(curVerArr[i]);
                }
                else
                {
                    curVer = 0;
                }
                if (i < len)
                {
                    ver = int.Parse(verArr[i]);
                }
                else
                {
                    ver = 0;
                }
                if (curVer > ver)
                {
                    return false;
                }
                else if (curVer < ver)
                {
                    return true;
                }
            }
            return true;
        }

        public void SetShareMessage(string imageUrl, string title, string query)
        {
            string[] menus = { "shareAppMessage", "shareTimeline" };
            WX.ShowShareMenu(new ShowShareMenuOption
            {
                withShareTicket = true,
                menus = menus,
            });
            WX.OnShareAppMessage(new WXShareAppMessageParam
            {
                title = title,
                query = query,
                imageUrl = imageUrl,
            });
        }

    }

}
