using System.Drawing;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    private Rectangle _stageRect = new() { Width = GameConfig.initWidth, Height = GameConfig.initHeight };
    private Rectangle _actionRect = new() { Width = GameConfig.initWidth, Height = GameConfig.initHeight };
    private Rectangle _safeRect = new() { Width = GameConfig.initWidth, Height = GameConfig.initHeight };

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
