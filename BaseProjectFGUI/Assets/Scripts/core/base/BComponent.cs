using FairyGUI;
using FairyGUI.Utils;

public class BComponent : GComponent
{

    protected override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override void ConstructFromXML(XML xml)
    {
        ConstructFromXML(xml);
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
