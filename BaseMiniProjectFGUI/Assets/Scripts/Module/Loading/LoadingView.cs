using FairyGUI;

public class LoadingView : BaseView
{

    private GProgressBar _progressLoad;

    public LoadingView() : base(new() { "loading", "common" }, "LoadingView", ViewType.VIEW, ViewLayerType.MIDDLE_LAYER)
    {
    }

    public void SetProgress(float progress)
    {
        if (_progressLoad != null)
        {
            _progressLoad.value = progress * 100;
        }
    }

    protected override void OnInit()
    {
        _progressLoad = ContentPane.GetChild("progressLoad").asProgress;
    }

    protected override void OnShown()
    {
    }

}
