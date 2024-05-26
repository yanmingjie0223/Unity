
public abstract class BaseModel
{
    public static string key = "";
    public void Destroy() { }
    public void Clear() { }
    public void Initialize() { }

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
