using UnityEngine;

/// <summary>
/// 確認集
/// </summary>
public class MinoConfirm : MinoArray
{
    /// <summary>左に行けるか調べる</summary>
    public static bool JudgeLeft(Vector2 v)
    {
        var x = v.x - 1;

        foreach (var item in MArraySetting)
        {
            if (item._wallBlock == new Vector2(x, v.y) && !item.IsExist)//値が存在し、そこには何もなければ
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>右に行けるか調べる</summary>
    public static bool JudgeRight(Vector2 v)
    {
        var x = v.x + 1;

        foreach (var item in MArraySetting)
        {
            if (item._wallBlock == new Vector2(x, v.y) && !item.IsExist)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>下に行けるか調べる</summary>
    public static bool JudgeUnder(Vector2 v)
    {
        var y = v.y - 1;

        foreach (var item in MArraySetting)
        {
            if (item._wallBlock == new Vector2(v.x, y) && !item.IsExist)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>反時計回り</summary>
    public static bool JudgeLeftRotate(Vector2 involve, Vector2 rotate, out Vector2 result)
    {
        //中心点基準にする
        var rx = involve.x - rotate.x;
        var ry = involve.y - rotate.y;

        //左に90度回転させる
        var rx2 = -ry;
        var ry2 = rx;

        //最終的な数値
        var fx = rotate.x + rx2;
        var fy = rotate.y + ry2;

        foreach (var item in MArraySetting)
        {
            if (item._wallBlock == new Vector2(fx, fy) && !item.IsExist)
            {
                result = new Vector2(fx, fy);
                return true;
            }
        }

        result = Vector2.zero;
        return false;
    }

    /// <summary>時計回り</summary>
    public static bool JudgeRightRotate(Vector2 involve, Vector2 rotate, out Vector2 result)
    {
        //中心点基準にする
        var rx = involve.x - rotate.x;
        var ry = involve.y - rotate.y;

        //右に90度回転させる
        var rx2 = ry;
        var ry2 = -rx;

        //最終的な数値
        var fx = rotate.x + rx2;
        var fy = rotate.y + ry2;

        foreach (var item in MArraySetting)
        {
            if (item._wallBlock == new Vector2(fx, fy) && !item.IsExist)
            {
                result = new Vector2(fx, fy);
                return true;
            }
        }

        result = Vector2.zero;
        return false;
    }
}
