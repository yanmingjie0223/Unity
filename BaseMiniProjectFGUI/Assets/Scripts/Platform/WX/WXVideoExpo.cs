#if !UNITY_EDITOR && WEIXINMINIGAME
using WeChatWASM;

namespace Assets.Scripts.Platform
{

    public class WXVideoExpo : IExpo
    {

        private WXRewardedVideoAd _video;
        private ExpoOptions _options;

        public void Initialize(ExpoOptions options)
        {
            _options = options;
            if (PlatformSDK.GetInstance().GetSystem().CheckRunVersionIsOrHigher("2.8.0"))
            {
                _video = WX.CreateRewardedVideoAd(new WXCreateRewardedVideoAdParam()
                {
                    adUnitId = options.ADId,
                    multiton = true,
                });
            }
            else
            {
                _video = WX.CreateRewardedVideoAd(new WXCreateRewardedVideoAdParam()
                {
                    adUnitId = options.ADId,
                });
            }
        }

        public void Destroy()
        {
            if (_video != null)
            {
                _video.OffClose(OnCloseCB);
                _video.OffError(OnErrorCB);
                _video.OffLoad(OnLoadCB);
                _video = null;
                _options = null;
            }
        }

        public void Show(ExpoOptions options)
        {
            Initialize(options);
            if (_video != null)
            {
                _video.OnClose(OnCloseCB);
                _video.OnError(OnErrorCB);
                _video.OnLoad(OnLoadCB);
                _video.Load();
            }
        }

        private void OnLoadCB(WXADLoadResponse res)
        {
            if (_options == null)
            {
                return;
            }

            if (res != null && !string.IsNullOrEmpty(res.errMsg))
            {
                _options.OnError?.Invoke(CodeStatus.ERROR, res.errMsg);
            }
            else
            {
                _video?.Show();
            }
        }

        private void OnCloseCB(WXRewardedVideoAdOnCloseResponse res)
        {
            if (_options == null)
            {
                return;
            }

            _options.OnClose?.Invoke(res.isEnded);
            Destroy();
        }

        private void OnErrorCB(WXADErrorResponse res)
        {
            if (_options == null)
            {
                return;
            }

            _options.OnError?.Invoke(CodeStatus.ERROR, res.errMsg);
        }

    }

}
#endif
