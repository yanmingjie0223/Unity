public class LayerManager : Singleton<LayerManager>
{
    public void Initialize()
    {
        ViewLayer.Initialize();

        var rootCom = FairyGUI.GRoot.inst;
        rootCom.AddChild(ViewLayer.BACKSTAGE_COMPONENT);
        rootCom.AddChild(ViewLayer.BOTTOM_COMPONENT);
        rootCom.AddChild(ViewLayer.MIDDLE_COMPONENT);
        rootCom.AddChild(ViewLayer.TOP_COMPONENT);
        rootCom.AddChild(ViewLayer.WINDOW_COMPONENT);
        rootCom.AddChild(ViewLayer.GUIDE_COMPONENT);
        rootCom.AddChild(ViewLayer.MAX_COMPONENT);
    }

    public FairyGUI.GComponent GetLayer(string layer)
    {
        FairyGUI.GComponent layerCom;
        if (layer == ViewLayerType.BACKSTAGE_LAYER)
        {
            layerCom = ViewLayer.BACKSTAGE_COMPONENT;
        }
        else if (layer == ViewLayerType.BOTTOM_LAYER)
        {
            layerCom = ViewLayer.BOTTOM_COMPONENT;
        }
        else if (layer == ViewLayerType.MIDDLE_LAYER)
        {
            layerCom = ViewLayer.MIDDLE_COMPONENT;
        }
        else if (layer == ViewLayerType.TOP_LAYER)
        {
            layerCom = ViewLayer.TOP_COMPONENT;
        }
        else if (layer == ViewLayerType.WINDOW_LAYER)
        {
            layerCom = ViewLayer.WINDOW_COMPONENT;
        }
        else if (layer == ViewLayerType.GUIDE_LAYER)
        {
            layerCom = ViewLayer.GUIDE_COMPONENT;
        }
        else if (layer == ViewLayerType.MAX_LAYER)
        {
            layerCom = ViewLayer.MAX_COMPONENT;
        }
        else
        {
            layerCom = null;
        }
        return layerCom;
    }

    public int GetLayerIndex(string layer)
    {
        int index;
        if (layer == ViewLayerType.BACKSTAGE_LAYER)
        {
            index = 1000;
        }
        else if (layer == ViewLayerType.BOTTOM_LAYER)
        {
            index = 2000;
        }
        else if (layer == ViewLayerType.MIDDLE_LAYER)
        {
            index = 3000;
        }
        else if (layer == ViewLayerType.TOP_LAYER)
        {
            index = 4000;
        }
        else if (layer == ViewLayerType.WINDOW_LAYER)
        {
            index = 5000;
        }
        else if (layer == ViewLayerType.GUIDE_LAYER)
        {
            index = 6000;
        }
        else if (layer == ViewLayerType.MAX_LAYER)
        {
            index = 7000;
        }
        else
        {
            index = 0;
        }
        return index;
    }
}
