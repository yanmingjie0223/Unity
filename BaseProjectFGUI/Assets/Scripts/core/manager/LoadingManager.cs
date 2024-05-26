public class LoadingManager : Singleton<LoadingManager>
{
    private GlobalModalWaiting _waiting;
    private int _waitingCount;

    public static GlobalModalWaiting GetLoading()
    {
        var loading = FairyGUI.UIPackage.CreateObject("preload", "GlobalModalWaiting") as GlobalModalWaiting;
        return loading;
    }

    public static void FreeLoading(GlobalModalWaiting gmw)
    {
        gmw.Hide();
    }

    public void Initialize()
    {
        _waiting = LoadingManager.GetLoading();
        _waiting.SetSize(1334, 750);
    }

    public void Show(bool isInstant = false)
    {
        ++_waitingCount;
        if (_waiting.parent != null)
        {
            var layerManager = LayerManager.GetInstance();
            var layer = layerManager.GetLayer(ViewLayerType.MAX_LAYER);
            layer.AddChild(_waiting);
            _waiting.Show(isInstant);
        }
    }

    public void Close()
    {
        if (_waitingCount < 1)
        {
            return;
        }

        --_waitingCount;
        if (_waitingCount <= 0)
        {
            CloseAll();
        }
    }

    public void CloseAll()
    {
        _waitingCount = 0;
        _waiting.Hide();
    }

}
