using System.Drawing;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    private Rectangle _stageRect = new() { Width = 750, Height = 1334 };
    private Rectangle _actionRect = new() { Width = 750, Height = 1334 };
    private Rectangle _safeRect = new() { Width = 750, Height = 1334 };

    public void Initialize()
    {
        var scale = FairyGUI.GRoot.inst.scale;
        var width = Screen.width / scale.x;
        var height = Screen.height / scale.y;


        _stageRect.Width = (int)width;
        _stageRect.Height = (int)height;
        _actionRect.Width = (int)width;
        _actionRect.Height = (int)height;
        _safeRect.Width = (int)width;
        _safeRect.Height = (int)height;
        var safe = Screen.safeArea;
        _safeRect.X = (int)safe.x;
        _safeRect.Y = (int)safe.y;
        Debug.Log(_stageRect);
        Debug.Log(_safeRect);
        Debug.Log(FairyGUI.GRoot.inst.scale);
    }

    public Rectangle ActionRect
    {
        get { return _actionRect; }
    }

    public Rectangle SafeRect
    {
        get { return _safeRect; }
    }

    public Rectangle StageRect
    {
        get { return _stageRect; }
    }
}
