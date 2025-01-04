using System.Collections.Generic;

public class EventManager : Singleton<EventManager>
{
    private readonly List<BEvent> events = new();
    private readonly List<BEvent> eventsCache = new();

    public void AddListener(GameEvent type, EvevtCallback evevtCallback)
    {
        var eve = GetEvent();
        eve.SetEvent((int)type, evevtCallback);
        events.Add(eve);
    }

    public void RemoveListener(GameEvent type, EvevtCallback evevtCallback)
    {
        foreach (var item in events)
        {
            if (item.IsSame((int)type, evevtCallback))
            {
                item.Clear();
                break;
            }
        }
    }

    public void Dispatch(GameEvent type)
    {
        for (var i = 0; i < events.Count; ++i)
        {
            var item = events[i];
            if (item.IsSameId(type))
            {
                item.Dispatch();
            }
        }

        var count = events.Count;
        for (var i = 0; i < events.Count; ++i)
        {
            var item = events[i];
            if (item.IsEmpty())
            {
                events.Remove(item);
                eventsCache.Add(item);
                --i;
                --count;
            }
        }
    }

    public void Dispatch(GameEvent type, string message)
    {
        for (var i = 0; i < events.Count; ++i)
        {
            var item = events[i];
            if (item.IsSameId(type))
            {
                item.Dispatch(message);
            }
        }

        var count = events.Count;
        for (var i = 0; i < events.Count; ++i)
        {
            var item = events[i];
            if (item.IsEmpty())
            {
                events.Remove(item);
                eventsCache.Add(item);
                --i;
                --count;
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
