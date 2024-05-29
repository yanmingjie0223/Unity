public delegate void EvevtCallback(int evevtId, string message);

public class BEvent
{
    private int id;
    private event EvevtCallback Ecb;

    public int Id()
    {
        return id;
    }

    public void SetEvent(int _id, EvevtCallback _evevtCallback)
    {
        id = _id;
        Ecb = _evevtCallback;
    }

    public bool IsSame(int _id, EvevtCallback _evevtCallback)
    {
        if (id == _id)
        {
            if (_evevtCallback == Ecb)
            {
                return true;
            }
        }
        return false;
    }

    public void Clear()
    {
        id = 0;
        Ecb = null;
    }

    public void Dispatch()
    {
        Ecb?.Invoke(id, "");
    }

    public void Dispatch(string message)
    {
        Ecb?.Invoke(id, message);
    }

}
