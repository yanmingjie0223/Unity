using System.Collections.Generic;

public class EventManager : Singleton<EventManager>
{
    private readonly List<BEvent> events = new();
    private readonly List<BEvent> eventsCache = new();

    public void AddEventListener(GameEvent type, EvevtCallback evevtCallback)
    {
        var eve = GetEvent();
        eve.SetEvent((int)type, evevtCallback);
        events.Add(eve);
    }

    public void OffEventListener(GameEvent type, EvevtCallback evevtCallback)
    {
        foreach (var item in events)
        {
            if (item.IsSame((int)type, evevtCallback))
            {
                item.Clear();
                events.Remove(item);
                eventsCache.Add(item);
                break;
            }
        }
    }

    public void Dispatch(GameEvent type)
    {
        foreach (var item in events)
        {
            if (item.Id() == (int)type)
            {
                item.Dispatch();
            }
        }
    }

    public void Dispatch(GameEvent type, string message)
    {
        foreach (var item in events)
        {
            if (item.Id() == (int)type)
            {
                item.Dispatch(message);
            }
        }
    }

    private BEvent GetEvent()
    {
        BEvent eve;
        if (eventsCache.Count > 0)
        {
            eve = eventsCache[0];
            eventsCache.RemoveAt(0);
        }
        else
        {
            eve = new BEvent();
        }
        return eve;
    }
}
