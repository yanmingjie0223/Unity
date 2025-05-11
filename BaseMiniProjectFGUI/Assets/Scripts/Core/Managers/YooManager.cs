using System.IO;
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

public class BuildQueryServices : IBuildinQueryServices
{

    public bool QueryStreamingAssets(string packageName, string fileName)
    {
        return false;
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
