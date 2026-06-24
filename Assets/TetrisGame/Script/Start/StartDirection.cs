using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

public class StartDirection : MonoBehaviour
{
    protected bool _isFinish;

    private void Start()
    {
        Direction();
    }

    private void Direction()
    {
        CancellationTokenSource source = new CancellationTokenSource();
        CancellationToken token = source.Token;
        DirectionBase(token).Forget();
    }

    private async UniTask DirectionBase(CancellationToken token)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(4));
        _isFinish = true;
    }
}
