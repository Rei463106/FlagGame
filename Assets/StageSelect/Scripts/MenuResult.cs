using Cysharp.Threading.Tasks;
using UnityEngine;

public class MenuResult : MonoBehaviour
{
    private LineRenderer[] _ren;

    private void OnEnable() => EventBus.Subscribe<SendLine>(this, ReceiveLine);

    private void OnDisable() => EventBus.AllUnSubscribe(this);

    private void ReceiveLine(SendLine s)
    {
        _ren = s._renderer;
        WaitDirection().Forget();
    }

    private async UniTask WaitDirection()
    {
        await new StageDirection(_ren).PlayAsync();
        EventBus.Publish<SendMenuBackEvent>(new SendMenuBackEvent());
    }
}

public struct SendMenuBackEvent : IGameEvent { }
