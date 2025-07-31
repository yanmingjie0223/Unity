#if !UNITY_EDITOR && DOUYINMINIGAME
using System;
using System.Collections.Generic;
using TTSDK;

namespace Assets.Scripts.Platform
{
    public class DYSystem : ISystem
    {

        private string defaultImageUrl = "";

        public void InitSDK(Action<int> initCB)
        {
            TT.InitSDK((code, env) =>
            {
                initCB?.Invoke(code);
            });
        }

        public void LoginSDK(Action<string> completeCB, Action<string> errorCB)
        {
            TT.Login(
                (string code, string anonymousCode, bool isLogin) =>
                {
                    completeCB?.Invoke(code);
                },
                (string errMsg) =>
                {
                    errorCB?.Invoke(errMsg);
                }
            );
        }

        public void InitUserInfo(Action<UserBody> completeCB, Action<string> errorCB)
        {
            TT.Authorize(
                "scope.userInfo",
                (key, res) =>
                {
                    GetUserInfo(completeCB, errorCB);
                },
                (key, errMsg) =>
                {
                    errorCB?.Invoke(errMsg);
                }
            );
        }

        public void TryGetUserInfo(Action<UserBody> completeCB, Action<string> errorCB)
        {
            TT.GetSetting(
                (AuthSetting authSetting) =>
                {
                    if (authSetting.UserInfo)
                    {
                        TT.GetUserInfo(
                            (ref TTUserInfo scUserInfo) =>
                            {
                                UserBody userBodyInfo = new()
                                {
                                    nickName = scUserInfo.nickName,
                                    avatarUrl = scUserInfo.avatarUrl,
                                    country = scUserInfo.country,
                                    province = scUserInfo.province,
                                    city = scUserInfo.city,
                                    language = scUserInfo.language,
                                    gender = scUserInfo.gender
                                };
                                completeCB?.Invoke(userBodyInfo);
                            },
                            (string errMsg) =>
                            {
                                errorCB?.Invoke(errMsg);
                            }
                        );
                    }
                    else
                    {
                        errorCB?.Invoke("");
                    }
                },
                (string errMsg) =>
                {
                    errorCB?.Invoke(errMsg);
                }
            );
        }

        public void Exit()
        {
            TT.ExitMiniProgram();
        }

        public void KeepScreenOn(bool isOn)
        {
            TT.SetKeepScreenOn(isOn);
        }

        public void RequestSubscribeMessage()
        {
            TT.RequestSubscribeMessage(null);
        }

        public void Restart()
        {
            TT.RestartMiniProgramSync();
        }

        public void VibrateShort(VibrateType vibrateType = VibrateType.Light)
        {
            TT.VibrateShort(new VibrateShortParam() { });
        }

        public void VibrateLong()
        {
            TT.VibrateLong(new VibrateLongParam());
        }

        public bool CheckRunVersionIsOrHigher(string version)
        {
            string curVersion = TT.GetSystemInfo().sdkVersion;
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
                    return true;
                }
                else if (curVer < ver)
                {
                    return false;
                }
            }
            return false;
        }

        public void SetShareMessage(string imageUrl, string title, string query)
        {
            defaultImageUrl = imageUrl;
            TT.ShowShareMenu();
            TT.OnShareAppMessage((TTShare.ShareOption shareOption) =>
            {
                var shareJson = new TTSDK.UNBridgeLib.LitJson.JsonData();
                shareJson["title"] = title;
                shareJson["templateId"] = "";
                shareJson["query"] = query;
                shareJson["channel"] = shareOption.channel;
                return new TTShare.ShareParam(
                    shareJson,
                    (Dictionary<string, object> data) => { },
                    (string errMsg) => { },
                    () => { }
                );
            });
        }

        public void ShareAppMessage(string imageUrl, string title, string query, Action<bool> shareCB)
        {
            var shareJson = new TTSDK.UNBridgeLib.LitJson.JsonData();
            shareJson["title"] = title;
            shareJson["imageUrl"] = imageUrl;
            shareJson["query"] = query;
            TT.ShareAppMessage(
                shareJson, 
                (Dictionary<string, object> data) =>
                {
                    shareCB?.Invoke(true);
                },
                (string errMsg) =>
                {
                    shareCB?.Invoke(false);
                },
                () =>
                {
                    shareCB?.Invoke(true);
                }
            );
        }

        public void ShareTemplateAppMessage(string templateId, string title, string query, Action<bool> shareCB)
        {
            var shareJson = new TTSDK.UNBridgeLib.LitJson.JsonData();
            shareJson["title"] = title;
            shareJson["templateId"] = templateId;
            shareJson["query"] = query;
            TT.ShareAppMessage(
                shareJson, 
                (Dictionary<string, object> data) =>
                {
                    shareCB?.Invoke(true);
                },
                (string errMsg) =>
                {
                    shareCB?.Invoke(false);
                },
                () =>
                {
                    shareCB?.Invoke(true);
                }
            );
        }

        public void ShareAppMessageByScreenshot(int x, int y, int width, int height, string title, string query, Action<bool> shareCB)
        {
            ShareAppMessage(defaultImageUrl, title, query, shareCB);
        }

        public void UpdateVersion()
        {
            var updateManager = TT.GetUpdateManager();
            updateManager.OnCheckForUpdate((res) =>
            {
            });
            updateManager.OnUpdateReady(() =>
            {
                updateManager.ApplyUpdate(new ApplyUpdateParams());

            });
            updateManager.OnUpdateFailed((rt) =>
            {
                UpdateVersion();
            });
        }

        public void OnShow(Action<ShowListenerResult> action)
        {
            TT.GetAppLifeCycle().OnShow += (rt) =>
            {
                UpdateVersion();
                action?.Invoke(new());
            };
        }

        private void GetUserInfo(Action<UserBody> completeCB, Action<string> errorCB)
        {
            TT.GetSetting(
                (AuthSetting authSetting) =>
                {
                    if (authSetting.UserInfo)
                    {
                        TT.GetUserInfo(
                            (ref TTUserInfo scUserInfo) =>
                            {
                                UserBody userBodyInfo = new()
                                {
                                    nickName = scUserInfo.nickName,
                                    avatarUrl = scUserInfo.avatarUrl,
                                    country = scUserInfo.country,
                                    province = scUserInfo.province,
                                    city = scUserInfo.city,
                                    language = scUserInfo.language,
                                    gender = scUserInfo.gender
                                };
                                completeCB?.Invoke(userBodyInfo);
                            },
                            (string errMsg) =>
                            {
                                errorCB?.Invoke(errMsg);
                            }
                        );
                    }
                    else
                    {
                        InitUserInfo(completeCB, errorCB);
                    }
                },
                (string errMsg) =>
                {
                    errorCB?.Invoke(errMsg);
                }
            );
        }
    }
}
#endif