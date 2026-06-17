using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ゲームスタート時の演出
/// </summary>
public class L_GameStart : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private Canvas _canvas;
    [Header("妨害Image")]
    [SerializeField] private Image _image;

    private CancellationTokenSource _source;
    private CancellationToken _token;

    private void Start()
    {
        _source = new CancellationTokenSource();
        _token = _source.Token;
        StartTask(_token).Forget();
    }

    private async UniTask StartTask(CancellationToken token)
    {
        _image.enabled = true;
        _canvas.sortingOrder = 10000;

        await UniTask.Delay(TimeSpan.FromSeconds(1));

        EventBus.Publish<WearGameStartEvent>(new WearGameStartEvent());
        _image.enabled = false;
        _canvas.sortingOrder = -1000;
        Debug.Log("始まりました");
    }
}
