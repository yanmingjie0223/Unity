#if !UNITY_EDITOR && DOUYINMINIGAME
using TTSDK;

namespace Assets.Scripts.Platform
{

    public class DYVideoExpo : IExpo
    {

        private TTRewardedVideoAd _video;
        private ExpoOptions _options;

        public void Initialize(ExpoOptions options)
        {
            _options = options;
            _video = TT.CreateRewardedVideoAd(
                options.ADId,
                (bool isEnded, int count) =>
                {
                    _options.OnClose?.Invoke(isEnded);
                },
                (int code, string errMsg) =>
                {
                    _options.OnError?.Invoke(CodeStatus.ERROR, errMsg);
                }
            );
        }

        public void Destroy()
        {
            if (_video != null)
            {
                _video.OnClose -= OnCloseCB;
                _video.OnError -= OnErrorCB;
                _video.OnLoad -= OnLoadCB;
                _video = null;
                _options = null;
            }
        }

        public void Show(ExpoOptions options)
        {
            Initialize(options);
            if (_video != null)
            {
                _video.OnClose += OnCloseCB;
                _video.OnError += OnErrorCB;
                _video.OnLoad += OnLoadCB;
                _video.Load();
            }
        }

        private void OnLoadCB()
        {
            if (_options == null)
            {
                return;
            }

            _video?.Show();
        }

        private void OnCloseCB(bool isEnded, int count)
        {
            if (_options == null)
            {
                return;
            }

            _options.OnClose?.Invoke(isEnded);
            Destroy();
        }

        private void OnErrorCB(int code, string errMsg)
        {
            if (_options == null)
            {
                return;
            }

            _options.OnError?.Invoke(CodeStatus.ERROR, errMsg);
        }

    }

}
#endif