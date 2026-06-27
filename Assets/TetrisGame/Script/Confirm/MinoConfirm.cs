/// <summary>
/// 確認集
/// </summary>
public class MinoConfirm : MinoArraySetting
{
    /// <summary>左に行けるか調べる</summary>
    public static bool JudgeLeft(int line, int corumn)
    {
        var c = corumn - 1;

        if (c < 0 || MinoArray[line, c]) return false;
        else return true;
    }

    /// <summary>右に行けるか調べる</summary>
    public static bool JudgeRight(int line, int corumn)
    {
        var c = corumn + 1;

        if (c > MinoArray.GetLength(1) || MinoArray[line, c]) return false;
        else return true;
    }

    /// <summary>下に行けるか調べる</summary>
    public static bool JudgeUnder(int line, int corumn)
    {
        var u = line + 1;

        if (u > MinoArray.GetLength(0) || MinoArray[line, u]) return false;
        else return true;
    }

    /// <summary>反時計回り</summary>
    public static bool JudgeLeftRotate(int line, int corumn, int rotateX, int rotateY)
    {

    }

    /// <summary>時計回り</summary>
    public static bool JudgeRightRotate(int line, int corumn, int rotateX, int rotateY)
    {
        //中心点基準にする
        var rx = corumn - rotateX;
        var ry = line - rotateY;

        //右に90度回転させる
        var rx2 = ry;
        var ry2 = -ry;

        //最終的な数値
        var fx = rotateX + rx2;
        var fy = rotateY + ry2;

        if (fx < 0 || fx > MinoArray.GetLength(1)|| fy > MinoArray.GetLength(0) || MinoArray[fx, fy]) return false;
        else return true;
    }
}
