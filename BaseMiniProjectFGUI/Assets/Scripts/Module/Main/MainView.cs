using Assets.Scripts.Platform;
using FairyGUI;

public class MainView : BaseView
{

    private GButton _btnStart;
    private GButton _btnClose;
    private GButton _btnFriend;
    private GButton _btnWorld;
    private GButton _btnBag;

    public MainView() : base(new() { "main", "common" }, "MainView", ViewType.VIEW, ViewLayerType.TOP_LAYER)
    {
    }

    protected override void OnInit()
    {
        _btnStart = ContentPane.GetChild("btnStart") as GButton;
        _btnClose = ContentPane.GetChild("btnClose") as GButton;
        _btnFriend = ContentPane.GetChild("btnFriend") as GButton;
        _btnWorld = ContentPane.GetChild("btnWorld") as GButton;
        _btnBag = ContentPane.GetChild("btnBag") as GButton;

        _btnStart.onClick.Add(OnClickBtn);
        _btnClose.onClick.Add(OnClickBtn);
        _btnFriend.onClick.Add(OnClickBtn);
        _btnWorld.onClick.Add(OnClickBtn);
        _btnBag.onClick.Add(OnClickBtn);
    }

    protected override void OnShown()
    {
    }

    private void OnClickBtn(EventContext ec)
    {
        var sender = ec.sender as GButton;
        var wxOpenContext = PlatformSDK.GetInstance().GetOpenContext();
        if (sender == _btnStart)
        {
            wxOpenContext.Show();
        }
        else if (sender == _btnClose)
        {
            wxOpenContext.Hide();
        }
        else if (sender == _btnFriend)
        {
            wxOpenContext.SendFriendMessage();
        }
        else if (sender == _btnWorld)
        {
            wxOpenContext.SendGroupFriendMessage();
        }
        else if (sender == _btnBag)
        {
            ViewManager.GetInstance().Show(typeof(BagView));
        }
    }

}
