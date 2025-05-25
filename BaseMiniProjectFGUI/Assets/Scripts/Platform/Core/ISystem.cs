namespace Assets.Scripts.Platform
{

    public interface ISystem
    {

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

    }

}
