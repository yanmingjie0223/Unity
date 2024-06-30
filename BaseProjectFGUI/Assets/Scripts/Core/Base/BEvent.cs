public delegate void EvevtCallback(int evevtId, string message);

public class BEvent
{
    private int id = -1;
    private event EvevtCallback Ecb;

    public int Id()
    {
        return id;
    }

    public bool IsEmpty()
    {
        if (Ecb == null)
        {
            return true;
        }
        return false;
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

    public bool IsSameId(GameEvent _id)
    {
        if (id == (int)_id)
        {
            return true;
        }
        return false;
    }

    public void Clear()
    {
        id = -1;
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
