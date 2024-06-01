using FairyGUI;
using UnityEngine;
using Flame.CSV;

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
        var resManager = ResManager.GetInstance();
        LoadManager.GetInstance().LoadBundle(BundleType.Config,
        (string start) =>
        {
        },
        (float progress) =>
        {
        },
        (bool isError) =>
        {
            if (!isError)
            {
                var t = CSVManager.GetInstance().GetTable<TestTable>();
                var t1 = t[1];
                Debug.Log("load complete!");
            }
        });
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
