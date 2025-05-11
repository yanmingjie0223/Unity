using FairyGUI;
using System.Collections.Generic;
using UnityEngine;

public class BaseView : BComponent
{
    private string _pkgName;
    private List<string> _pkgNames;
    private string _resName;
    private ViewType _viewType;
    private AreaType _areaType;
    private string _viewLayerType;

    private BaseModel _model;
    private BaseCtrl _ctrl;
    private IBaseViewData _viewData;
    private ViewStatus _showStatus;
    private GComponent _contentPane;
    private GLoader _bbgLoader;
    private float _bgAlpha = 0.8f;

    private bool _isInit;
    private bool _isDestroy;

    public BaseView(List<string> pkgNames, string resName, ViewType viewType, string layer)
    {
        _pkgNames = pkgNames;
        _pkgName = pkgNames[0];
        _resName = resName;
        _viewType = viewType;
        _viewLayerType = layer;

        _isInit = false;
        _isDestroy = false;
        _showStatus = ViewStatus.CLOSE;
    }

    public IBaseViewData viewData
    {
        set { _viewData = value; }
        get { return _viewData; }
    }

    public BaseModel model
    {
        set
        {
            _model = value;
            if (_ctrl != null)
            {
                _ctrl.model = value;
            }
        }
        get { return _model; }
    }

    public BaseCtrl ctrl
    {
        set
        {
            _ctrl = value;
            if (_ctrl != null)
            {
                _ctrl.view = this;
            }
        }
        get { return _ctrl; }
    }

    public GComponent contentPane
    {
        set
        {
            _contentPane = value;
            if (value != null)
            {
                AddChild(value);
            }
        }
        get { return _contentPane; }
    }

    public string layer
    {
        set { _viewLayerType = value; }
        get { return _viewLayerType; }
    }

    public ViewType viewType { get { return _viewType; } }

    public bool isDestroy
    {
        get { return displayObject == null; }
    }

    public bool isInit
    {
        get { return _isInit; }
    }

    public T GetViewData<T>() where T : IBaseViewData, new()
    {
        return (T)_viewData;
    }

    public T GetCtrl<T>() where T : BaseCtrl
    {
        return (T)_ctrl;
    }

    public bool GetShowInStage()
    {
        if (
            _showStatus == ViewStatus.CLOSE && visible == true)
        {
            return true;
        }
        return false;
    }

    public int GetLayerIndex()
    {
        var layerManager = LayerManager.GetInstance();
        var index = layerManager.GetLayerIndex(layer);
        if (displayObject != null)
        {
            var cIndx = displayObject.renderingOrder;
            return index + cIndx;
        }
        return 0;
    }

    public GLoader GetBgLoader()
    {
        return _bbgLoader;
    }

    public float GetBgAlpha()
    {
        return _bgAlpha;
    }

    public ViewStatus GetShowStatus()
    {
        return _showStatus;
    }

    public void SetBgAlpha(float alpha)
    {
        _bgAlpha = alpha;
    }

    public void SetIsTrust(bool isTrust)
    {
        _isInit = isTrust;
    }

    public void RemoveBgLoader()
    {
        if (_bbgLoader != null)
        {
            _bbgLoader.onClick.Remove(OnClickMatte);
            _bbgLoader.Dispose();
            _bbgLoader = null;
        }
    }

    public void Destroy()
    {
        if (_isDestroy)
        {
            return;
        }

        _isDestroy = true;
        _ctrl?.Destroy();
        _viewData?.Destroy();

        _pkgName = null;
        _resName = null;
        _model = null;
        _showStatus = ViewStatus.CLOSE;
        if (contentPane != null)
        {
            contentPane.Dispose();
            contentPane = null;
        }
        RemoveBgLoader();
        Dispose();
    }

    public void Show()
    {
        _showStatus = ViewStatus.SHOWING;
        if (_pkgName == "" || _resName == "")
        {
            return;
        }

        OnLoad();
    }

    public void Close()
    {
        _showStatus = ViewStatus.CLOSE;
        if (_isInit)
        {
            OnClosen();
        }
    }

    virtual protected void OnInit() { }
    virtual protected void OnShown() { }
    virtual protected void OnProgress(float percent) { }
    virtual protected void OnClosen() { }
    virtual protected void OnClickMatte() { }
    virtual protected void OnResize() { }
    virtual protected void OnShowAnimation()
    {
        OnCompleteAnimation();
    }

    protected void OnPaneRelation()
    {
        var stageManager = StageManager.GetInstance();
        var rect = stageManager.ActionRect;
        var safe = stageManager.SafeRect;
        SetSize(rect.Width, rect.Height);
        AddRelation(GRoot.inst, RelationType.Size);
        switch (_viewType)
        {
            case ViewType.VIEW:
                if (_areaType == AreaType.FULL)
                {
                    _contentPane.SetSize(rect.Width, rect.Height);
                    _contentPane.AddRelation(this, RelationType.Size);
                }
                else
                {
                    _contentPane.SetSize(safe.Width, safe.Height);
                    _contentPane.SetXY(safe.X, safe.Y);
                }
                break;
            case ViewType.WINDOW:
            case ViewType.X_WINDOW:
                _contentPane.x = (width - _contentPane.width) / 2;
                _contentPane.y = (height - _contentPane.height) / 2;
                _contentPane.AddRelation(this, RelationType.Center_Center);
                _contentPane.AddRelation(this, RelationType.Middle_Middle);
                break;
        }
    }

    protected void OnCompleteAnimation()
    {
        _showStatus = ViewStatus.SHOW;
        if (_bbgLoader != null)
        {
            _bbgLoader.onClick.Remove(OnClickMatte);
            _bbgLoader.onClick.Add(OnClickMatte);
        }
        OnShown();
    }

    private void OnLoad()
    {
        var pkg = UIPackage.GetByName(_pkgName);
        if (!_isInit || pkg == null)
        {
            LoadManager.GetInstance().LoadGroup(GameConfig.yooPackageName, GroupType.UI, null, OnProgress, (bool isError) =>
            {
                if (isError)
                {
                    Destroy();
                }
                else
                {
                    OnInitUI();
                }
            });
        }
        else
        {
            OnCompleteUI();
        }
    }

    private void OnInitUI()
    {
        if (_isDestroy)
        {
            return;
        }

        _pkgNames.ForEach(pkg =>
        {
            UIPackage.AddPackage("ui/" + pkg, OnLoadFun);
        });
        contentPane = UIPackage.CreateObject(_pkgName, _resName).asCom;
        if (contentPane == null)
        {
            Destroy();
            Debug.LogError("not found: " + _pkgName + "/" + _resName);
            return;
        }

        displayObject.cachedTransform.name = $"{contentPane.displayObject.cachedTransform.name}_Container";

        OnPaneRelation();
        OnCompleteUI();
        _isInit = true;
    }

    private void OnCompleteUI()
    {
        if (!_isInit)
        {
            OnWindowBG();
            OnInit();
        }
        OnShowAnimation();
    }

    private void OnWindowBG()
    {
        if (_viewType == ViewType.VIEW)
        {
            RemoveBgLoader();
        }
        else
        {
            if (_bbgLoader == null)
            {
                var stageManager = StageManager.GetInstance();
                _bbgLoader = new GLoader();
                var stageRect = stageManager.StageRect;
                var actionRect = stageManager.ActionRect;
                _bbgLoader.SetSize(stageRect.Width, stageRect.Height);
                _bbgLoader.SetXY(actionRect.X, actionRect.Y);
                _bbgLoader.touchable = true;
                if (_viewType == ViewType.X_WINDOW)
                {
                    _bbgLoader.url = GameConfig.matteUrl;
                    _bbgLoader.fill = FillType.ScaleFree;
                    _bbgLoader.alpha = GetBgAlpha();
                    _bbgLoader.AddRelation(this, RelationType.Size);
                }
                AddChildAt(_bbgLoader, 0);
            }
        }
    }

    private Object OnLoadFun(string name, string extension, System.Type type, out DestroyMethod destroyMethod)
    {
        destroyMethod = DestroyMethod.Unload;
        var obj = ResManager.GetInstance().GetAssetSync(GameConfig.yooPackageName, GroupType.UI, name);
        return obj;
    }

}
