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

        public void Initialize(IExpo videoExpo, ISystem system)
        {
            _videoExpo = videoExpo;
            _system = system;
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
