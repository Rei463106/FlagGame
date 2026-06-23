using System;
using System.Collections.Generic;

/// <summary>
/// Minoを置く処理
/// </summary>
public class ConfirmPutMino : IConfirm
{  
    /// <summary>
    /// 置かれてるか判断する
    /// </summary>
    /// <param name="minoPosition"></param>
    /// <param name="minoPlace"></param>
    public void Confirm(List<(int x, int y)> minoPosition, bool[,] minoPlace)
    {
        //たぶん今後X,Yをまとめたタプルのリストができあがると思うのでそれを待ちます
        foreach (var m in minoPosition)
        {
            var cal = m.y + 1;
            if (cal > minoPlace.GetLength(1) - 1 || minoPlace[m.x, cal])//2+1>2等、一番下に来た時            
            {
               //1秒確認を後で追加する
            }
        }
    }
}
