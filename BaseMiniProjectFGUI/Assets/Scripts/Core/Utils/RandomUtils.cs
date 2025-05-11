using System;
using System.Collections.Generic;

public class RandomUtils
{

    private readonly static Random _random = new();

    /// <summary>
    /// 获取随机数 [start, end)
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public static int GetRoundCount(int start, int end)
    {
        int min;
        int max;
        if (start < end)
        {
            min = start;
            max = end;
        }
        else
        {
            min = end;
            max = start;
        }
        return _random.Next(min, max);
    }

    /// <summary>
    /// 获取列表中的一项
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="isPutback"></param>
    /// <returns></returns>
    public static T GetRoundItem<T>(List<T> list, bool isPutback = true)
    {
        if (list == null || list.Count == 0)
        {
            return default;
        }

        var index = GetRoundCount(0, list.Count);
        var item = list[index];
        if (!isPutback)
        {
            list.RemoveAt(index);
        }

        return item;
    }

}
