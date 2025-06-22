using FairyGUI;
using System.Collections.Generic;
using UnityEngine;
using YooAsset;

public class BaseView : BComponent
{
    private readonly List<string> _pkgNames;
    private readonly ViewType _viewType;
    private readonly AreaType _areaType;
    private string _pkgName;
    private string _resName;
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

    public IBaseViewData ViewData
    {
        set { _viewData = value; }
        get { return _viewData; }
    }

    public BaseModel Model
    {
        set
        {
            _model = value;
            if (_ctrl != null)
            {
                _ctrl.Model = value;
            }
        }
        get { return _model; }
    }

    public BaseCtrl Ctrl
    {
        set
        {
            _ctrl = value;
            if (_ctrl != null)
            {
                _ctrl.View = this;
            }
        }
        get { return _ctrl; }
    }

    public GComponent ContentPane
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

    public string Layer
    {
        set { _viewLayerType = value; }
        get { return _viewLayerType; }
    }

    public ViewType ViewType { get { return _viewType; } }

    public bool IsDestroy
    {
        get { return displayObject == null; }
    }

    public bool IsInit
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
        var index = layerManager.GetLayerIndex(Layer);
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
        if (ContentPane != null)
        {
            ContentPane.Dispose();
            ContentPane = null;
        }
        RemoveBgLoader();
        Dispose();
        _pkgNames.ForEach(pkgName =>
        {
            ResourceConfig.groupData.TryGetValue(pkgName, out var curResList);
            if (curResList != null)
            {
                for (int i = 0; i < curResList.Count; i++)
                {
                    ResManager.GetInstance().Release(GameConfig.yooPackageName, $"{GroupTypeName.UI}_{curResList[i]}");
                }
            }
        });
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
            ResManager.GetInstance().GetResAssetAsync(
                GameConfig.yooPackageName,
                _pkgNames,
                null,
                OnProgress,
                (bool isError) =>
                {
                    if (isError)
                    {
                        Destroy();
                    }
                    else
                    {
                        OnInitUI();
                    }
                }
            );
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
        for (var i = 0; i < _pkgNames.Count; ++i)
        {
            UIPackage.AddPackage($"ui/{_pkgNames[i]}", OnLoadFun);
        }
        ContentPane = UIPackage.CreateObject(_pkgName, _resName).asCom;
        if (ContentPane == null)
        {
            Destroy();
            Debug.LogError("not found: " + _pkgName + "/" + _resName);
            return;
        }

        displayObject.cachedTransform.name = $"{ContentPane.displayObject.cachedTransform.name}_Container";

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
        var package = YooAssets.TryGetPackage(GameConfig.yooPackageName);
        if (package == null)
        {
            Debug.LogError("not download package!");
            return null;
        }

        string[] resArr = name.Split("/");
        if (resArr.Length <= 0)
        {
            return null;
        }

        var obj = ResManager.GetInstance().GetAssetSync<Object>(GameConfig.yooPackageName, GroupType.UI, resArr[^1]);
        return obj;
    }

}
