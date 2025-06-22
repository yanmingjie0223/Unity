using FairyGUI;

public class BagView : BaseView
{

    private GButton _btnReturn;

    public BagView() : base(new() { "bag", "common" }, "BagView", ViewType.VIEW, ViewLayerType.TOP_LAYER)
    {
    }

    protected override void OnInit()
    {
        _btnReturn = ContentPane.GetChild("btnReturn") as GButton;

        _btnReturn.onClick.Add(OnClickBtn);
    }

    protected override void OnShown()
    {
    }

    private void OnClickBtn(EventContext ec)
    {
        var sender = ec.sender as GButton;
        if (sender == _btnReturn)
        {
            ViewManager.GetInstance().Close(typeof(BagView));
        }
    }

}
