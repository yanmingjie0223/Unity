﻿#if !UNITY_EDITOR && WEIXINMINIGAME
using UnityEngine;
using System;
using WeChatWASM;

namespace Assets.Scripts.Platform
{

    public class WXSystem : ISystem
    {

        public void InitSDK(Action<int> initCB)
        {
            WX.InitSDK(initCB);
        }

        public void LoginSDK(Action<string> completeCB, Action<string> errorCB)
        {
            WX.Login(new LoginOption()
            {
                success = (LoginSuccessCallbackResult rt) =>
                {
                    completeCB?.Invoke(rt.code);
                },
                fail = (RequestFailCallbackErr rt) =>
                {
                    errorCB?.Invoke(rt.errMsg);
                }
            });
        }

        public void InitUserInfo(Action<UserBody> completeCB, Action<string> errorCB)
        {
            WX.GetPrivacySetting(new GetPrivacySettingOption()
            {
                success = (res) =>
                {
                    if (res.needAuthorization)
                    {
                        WX.RequirePrivacyAuthorize(new RequirePrivacyAuthorizeOption()
                        {
                            success = (res) =>
                            {
                                GetUserInfo(completeCB, errorCB);
                            },
                            fail = (err) =>
                            {
                                errorCB?.Invoke(err.errMsg);
                            },
                        });
                    }
                    else
                    {
                        GetUserInfo(completeCB, errorCB);
                    }
                },
                fail = (err) =>
                {
                    errorCB?.Invoke(err.errMsg);
                },
            });
        }

        public void TryGetUserInfo(Action<UserBody> completeCB, Action<string> errorCB)
        {
            WX.GetSetting(new GetSettingOption()
            {
                success = (GetSettingSuccessCallbackResult rt) =>
                {
                    if (rt.authSetting.ContainsKey("scope.userInfo"))
                    {
                        WX.GetUserInfo(new GetUserInfoOption()
                        {
                            lang = "zh_CN",
                            success = (GetUserInfoSuccessCallbackResult rt) =>
                            {
                                UserBody userBodyInfo = new()
                                {
                                    nickName = rt.userInfo.nickName,
                                    avatarUrl = rt.userInfo.avatarUrl,
                                    country = rt.userInfo.country,
                                    province = rt.userInfo.province,
                                    city = rt.userInfo.city,
                                    language = rt.userInfo.language,
                                    gender = rt.userInfo.gender
                                };
                                completeCB?.Invoke(userBodyInfo);
                            }
                        });
                    }
                    else
                    {
                        errorCB?.Invoke("");
                    }
                },
                fail = (GeneralCallbackResult err) =>
                {
                    errorCB?.Invoke(err.errMsg);
                }
            });
        }

        public void Exit()
        {
            WX.ExitMiniProgram(null);
        }

        public void KeepScreenOn(bool isOn)
        {
            WX.SetKeepScreenOn(null);
        }

        public void RequestSubscribeMessage()
        {
            WX.RequestSubscribeMessage(null);
        }

        public void Restart()
        {
            WX.RestartMiniProgram(null);
        }

        public void VibrateShort(VibrateType vibrateType = VibrateType.Light)
        {
            switch (vibrateType)
            {
                case VibrateType.Light:
                    WX.VibrateShort(new VibrateShortOption() { type = "light" });
                    break;
                case VibrateType.Medium:
                    WX.VibrateShort(new VibrateShortOption() { type = "medium" });
                    break;
                case VibrateType.Heavy:
                    WX.VibrateShort(new VibrateShortOption() { type = "heavy" });
                    break;
                default:
                    WX.VibrateShort(new VibrateShortOption() { type = "light" });
                    break;
            }
        }

        public void VibrateLong()
        {
            WX.VibrateLong(new VibrateLongOption());
        }

        public bool CheckRunVersionIsOrHigher(string version)
        {
            string curVersion = WX.GetAppBaseInfo().SDKVersion;
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
            string[] menus = { "shareAppMessage", "shareTimeline" };
            WX.ShowShareMenu(new ShowShareMenuOption
            {
                withShareTicket = true,
                menus = menus,
            });
            WX.OnShareAppMessage(new WXShareAppMessageParam
            {
                title = title,
                query = query,
                imageUrl = imageUrl,
            });
        }

        public void ShareAppMessage(string imageUrl, string title, string query, Action<bool> shareCB)
        {
            WX.ShareAppMessage(new ShareAppMessageOption()
            {
                title = title,
                imageUrl = imageUrl,
                query = query
            });
            shareCB?.Invoke(true);
        }

        public void ShareTemplateAppMessage(string templateId, string title, string query, Action<bool> shareCB)
        {
            WX.ShareAppMessage(new ShareAppMessageOption()
            {
                title = title,
                imageUrlId = templateId,
                query = query
            });
            shareCB?.Invoke(true);
        }

        public void ShareAppMessageByScreenshot(int x, int y, int width, int height, string title, string query, Action<bool> shareCB)
        {
            int ShareWidth = width;
            int ShareHeight = height;
            var tf = WXCanvas.ToTempFilePathSync(new WXToTempFilePathParam()
            {
                x = x,
                y = y,
                width = ShareWidth,
                height = ShareHeight,
                destWidth = ShareWidth,
                destHeight = ShareHeight,
            });
            WX.ShareAppMessage(new ShareAppMessageOption()
            {
                title = title,
                imageUrl = tf,
                query = query
            });
            shareCB?.Invoke(true);
        }

        public void UpdateVersion()
        {
            var updateManager = WX.GetUpdateManager();
            updateManager.OnCheckForUpdate((res) =>
            {
            });
            updateManager.OnUpdateReady((rt) =>
            {
                WX.ShowModal(new ShowModalOption()
                {
                    title = "更新提示",
                    content = "新版本已经准备好，是否重启应用？",
                    success = (ShowModalSuccessCallbackResult res) =>
                    {
                        if (res.confirm)
                        {
                            // 新的版本已经下载好，调用 applyUpdate 应用新版本并重启
                            updateManager.ApplyUpdate();
                        }
                    }
                });
            });
            updateManager.OnUpdateFailed((rt) =>
            {
                UpdateVersion();
            });
        }

        public void OnShow(Action<ShowListenerResult> action)
        {
            WX.OnShow((rt) =>
            {
                UpdateVersion();
                action?.Invoke(new()
                {
                    query = rt.query,
                    referrerInfo = new()
                    {
                        appId = rt.referrerInfo.appId,
                        extraData = rt.referrerInfo.extraData,
                    },
                    scene = rt.scene,
                    chatType = rt.chatType,
                    shareTicket = rt.shareTicket,
                });
            });
        }

        private void GetUserInfo(Action<UserBody> completeCB, Action<string> errorCB)
        {
            WX.GetSetting(new GetSettingOption()
            {
                success = (GetSettingSuccessCallbackResult rt) =>
                {
                    if (rt.authSetting.ContainsKey("scope.userInfo"))
                    {
                        WX.GetUserInfo(new GetUserInfoOption()
                        {
                            lang = "zh_CN",
                            success = (GetUserInfoSuccessCallbackResult rt) =>
                            {
                                UserBody userBodyInfo = new()
                                {
                                    nickName = rt.userInfo.nickName,
                                    avatarUrl = rt.userInfo.avatarUrl,
                                    country = rt.userInfo.country,
                                    province = rt.userInfo.province,
                                    city = rt.userInfo.city,
                                    language = rt.userInfo.language,
                                    gender = rt.userInfo.gender
                                };
                                completeCB?.Invoke(userBodyInfo);
                            }
                        });
                    }
                    else
                    {
                        var button = WX.CreateUserInfoButton(0, 0, Screen.width, Screen.height, "zh_CN", false);
                        button.OnTap((WXUserInfoResponse rt) =>
                        {
                            UserBody userBodyInfo = new()
                            {
                                nickName = rt.userInfo.nickName,
                                avatarUrl = rt.userInfo.avatarUrl,
                                country = rt.userInfo.country,
                                province = rt.userInfo.province,
                                city = rt.userInfo.city,
                                language = rt.userInfo.language,
                                gender = rt.userInfo.gender
                            };
                            completeCB?.Invoke(userBodyInfo);
                            button.Destroy();
                        });
                    }
                },
                fail = (GeneralCallbackResult err) =>
                {
                    errorCB?.Invoke(err.errMsg);
                }
            });
        }

    }

}
#endif
