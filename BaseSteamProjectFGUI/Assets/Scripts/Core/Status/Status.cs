using System;

public class Status<E> where E : Enum
{
    private int _status = 0;

    public void Add(E status)
    {
        _status |= 1 << status.GetHashCode();
    }

    public void Remove(E status)
    {
        _status &= ~(1 << status.GetHashCode());
    }

    public void Set(E status)
    {
        _status = 0;
        _status |= 1 << status.GetHashCode();
    }

    public void Clear()
    {
        _status = 0;
    }

    public bool IsStatus(E status)
    {
        var code = _status & (1 << status.GetHashCode());
        return code != 0;
    }

    public bool IsOnlyStatus(E status)
    {
        var sValue = 1 << status.GetHashCode();
        if (_status == sValue)
        {
            return true;
        }
        return false;
    }
}
