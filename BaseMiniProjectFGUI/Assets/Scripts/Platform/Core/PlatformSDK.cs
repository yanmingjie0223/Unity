﻿using UnityEngine;

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
        /// <summary>
        /// 开放域
        /// </summary>
        private IOpenContext _openContext;
        /// <summary>
        /// 本地存储
        /// </summary>
        private IStorage _localStorage;

        public void Initialize(PlatformType type)
        {
            switch (type)
            {
                case PlatformType.H5:
                    _videoExpo = new H5VideoExpo();
                    _system = new H5System();
                    _openContext = H5OpenContext.GetInstance();
                    _localStorage = new H5Storage();
                    break;
                case PlatformType.WX:
#if !UNITY_EDITOR && WEIXINMINIGAME
                    _videoExpo = new WXVideoExpo();
                    _system = new WXSystem();
                    _openContext = WXOpenContext.GetInstance();
                    _localStorage = new WXStorage();
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

        public IOpenContext GetOpenContext()
        {
            return _openContext;
        }

        public IStorage GetLocalStorage()
        {
            return _localStorage;
        }

    }

}
