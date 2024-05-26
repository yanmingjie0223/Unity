using FairyGUI;

public class BagView : BaseView
{

    private GButton _btn;
    private GRichTextField _tf;

    public BagView() : base(new() { "bag", "common", "preload" }, "BagView", ViewType.X_WINDOW, ViewLayerType.WINDOW_LAYER)
    {
    }

    protected override void OnInit()
    {
        _tf = contentPane.GetChild("tf").asRichTextField;
        _btn = contentPane.GetChild("closeBtn").asButton;
        _btn.onClick.Add(OnClickBtn);
    }

    protected override void OnShown()
    {
        _tf.text = "繁城明月夜，唯风可做伴。";
        TypingEffect tef = new(_tf);
        tef.Start();
        tef.PrintAll(0.1f);
    }

    private void OnClickBtn()
    {
        ViewManager.GetInstance().Close(typeof(BagView));
    }
}
