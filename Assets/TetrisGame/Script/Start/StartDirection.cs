using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class StartDirection : MonoBehaviour
{
    protected bool _isFinish;

    private void Start()
    {
        Direction();
    }

    private void Direction() => DirectionBase().Forget();

    private async UniTask DirectionBase()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(4));
        _isFinish = true;
    }
}
