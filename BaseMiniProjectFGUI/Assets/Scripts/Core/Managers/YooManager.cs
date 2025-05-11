using System;
using System.IO;
using UnityEngine;
using YooAsset;

public class YooManager : Singleton<YooManager>
{

    public void Initialize()
    {
    }

}

public class WebDecryption : IWebDecryptionServices
{
    public WebDecryptResult LoadAssetBundle(WebDecryptFileInfo fileInfo)
    {
        // 安全起见可以拷贝一份原始数据
        byte[] copyData = new byte[fileInfo.FileData.Length];
        Buffer.BlockCopy(fileInfo.FileData, 0, copyData, 0, fileInfo.FileData.Length);

        // 实现你的解密算法
        for (int i = 0; i < copyData.Length; i++)
        {
            // 暂无解密
        }

        // 从内存中加载AssetBundle
        WebDecryptResult decryptResult = new()
        {
            Result = AssetBundle.LoadFromMemory(copyData)
        };
        return decryptResult;
    }
}

public class RemoteServices : IRemoteServices
{

    private readonly string _defaultHostServer;
    private readonly string _fallbackHostServer;

    public RemoteServices(string defaultHostServer, string fallbackHostServer)
    {
        _defaultHostServer = defaultHostServer;
        _fallbackHostServer = fallbackHostServer;
    }

    string IRemoteServices.GetRemoteMainURL(string fileName)
    {
        return $"{_defaultHostServer}/{fileName}";
    }

    string IRemoteServices.GetRemoteFallbackURL(string fileName)
    {
        return $"{_fallbackHostServer}/{fileName}";
    }

}

/// <summary>
/// 资源文件解密流
/// </summary>
public class BundleStream : FileStream
{

    public const byte KEY = 64;

    public BundleStream(string path, FileMode mode, FileAccess access, FileShare share) : base(path, mode, access, share)
    {
    }

    public BundleStream(string path, FileMode mode) : base(path, mode)
    {
    }

    public override int Read(byte[] array, int offset, int count)
    {
        var index = base.Read(array, offset, count);
        for (int i = 0; i < array.Length; i++)
        {
            array[i] ^= KEY;
        }
        return index;
    }

}
