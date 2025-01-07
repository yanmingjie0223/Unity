using FairyGUI;
using UnityEngine;

public class DisplayUtils
{

    public delegate void TextPopupCallback();
    public delegate void NumEffectUpdateCallback(float value);
    public delegate void NumEffectCompleteCallback();

    /// <summary>
    /// 滚动数字
    /// </summary>
    /// <param name="sNum"></param>
    /// <param name="eNum"></param>
    /// <param name="updateCb"></param>
    /// <param name="maxTimes"></param>
    public static void StartNumScrollEffect(float sNum, float eNum, NumEffectUpdateCallback updateCb, NumEffectCompleteCallback completeCb, float maxTimes = 1)
    {
        var diffNum = Mathf.Abs(eNum - sNum);
        float curTimes;
        if (diffNum < maxTimes)
        {
            curTimes = Mathf.Floor(diffNum);
        }
        else
        {
            curTimes = maxTimes;
        }

        var _timeTween = GTween.To(sNum, eNum, curTimes);
        _timeTween.SetEase(EaseType.Linear);
        _timeTween.OnUpdate(() =>
        {
            if (updateCb != null)
            {
                updateCb(_timeTween.value.x);
            }
        });
        _timeTween.OnComplete(() =>
        {
            if (completeCb != null)
            {
                completeCb();
            }
        });
    }

}
