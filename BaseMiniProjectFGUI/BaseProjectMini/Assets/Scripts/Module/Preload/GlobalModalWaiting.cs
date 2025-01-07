using UnityEngine;

public class GlobalModalWaiting : BComponent
{
    private readonly FairyGUI.GImage _bar;
    private readonly float _dalyTime = 0.8f;
    private readonly Status<ViewStatus> _status;
    private float _curTime = 0f;

    public void Show(bool isInstant = false)
    {
        if (isInstant)
        {
            _curTime = _dalyTime;
            OnShow();
        }
        else
        {
            Hide();
            _status.Set(ViewStatus.SHOWING);
        }
    }

    public void Hide()
    {
        _status.Set(ViewStatus.CLOSE);
        _bar.visible = false;
    }

    override protected void OnUpdate()
    {
        base.OnUpdate();

        if (_status.IsStatus(ViewStatus.SHOWING))
        {
            _curTime -= Time.deltaTime;
            if (_curTime < 0f)
            {
                OnShow();
            }
        }

        var rotation = _bar.rotation;
        rotation += 10;
        if (rotation > 360)
        {
            rotation %= 360;
        }
        _bar.rotation = rotation;
    }

    private void OnShow()
    {
        _status.Set(ViewStatus.SHOW);
        _bar.visible = true;
    }

}
