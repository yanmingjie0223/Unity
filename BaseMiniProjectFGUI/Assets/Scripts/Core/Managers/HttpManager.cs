using System;
using System.Collections;
using UnityEngine.Networking;

public class HttpManager : SingletonDontDestroyMono<HttpManager>
{

    public void SendBuffer(string routerUrl, byte[] buffer, Action<byte[]> complete = null, Action<string> fail = null)
    {
        StartCoroutine(OnSendBuffer(routerUrl, buffer, complete, fail));
    }

    private IEnumerator OnSendBuffer(string routerUrl, byte[] buffer, Action<byte[]> complete, Action<string> fail)
    {
        if (string.IsNullOrEmpty(GameConfig.httpRoot))
        {
            yield break;
        }

        string url = GameConfig.httpRoot + routerUrl;
        UnityWebRequest request = new(url, "POST")
        {
            uploadHandler = new UploadHandlerRaw(buffer),
            downloadHandler = new DownloadHandlerBuffer()
        };
        request.SetRequestHeader("Content-Type", "application/octet-stream");

        yield return request.SendWebRequest();

        if (
            request.result == UnityWebRequest.Result.ConnectionError ||
            request.result == UnityWebRequest.Result.ProtocolError
        )
        {
            fail?.Invoke("");
        }
        else
        {
            complete?.Invoke(request.downloadHandler.data);
        }
        request.Dispose();
    }

}
