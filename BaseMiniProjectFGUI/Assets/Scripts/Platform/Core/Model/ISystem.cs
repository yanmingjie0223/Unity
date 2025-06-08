using System;

namespace Assets.Scripts.Platform
{

    public interface ISystem
    {

        /// <summary>
        /// 初始化平台sdk
        /// </summary>
        /// <param name="initCB"></param>
        void InitSDK(Action<int> initCB);

        /// <summary>
        /// 登录sdk返回code
        /// </summary>
        /// <param name="completeCB"></param>
        /// <param name="errorCB"></param>
        void LoginSDK(Action<string> completeCB, Action<string> errorCB);

        /// <summary>
        /// 获取user数据
        /// </summary>
        /// <param name="completeCB"></param>
        /// <param name="errorCB"></param>
        void InitUserInfo(Action<UserBody> completeCB, Action<string> errorCB);

        /// <summary>
        /// 尝试获取用户信息
        /// </summary>
        /// <param name="completeCB"></param>
        /// <param name="errorCB"></param>
        void TryGetUserInfo(Action<UserBody> completeCB, Action<string> errorCB);

        /// <summary>
		/// 退出游戏
		/// </summary>
        void Exit();

        /// <summary>
        /// 重启游戏
        /// </summary>
        void Restart();

        /// <summary>
        /// 手机震动
        /// </summary>
        void VibrateShort();

        /// <summary>
        /// 手机长时间震动
        /// </summary>
        void VibrateLong();

        /// <summary>
        /// 检测运行版本是或者更高
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        bool CheckRunVersionIsOrHigher(string version);

        /// <summary>
        /// 保持常亮
        /// </summary>
        /// <param name="isOn"></param>
        void KeepScreenOn(bool isOn);

        /// <summary>
        /// 订阅数据
        /// </summary>
        void RequestSubscribeMessage();

        /// <summary>
        /// 设置分享信息
        /// </summary>
        /// <param name="imageUrl"></param>
        /// <param name="title"></param>
        /// <param name="query"></param>
        void SetShareMessage(string imageUrl, string title, string query);

        /// <summary>
        /// 分享应用信息
        /// </summary>
        /// <param name="imageUrl"></param>
        /// <param name="title"></param>
        /// <param name="query"></param>
        void ShareAppMessage(string imageUrl, string title, string query);

        /// <summary>
        /// 分享截图信息
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="title"></param>
        /// <param name="query"></param>
        void ShareAppMessageByScreenshot(int x, int y, int width, int height, string title, string query);

    }

}
