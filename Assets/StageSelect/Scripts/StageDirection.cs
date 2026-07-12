using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

/// <summary>
/// 基本はこれを行う
/// </summary>
public class StageDirection
{
    private readonly LineRenderer[] _lines;

    public StageDirection(LineRenderer[] lines) => _lines = lines;

    public async UniTask PlayAsync()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(2));

        foreach (LineRenderer line in _lines)
        {
            Color start = line.startColor;
            Color end = line.endColor;

            start.a = 1f;
            end.a = 1f;

            line.startColor = start;
            line.endColor = end;
        }
    }
}
