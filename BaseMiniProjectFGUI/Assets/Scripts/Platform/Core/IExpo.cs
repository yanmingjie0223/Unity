using System;

namespace Assets.Scripts.Platform
{

    public class ExpoOptions
    {

        /// <summary>
        /// 广告唯一id
        /// </summary>
        public string ADId;

        /// <summary>
        /// 广告关闭回调
        /// </summary>
        public Action<bool> OnClose;

        /// <summary>
        /// 广告错误回调
        /// </summary>
        public Action<CodeStatus, string> OnError;

    }

    public interface IExpo
    {

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="options"></param>
        void Initialize(ExpoOptions options);

        /// <summary>
        /// 展示广告
        /// </summary>
        void Show();

        /// <summary>
        /// 销毁
        /// </summary>
        void Destroy();

    }

}
