using UnityEngine;

public class MathUtils
{

    /// <summary>
    /// [0-1]区间获取二次贝塞尔曲线上某点坐标
    /// </summary>
    /// <param name="p0">起点</param>
    /// <param name="p1">控制点</param>
    /// <param name="p2">终点</param>
    /// <param name="t">时间</param>
    /// <param name="point"></param>
    /// <returns></returns>
    public static Vector2 GetBezierPoint(Vector2 p0, Vector2 p1, Vector2 p2, float t, ref Vector2 point)
    {
        point.x = (1f - t) * (1f - t) * p0.x + 2f * t * (1f - t) * p1.x + t * t * p2.x;
        point.y = (1f - t) * (1f - t) * p0.y + 2f * t * (1f - t) * p1.y + t * t * p2.y;
        return point;
    }

}
