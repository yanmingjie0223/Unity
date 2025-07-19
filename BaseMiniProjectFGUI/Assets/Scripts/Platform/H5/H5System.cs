using System;

namespace Assets.Scripts.Platform
{

    public class H5System : ISystem
    {

        public void InitSDK(Action<int> initCB)
        {
            initCB?.Invoke(0);
        }

        public void LoginSDK(Action<string> completeCB, Action<string> errorCB)
        {
            completeCB?.Invoke("");
        }

        public void InitUserInfo(Action<UserBody> completeCB, Action<string> errorCB)
        {
            completeCB?.Invoke(new());
        }

        public void GetUserInfo(Action<UserBody> completeCB, Action<string> errorCB)
        {
            completeCB?.Invoke(new());
        }

        public void TryGetUserInfo(Action<UserBody> completeCB, Action<string> errorCB)
        {
            completeCB?.Invoke(new());
        }

        public void Exit()
        {
        }

        public void KeepScreenOn(bool isOn)
        {
        }

        public void RequestSubscribeMessage()
        {
        }

        public void Restart()
        {
        }

        public void VibrateShort(VibrateType vibrateType = VibrateType.Light)
        {
        }

        public void VibrateLong()
        {
        }

        public bool CheckRunVersionIsOrHigher(string version)
        {
            return true;
        }

        public void SetShareMessage(string imageUrl, string title, string query)
        {
        }

        public void ShareAppMessage(string imageUrl, string title, string query, Action<bool> shareCB)
        {
            shareCB?.Invoke(true);
        }

        public void ShareAppMessageByScreenshot(int x, int y, int width, int height, string title, string query, Action<bool> shareCB)
        {
            shareCB?.Invoke(true);
        }

        public void UpdateVersion()
        {
        }

        public void OnShow(Action<ShowListenerResult> action)
        {
        }

    }

}
