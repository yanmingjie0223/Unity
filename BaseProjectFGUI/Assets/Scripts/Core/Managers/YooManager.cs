﻿using System.IO;
using UnityEngine;
using YooAsset;

public class YooManager : Singleton<YooManager>
{

    public void Initialize()
    {

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

public class FileStreamDecryption : IDecryptionServices
{
    /// <summary>
    /// 同步方式获取解密的资源包对象
    /// 注意：加载流对象在资源包对象释放的时候会自动释放
    /// </summary>
    AssetBundle IDecryptionServices.LoadAssetBundle(DecryptFileInfo fileInfo, out Stream managedStream)
    {
        BundleStream bundleStream = new BundleStream(fileInfo.FileLoadPath, FileMode.Open, FileAccess.Read, FileShare.Read);
        managedStream = bundleStream;
        return AssetBundle.LoadFromStream(bundleStream, fileInfo.FileLoadCRC, GetManagedReadBufferSize());
    }

    /// <summary>
    /// 异步方式获取解密的资源包对象
    /// 注意：加载流对象在资源包对象释放的时候会自动释放
    /// </summary>
    AssetBundleCreateRequest IDecryptionServices.LoadAssetBundleAsync(DecryptFileInfo fileInfo, out Stream managedStream)
    {
        BundleStream bundleStream = new BundleStream(fileInfo.FileLoadPath, FileMode.Open, FileAccess.Read, FileShare.Read);
        managedStream = bundleStream;
        return AssetBundle.LoadFromStreamAsync(bundleStream, fileInfo.FileLoadCRC, GetManagedReadBufferSize());
    }

    private static uint GetManagedReadBufferSize()
    {
        return 1024;
    }
}

/// <summary>
/// 资源文件偏移加载解密类
/// </summary>
public class FileOffsetDecryption : IDecryptionServices
{
    /// <summary>
    /// 同步方式获取解密的资源包对象
    /// 注意：加载流对象在资源包对象释放的时候会自动释放
    /// </summary>
    AssetBundle IDecryptionServices.LoadAssetBundle(DecryptFileInfo fileInfo, out Stream managedStream)
    {
        managedStream = null;
        return AssetBundle.LoadFromFile(fileInfo.FileLoadPath, fileInfo.FileLoadCRC, GetFileOffset());
    }

    /// <summary>
    /// 异步方式获取解密的资源包对象
    /// 注意：加载流对象在资源包对象释放的时候会自动释放
    /// </summary>
    AssetBundleCreateRequest IDecryptionServices.LoadAssetBundleAsync(DecryptFileInfo fileInfo, out Stream managedStream)
    {
        managedStream = null;
        return AssetBundle.LoadFromFileAsync(fileInfo.FileLoadPath, fileInfo.FileLoadCRC, GetFileOffset());
    }

    private static ulong GetFileOffset()
    {
        return 32;
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