using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 確認時用インターフェイス
/// </summary>
public interface IConfirm
{
    public void Confirm(List<(int x, int y)> minoPosition, bool[,] minoPlace);
}
