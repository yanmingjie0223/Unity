using Newtonsoft.Json;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class HttpManager : SingletonMono<HttpManager>
{

    public void Send(string routerUrl, C2S_Base data, Action<string> complete, Action<string> fail)
    {
        StartCoroutine(OnSend(routerUrl, data, complete, fail));
    }

    private IEnumerator OnSend(string routerUrl, C2S_Base data, Action<string> complete, Action<string> fail)
    {
        // Post请求的地址
        string url = GameConfig.httpRoot + routerUrl;
        // Post请求的参数
        WWWForm form = new();
        form.AddField("data", JsonConvert.SerializeObject(data, Formatting.Indented));
        UnityWebRequest webRequest = UnityWebRequest.Post(url, form);
        // 发送请求
        yield return webRequest.SendWebRequest();
        if (string.IsNullOrEmpty(webRequest.error))
        {
            //Post的请求成功
            complete.Invoke(webRequest.downloadHandler.text);
        }
        else
        {
            //Post的请求失败
            fail?.Invoke("");
        }
    }

}
