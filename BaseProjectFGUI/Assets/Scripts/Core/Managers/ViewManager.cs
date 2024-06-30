using System;
using System.Collections.Generic;

public class ViewManager : Singleton<ViewManager>
{
    private bool _isPause;
    private Type _currView;
    private readonly Dictionary<string, BaseView> _views = new();
    private readonly List<Type> _willViews = new();
    private readonly List<Type> _willModels = new();
    private readonly List<Type> _willCtrls = new();
    private readonly List<IBaseViewData> _willDatas = new();
    private readonly List<string> _willLayers = new();

    public BaseView Show(
        Type V,
        Type M,
        Type C,
        IBaseViewData viewData = null,
        ViewShowType showType = ViewShowType.MULTI_VIEW,
        string layer = ""
    )
    {
        if (showType == ViewShowType.SINGLETON_VIEW)
        {
            _willViews.Add(V);
            _willModels.Add(M);
            _willCtrls.Add(C);
            _willDatas.Add(viewData);
            _willLayers.Add(layer);
            return NextShow();
        }

        var key = V.Name;
        _views.TryGetValue(key, out BaseView view);
        if (view != null && !view.isDestroy)
        {
            if (view.viewData != null && view.viewData != viewData)
            {
                view.viewData.Destroy();
            }
            view.viewData = viewData;
        }
        else
        {
            var ctrl = (BaseCtrl)Activator.CreateInstance(C);
            var model = ModelManager.GetInstance().GetModel(M.Name);
            view = (BaseView)Activator.CreateInstance(V);
            view.model = model;
            view.ctrl = ctrl;
            view.viewData = viewData;
            _views.Add(key, view);
        }

        OnShow(key, view, layer);

        return view;
    }

    public BaseView Show(
        Type V,
        Type M,
        IBaseViewData viewData = null,
        ViewShowType showType = ViewShowType.MULTI_VIEW,
        string layer = ""
    )
    {
        if (showType == ViewShowType.SINGLETON_VIEW)
        {
            _willViews.Add(V);
            _willModels.Add(M);
            _willCtrls.Add(null);
            _willDatas.Add(viewData);
            _willLayers.Add(layer);
            return NextShow();
        }

        var key = V.Name;
        _views.TryGetValue(key, out BaseView view);
        if (view != null && !view.isDestroy)
        {
            if (view.viewData != null && view.viewData != viewData)
            {
                view.viewData.Destroy();
            }
            view.viewData = viewData;
        }
        else
        {
            var model = ModelManager.GetInstance().GetModel(M.Name);
            view = (BaseView)Activator.CreateInstance(V);
            view.model = model;
            view.viewData = viewData;
            _views.Add(key, view);
        }

        OnShow(key, view, layer);

        return view;
    }

    public BaseView Show(
        Type V,
        IBaseViewData viewData = null,
        ViewShowType showType = ViewShowType.MULTI_VIEW,
        string layer = ""
    )
    {
        if (showType == ViewShowType.SINGLETON_VIEW)
        {
            _willViews.Add(V);
            _willModels.Add(null);
            _willCtrls.Add(null);
            _willDatas.Add(viewData);
            _willLayers.Add(layer);
            return NextShow();
        }

        var key = V.Name;
        _views.TryGetValue(key, out BaseView view);
        if (view != null && !view.isDestroy)
        {
            if (view.viewData != null && view.viewData != viewData)
            {
                view.viewData.Destroy();
            }
            view.viewData = viewData;
        }
        else
        {
            view = (BaseView)Activator.CreateInstance(V);
            view.viewData = viewData;
            _views.Add(key, view);
        }

        OnShow(key, view, layer);

        return view;
    }

    public void Close(Type V, bool isDestroy = true)
    {
        var key = V.Name;
        if (_views == null || !_views.ContainsKey(key))
        {
            return;
        }

        var view = _views[key];
        view.Close();

        view.RemoveFromParent();
        if (isDestroy)
        {
            _views.Remove(key);
            view.Destroy();
        }

        EventManager.GetInstance().Dispatch(GameEvent.VIEW_CLOSE, key);
        if (_currView == V)
        {
            _currView = null;
            NextShow();
        }
    }

    public void SwitchLayer(Type V, string layer, int index)
    {
        var view = GetView(V);
        if (view != null && view.layer != layer)
        {
            view.RemoveFromParent();
            view.layer = layer;
            var layerCom = LayerManager.GetInstance().GetLayer(layer);
            layerCom.AddChildAt(view, index);
        }
    }

    public void SwitchLayer(Type V, string layer)
    {
        var view = GetView(V);
        if (view != null && view.layer != layer)
        {
            view.RemoveFromParent();
            view.layer = layer;
            var layerCom = LayerManager.GetInstance().GetLayer(layer);
            layerCom.AddChild(view);
        }
    }

    public void SetIsPause(bool isPause)
    {
        _isPause = isPause;
        if (!_isPause)
        {
            NextShow();
        }
    }

    public bool GetIsPause()
    {
        return _isPause;
    }

    public BaseView GetView(Type V)
    {
        var key = V.Name;
        if (_views == null || !_views.ContainsKey(key))
        {
            return null;
        }
        return _views[key];
    }

    public V GetView<V>() where V : BaseView
    {
        var VC = typeof(V);
        var key = VC.Name;
        if (_views == null || !_views.ContainsKey(key))
        {
            return null;
        }
        return (V)_views[key];
    }

    private void OnShow(string key, BaseView view, string layer)
    {
        var layerManager = LayerManager.GetInstance();
        var eventManger = EventManager.GetInstance();
        if (layer == "")
        {
            layer = view.layer;
        }

        var layerCom = layerManager.GetLayer(layer);
        layerCom.AddChild(view);
        view.Show();

        eventManger.Dispatch(GameEvent.VIEW_SHOW, key);
    }

    private BaseView NextShow()
    {
        if (_currView == null || _isPause)
        {
            return null;
        }

        if (_willViews.Count == 0)
        {
            EventManager.GetInstance().Dispatch(GameEvent.WINDOW_CLOSE);
            return null;
        }

        var wView = _willViews[0];
        var wModel = _willModels[0];
        var wCtrl = _willCtrls[0];
        var wData = _willDatas[0];
        var wLayer = _willLayers[0];
        _willViews.Remove(wView);
        _willModels.Remove(wModel);
        _willCtrls.Remove(wCtrl);
        _willDatas.Remove(wData);
        _willLayers.Remove(wLayer);
        _currView = wView;

        BaseView view;
        if (wView != null && wModel != null && wCtrl != null)
        {
            view = Show(wView, wModel, wCtrl, wData, ViewShowType.MULTI_VIEW, wLayer);
        }
        else if (wView != null && wModel != null)
        {
            view = Show(wView, wModel, wData, ViewShowType.MULTI_VIEW, wLayer);
        }
        else
        {
            view = Show(wView, wData, ViewShowType.MULTI_VIEW, wLayer);
        }

        return view;
    }

}
