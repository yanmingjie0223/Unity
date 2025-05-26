using UnityEngine;

namespace Assets.Scripts.Platform
{

    public class PlatformSDK : Singleton<PlatformSDK>
    {

        /// <summary>
        /// 广告
        /// </summary>
        private IExpo _videoExpo;
        /// <summary>
        /// 系统
        /// </summary>
        private ISystem _system;

        public void Initialize(PlatformType type)
        {
            switch (type)
            {
                case PlatformType.H5:
                    _videoExpo = new H5VideoExpo();
                    _system = new H5System();
                    break;
                case PlatformType.WX:
#if !UNITY_EDITOR && WEIXINMINIGAME
                    _videoExpo = new WXVideoExpo();
                    _system = new WXSystem();
#endif
                    break;
                default:
                    Debug.LogError($"Unprocessed PlatformType: {type}");
                    break;
            }
        }

        public ISystem GetSystem()
        {
            return _system;
        }

        public IExpo GetVideoExpo()
        {
            return _videoExpo;
        }

    }

}
