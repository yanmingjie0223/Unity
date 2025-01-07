using System;
using System.Collections;
using UnityEngine;
using YooAsset;

public class LoadManager : MonoBehaviour
{
    private static LoadManager _instance;
    public static LoadManager GetInstance()
    {
        {
            if (_instance == null)
            {
                return new GameObject("Load Manager").AddComponent<LoadManager>();
            }
            else
            {
                return _instance;
            }
        }
    }

    public void Initialize()
    {
        YooAssets.Initialize();
    }
    public void Load(string pkgName, string resName, Action<string> start, Action<float> progress, Action<bool> end)
    {
        StartCoroutine(LoadRes(pkgName, resName, start, progress, end));
    }

    public void LoadGroup(string pkgName, GroupType groupType, Action<string> start, Action<float> progress, Action<bool> end)
    {
        string groupName = PathUtils.GetGroupName(groupType);
        StartCoroutine(LoadGroupRes(pkgName, groupName, start, progress, end));
    }

    public void LoadGroup(string pkgName, string groupName, Action<string> start, Action<float> progress, Action<bool> end)
    {
        StartCoroutine(LoadGroupRes(pkgName, groupName, start, progress, end));
    }

    public void LoadPackage(string pkgName, EPlayMode playMode, Action<string> start, Action<float> progress, Action<bool> end)
    {
        StartCoroutine(LoadPackageRes(pkgName, playMode, start, progress, end));
    }

    private IEnumerator LoadRes(string pkgName, string resName, Action<string> start, Action<float> progress, Action<bool> end)
    {
        start?.Invoke("loading start");
        var package = YooAssets.TryGetPackage(pkgName);
        if (package == null)
        {
            end?.Invoke(true);
        }
        else
        {
            var handler = package.LoadAssetAsync(resName);
            progress?.Invoke(handler.Progress);
            yield return handler;

            if (handler.Status != EOperationStatus.Succeed)
            {
                end?.Invoke(true);
            }
            else
            {
                end?.Invoke(false);
            }
        }
    }

    private IEnumerator LoadGroupRes(string pkgName, string groupName, Action<string> start, Action<float> progress, Action<bool> end)
    {
        start?.Invoke("loading start");
        var package = YooAssets.TryGetPackage(pkgName);
        if (package == null)
        {
            end?.Invoke(true);
        }
        else
        {
            // ������Ҫ�����Ż������ļ�����
            AssetInfo[] assetInfos = package.GetAssetInfos(groupName);
            int maxCount = assetInfos.Length;
            int count = 0;
            for (int i = 0; i < maxCount; i++)
            {
                var assetInfo = assetInfos[i];
                var handler = package.LoadAssetAsync(assetInfo.Address);
                yield return handler;
                var status = handler.Status;
                handler.Release();
                if (status != EOperationStatus.Succeed)
                {
                    end?.Invoke(true);
                    break;
                }
                else
                {
                    ++count;
                    progress?.Invoke(count / maxCount);
                    if (count >= maxCount)
                    {
                        end?.Invoke(false);
                    }
                }
            }
        }
    }

    private IEnumerator LoadPackageRes(string pkgName, EPlayMode playMode, Action<string> start, Action<float> progress, Action<bool> end)
    {
        start?.Invoke("loading start");
        // ������Դ������
        var package = YooAssets.TryGetPackage(pkgName);
        package ??= YooAssets.CreatePackage(pkgName);
        // �༭���µ�ģ��ģʽ
        InitializationOperation initializationOperation = null;
        if (playMode == EPlayMode.EditorSimulateMode)
        {
            var initParameters = new EditorSimulateModeParameters();
            initParameters.SimulateManifestFilePath = EditorSimulateModeHelper.SimulateBuild(pkgName);
            initializationOperation = package.InitializeAsync(initParameters);
        }
        // ��������ģʽ
        if (playMode == EPlayMode.OfflinePlayMode)
        {
            var initParameters = new OfflinePlayModeParameters();
            initializationOperation = package.InitializeAsync(initParameters);
        }
        // ��������ģʽ
        if (playMode == EPlayMode.HostPlayMode)
        {
            string serverUrl = PathUtils.GetHostServerURL();
            var initParameters = new HostPlayModeParameters
            {
                RemoteServices = new RemoteServices(serverUrl, serverUrl)
            };
            initializationOperation = package.InitializeAsync(initParameters);
        }
        // WebGL����ģʽ
        if (playMode == EPlayMode.WebPlayMode)
        {
            YooAssets.SetCacheSystemDisableCacheOnWebGL();

            string serverUrl = PathUtils.GetHostServerURL();
            var initParameters = new WebPlayModeParameters
            {
                BuildinQueryServices = new BuildQueryServices(),
                RemoteServices = new RemoteServices(serverUrl, serverUrl)
            };
            initializationOperation = package.InitializeAsync(initParameters);
        }
        // �ȴ����ؽ��
        progress?.Invoke(initializationOperation.Progress);
        yield return initializationOperation;
        // �����ʼ��ʧ�ܵ�����ʾ����
        if (initializationOperation.Status != EOperationStatus.Succeed)
        {
            end?.Invoke(true);
        }
        else
        {
            // �汾
            var operation = package.UpdatePackageVersionAsync();
            progress?.Invoke(operation.Progress);
            yield return operation;

            if (operation.Status != EOperationStatus.Succeed)
            {
                end?.Invoke(true);
            }
            else
            {
                // ��Դ�б��ļ�
                var updateOpe = package.UpdatePackageManifestAsync(operation.PackageVersion);
                progress?.Invoke(updateOpe.Progress);
                yield return updateOpe;

                if (operation.Status != EOperationStatus.Succeed)
                {
                    end?.Invoke(true);
                }
                else
                {
                    end?.Invoke(false);
                }
            }
        }
    }

    protected virtual void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

}
