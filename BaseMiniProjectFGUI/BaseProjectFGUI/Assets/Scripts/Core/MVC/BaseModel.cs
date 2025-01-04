
public abstract class BaseModel
{
    public void Destroy() { }
    public void Clear() { }
    virtual public void Initialize() { }

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
