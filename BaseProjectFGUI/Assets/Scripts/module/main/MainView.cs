using FairyGUI;
using UnityEngine;

public class MainView : BaseView
{

    private GButton _btn;

    public MainView() : base(new() { "main", "common" }, "MainView", ViewType.VIEW, ViewLayerType.MIDDLE_LAYER)
    {
    }

    protected override void OnInit()
    {
        _btn = contentPane.GetChild("btn").asButton;
        _btn.onClick.Add(OnClickBtn);
    }

    protected override void OnShown()
    {
        var resManager = ResManager.GetInstance();
        LoadManager.GetInstance().LoadBundle(BundleType.Main, (string start) =>
        {
        }, (float progress) =>
        {
        }, (bool isError) =>
        {
            if (!isError)
            {
                Debug.Log("load complete!");
            }
        });
    }

    private void OnClickBtn()
    {
        ViewManager.GetInstance().Show(typeof(BagView));
    }
}
