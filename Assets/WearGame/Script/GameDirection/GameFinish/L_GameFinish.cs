using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class L_GameFinish : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private Canvas _canvas;
    [Header("妨害Image")]
    [SerializeField] private Image _image;

    private CancellationTokenSource _source;
    private CancellationToken _token;

    private void OnEnable()
    {
        EventBus.Subscribe<WearGameFinishEvent>(this, ReceiveFinish);
    }

    private void OnDisable()
    {
        EventBus.AllUnSubscribe(this);
    }

    private void ReceiveFinish(WearGameFinishEvent w)
    {
        _source = new CancellationTokenSource();
        _token = _source.Token;
        FinishTask(_token).Forget();
    }

    private async UniTask FinishTask(CancellationToken token)
    {
        _image.enabled = true;
        _canvas.sortingOrder = 10000;

        await UniTask.Delay(TimeSpan.FromSeconds(5));

        _image.enabled = false;
        _canvas.sortingOrder = -1000;

        Debug.Log("ほんとうのおわり");
        SceneManager.LoadScene("");
    }
}
