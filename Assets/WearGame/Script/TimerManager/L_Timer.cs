using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// タイマーを動かす処理
/// </summary>
public class L_Timer : MonoBehaviour
{
    [Header("タイマー用")]
    [SerializeField] private Text _timerText;
    [Header("制限時間")]
    [SerializeField] private int _limitTime;

    private CancellationTokenSource _source;
    private CancellationToken _token;

    private void OnEnable()
    {
        EventBus.Subscribe<WearGameStartEvent>(this, ReceiveStart);
        EventBus.Subscribe<GameOverEvent>(this, ReceiveGameOver);
    }

    private void OnDisable()
    {
        EventBus.AllUnSubscribe(this);
    }

    private void Start()
    {
        _timerText.text = _limitTime.ToString();
    }

    private void ReceiveStart(WearGameStartEvent w)
    {
        _source = new CancellationTokenSource();
        _token = _source.Token;
        Timer(_token).Forget();
    }

    /// <summary>
    /// ゲームオーバー時タイマーを停止
    /// </summary>
    /// <param name="g"></param>
    private void ReceiveGameOver(GameOverEvent g)
    {
        _source.Cancel();
    }

    /// <summary>
    /// タイマー
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    private async UniTask Timer(CancellationToken token)
    {
        try
        {
            for (int i = _limitTime; i >= 0; i--)
            {
                _timerText.text = i.ToString();
                await UniTask.Delay(1000, cancellationToken: token);
            }
        }
        catch (OperationCanceledException)
        {
            return;
        }//これ以上先に進まない

        //ゲーム終了イベを呼ぶ
        EventBus.Publish<WearGameFinishEvent>(new WearGameFinishEvent());
    }
}
