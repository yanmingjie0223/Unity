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

    public void AddListener(GameEvent type, EvevtCallback evevtCallback)
    {
        EventManager.GetInstance().AddListener(type, evevtCallback);
    }

    public void RemoveListener(GameEvent type, EvevtCallback evevtCallback)
    {
        EventManager.GetInstance().RemoveListener(type, evevtCallback);
    }

    public void Dispatch(GameEvent type)
    {
        EventManager.GetInstance().Dispatch(type);
    }

    public void Dispatch(GameEvent type, string message)
    {
        EventManager.GetInstance().Dispatch(type, message);
    }
}
