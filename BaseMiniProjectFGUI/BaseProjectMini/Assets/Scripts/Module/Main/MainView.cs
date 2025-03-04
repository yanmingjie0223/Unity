using FairyGUI;
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
        var wxOpenContext = WxOpenContext.GetInstance();
        if (sender == _btnStart)
        {
            wxOpenContext.ShowOpenData();
        }
        else if (sender == _btnClose)
        {
            wxOpenContext.HideOpenData();
        }
        else if (sender == _btnFriend)
        {
            wxOpenContext.SendFriendMessage();
        }
        else if (sender == _btnWorld)
        {
            wxOpenContext.SendGroupFriendMessage();
        }
    }

}
