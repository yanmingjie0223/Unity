using FairyGUI;
using UnityEditor;
using UnityEngine.UI;
using WeChatWASM;
using UnityEngine;

public class MainView : BaseView
{

    private GButton _btnStart;

    public MainView() : base(new() { "main", "common" }, "MainView", ViewType.VIEW, ViewLayerType.TOP_LAYER)
    {
    }

    protected override void OnInit()
    {
        _btnStart = contentPane.GetChild("btnStart") as GButton;

        _btnStart.onClick.Add(OnClickBtn);
    }

    protected override void OnShown()
    {
    }

    private void OnClickBtn()
    {

        var openCanvas = GameObject.Find("Open Canvas");
        var openCanvaseScaler = openCanvas.GetComponent<CanvasScaler>();
        var RankBody = openCanvas.transform.Find("RankBody").GetComponent<RawImage>();
        var referenceResolution = openCanvaseScaler.referenceResolution;
        var p = RankBody.transform.position;
        WX.ShowOpenData(RankBody.texture, (int)p.x, Screen.height - (int)p.y, (int)((Screen.width / referenceResolution.x) * RankBody.rectTransform.rect.width), (int)((Screen.width / referenceResolution.x) * RankBody.rectTransform.rect.height));

        var openDataContext = new WXOpenDataContext();
        openDataContext.PostMessage("{\"type\":\"showFriendsRank\"}");
    }

}
