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
}
