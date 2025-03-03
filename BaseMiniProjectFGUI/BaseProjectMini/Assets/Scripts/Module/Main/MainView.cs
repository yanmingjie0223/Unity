using FairyGUI;
using UnityEngine.UI;
using WeChatWASM;
using UnityEngine;

public class MainView : BaseView
{

    private GButton _btnStart;
    private GButton _btnClose;
    private GButton _btnFriend;
    private GButton _btnWorld;

    public MainView() : base(new() { "main", "common" }, "MainView", ViewType.VIEW, ViewLayerType.TOP_LAYER)
    {
    }

    protected override void OnInit()
    {
        _btnStart = contentPane.GetChild("btnStart") as GButton;
        _btnClose = contentPane.GetChild("btnClose") as GButton;
        _btnFriend = contentPane.GetChild("btnFriend") as GButton;
        _btnWorld = contentPane.GetChild("btnWorld") as GButton;

        _btnStart.onClick.Add(OnClickBtn);
        _btnClose.onClick.Add(OnClickBtn);
        _btnFriend.onClick.Add(OnClickBtn);
        _btnWorld.onClick.Add(OnClickBtn);
    }

    protected override void OnShown()
    {
    }

    private void OnClickBtn(EventContext ec)
    {
        var sender = ec.sender as GButton;
        if (sender == _btnStart)
        {
            var openCanvas = GameObject.Find("Open Canvas");
            var openCanvaseScaler = openCanvas.GetComponent<CanvasScaler>();
            var RankBody = openCanvas.transform.Find("RankBody").GetComponent<RawImage>();
            var referenceResolution = openCanvaseScaler.referenceResolution;
            var p = RankBody.transform.position;
            WX.ShowOpenData(RankBody.texture, (int)p.x, Screen.height - (int)p.y, (int)((Screen.width / referenceResolution.x) * RankBody.rectTransform.rect.width), (int)((Screen.width / referenceResolution.x) * RankBody.rectTransform.rect.height));
        }
        else if (sender == _btnClose)
        {
            WX.HideOpenData();
        }
        else if (sender == _btnFriend)
        {
            var openDataContext = new WXOpenDataContext();
            openDataContext.PostMessage("{\"type\":\"showFriendsRank\"}");
        }
        else if (sender == _btnWorld)
        {
            var openDataContext = new WXOpenDataContext();
            openDataContext.PostMessage("{\"type\":\"showGroupFriendsRank\"}");
        }
    }

}
