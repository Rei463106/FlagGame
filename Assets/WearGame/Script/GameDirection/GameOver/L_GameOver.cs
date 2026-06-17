using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// ゲームオーバー時の演出
/// </summary>
public class L_GameOver : MonoBehaviour
{
    [Header("ゲームオーバー時の背景")]
    [SerializeField] private Image _image;
    [Header("死亡理由")]
    [SerializeField] private Text _text;
    [Header("Canvas")]
    [SerializeField] private Canvas _canvas;

    private CancellationTokenSource _source;
    private CancellationToken _token;
    private string _message;

    private void OnEnable()
    {
        EventBus.Subscribe<GameOverEvent>(this, RecieveGameOver);
    }

    private void OnDisable()
    {
        EventBus.AllUnSubscribe(this);
    }

    private void Start()
    {
        _image.enabled = false;
        _text.text = null;
    }

    private void RecieveGameOver(GameOverEvent g)
    {
        _message = g._deathText;
        _source = new CancellationTokenSource();
        _token = _source.Token;
        GameOverDirection(_token).Forget();
    }

    private async UniTask GameOverDirection(CancellationToken c)
    {
        //何かアニメーションが入るかもしれない
        _canvas.sortingOrder = 10000;    
        _image.enabled = true;

        await UniTask.Delay(TimeSpan.FromSeconds(1f), cancellationToken: c);

        _text.text = _message;

        await UniTask.WaitUntil(() => Mouse.current.leftButton.wasPressedThisFrame, cancellationToken: c);

        SceneManager.LoadScene("WearGameScene");
    }
}
