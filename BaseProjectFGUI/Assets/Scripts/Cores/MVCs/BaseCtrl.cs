public class BaseCtrl
{
    private BaseModel _model;
    private BaseView _view;

    public BaseModel model { get { return _model; } set { _model = value; } }

    public BaseView view { get { return _view; } set { _view = value; } }

    public void Destroy()
    {
        _model = null;
        _view = null;
    }

    public void AddEventListener(GameEvent type, EvevtCallback evevtCallback)
    {
        EventManager.GetInstance().AddEventListener(type, evevtCallback);
    }

    public void OffEventListener(GameEvent type, EvevtCallback evevtCallback)
    {
        EventManager.GetInstance().OffEventListener(type, evevtCallback);
    }

    public void Dispatch(GameEvent type)
    {
        EventManager.GetInstance().Dispatch(type);
    }
}
