using FairyGUI;

public class MainView : BaseView
{

    private GButton btnBag;
    private GButton btnVideo;

    public MainView() : base(new() { "main", "common" }, "MainView", ViewType.VIEW, ViewLayerType.MIDDLE_LAYER)
    {
    }

    protected override void OnInit()
    {
        btnBag = contentPane.GetChild("btnBag").asButton;
        btnBag.onClick.Add(OnClickBag);
        btnVideo = contentPane.GetChild("btnVideo").asButton;
        btnVideo.onClick.Add(OnClickVideo);
    }

    protected override void OnShown()
    {
        
    }

    private void OnClickBag()
    {
        ViewManager.GetInstance().Show(typeof(BagView));
    }

    private void OnClickVideo()
    {
        ViewManager.GetInstance().Close(typeof(MainView));
        ViewManager.GetInstance().Show(typeof(VideoView));
    }
}
