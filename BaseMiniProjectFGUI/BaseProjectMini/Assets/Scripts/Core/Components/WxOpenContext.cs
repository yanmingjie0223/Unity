using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WeChatWASM;

public class WxOpenContext : SingletonMono<WxOpenContext>
{
    public RawImage RankBody;

    public void ShowOpenData()
    {
        /**
         * 注意这里传x,y,width,height是为了点击区域能正确点击，x,y 是距离屏幕左上角的距离，宽度传 (int)RankBody.rectTransform.rect.width是在canvas的UI Scale Mode为 Constant Pixel Size的情况下设置的。
         * 如果父元素占满整个窗口的话，pivot 设置为（0，0），rotation设置为180，则左上角就是离屏幕的距离
         * 注意这里传x,y,width,height是为了点击区域能正确点击，因为开放数据域并不是使用 Unity 进行渲染而是可以选择任意第三方渲染引擎
         * 所以开放数据域名要正确处理好事件处理，就需要明确告诉开放数据域，排行榜所在的纹理绘制在屏幕中的物理坐标系
         * 比如 iPhone Xs Max 的物理尺寸是 414 * 896，如果排行榜被绘制在屏幕中央且物理尺寸为 200 * 200，那么这里的 x,y,width,height应当是 107,348,200,200
         * x,y 是距离屏幕左上角的距离，宽度传 (int)RankBody.rectTransform.rect.width是在canvas的UI Scale Mode为 Constant Pixel Size的情况下设置的
         * 如果是Scale With Screen Size，且设置为以宽度作为缩放，则要这要做一下换算，比如canavs宽度为960，rawImage设置为200 则需要根据 referenceResolution 做一些换算
         * 不过不管是什么屏幕适配模式，这里的目的就是为了算出 RawImage 在屏幕中绝对的位置和尺寸
         */
        CanvasScaler scaler = gameObject.GetComponent<CanvasScaler>();
        var referenceResolution = scaler.referenceResolution;
        var p = RankBody.transform.position;

        WX.ShowOpenData(RankBody.texture, (int)p.x, Screen.height - (int)p.y, (int)((Screen.width / referenceResolution.x) * RankBody.rectTransform.rect.width), (int)((Screen.width / referenceResolution.x) * RankBody.rectTransform.rect.height));
    }

    public void HideOpenData()
    {
        WX.HideOpenData();
    }

    public void SendFriendMessage()
    {
        var openDataContext = new WXOpenDataContext();
        openDataContext.PostMessage("{\"type\":\"showFriendsRank\"}");
    }

    public void SendGroupFriendMessage()
    {
        var openDataContext = new WXOpenDataContext();
        openDataContext.PostMessage("{\"type\":\"showGroupFriendsRank\"}");
    }

}
