using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class L_GameCorrect : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private Canvas _canvas;
    [Header("妨害Image")]
    [SerializeField] private Image _image;

    private CancellationTokenSource _source;
    private CancellationToken _token;

    private void OnEnable()
    {
        EventBus.Subscribe<CorrectEvent>(this, ReceiveCorrect);
    }

    private void OnDisable()
    {
        EventBus.AllUnSubscribe(this);
    }

    private void ReceiveCorrect(CorrectEvent c)
    {
        _source = new CancellationTokenSource();
        _token = _source.Token;
        CorrectTask(_token).Forget();
    }

    private async UniTask CorrectTask(CancellationToken token)
    {
        _image.enabled = true;
        _canvas.sortingOrder = 10000;

        await UniTask.Delay(TimeSpan.FromSeconds(2));

        _image.enabled = false;
        _canvas.sortingOrder = -1000;

        Debug.Log("正解！！");
    }
   
}
