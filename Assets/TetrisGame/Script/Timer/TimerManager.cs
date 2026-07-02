using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    [Header("時間のテキスト")]
    [SerializeField] private Text _timerText;
    [Header("制限時間")]
    [SerializeField] private int _timeLimit;

    private CancellationTokenSource _cts;
    private CancellationToken _token;

    private void OnEnable()
    {
        EventBus.Subscribe<StartEvent>(this, ReceiveStart);
        EventBus.Subscribe<PutOverEvent>(this, ReceiveGameOver);
    }
    private void OnDisable() => EventBus.AllUnSubscribe(this);

    private void Awake()
    {
        _timerText.text = _timeLimit.ToString();
        _cts = new CancellationTokenSource();
        _token = _cts.Token;
    }

    private void ReceiveStart(StartEvent s) => Timer(_token).Forget();
    private void ReceiveGameOver(PutOverEvent p) => _cts.Cancel();
    private async UniTask Timer(CancellationToken c)
    {
        var t = _timeLimit;

        try
        {
            while (!_token.IsCancellationRequested)
            {
                if (t > 0)
                {
                    t -= 1;
                    _timerText.text = t.ToString();
                    await UniTask.Delay(TimeSpan.FromSeconds(1f));
                }
                else
                {
                    EventBus.Publish<TimerEvent>(new TimerEvent());
                    _cts.Cancel();
                }
            }
        }
        catch (OperationCanceledException)
        {
            return;
        }
    }
}

public readonly struct TimerEvent : IGameEvent { }

