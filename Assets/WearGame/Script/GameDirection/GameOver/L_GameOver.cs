using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// ゲームオーバー時の演出
/// </summary>
public class L_GameOver : MonoBehaviour
{
    [Header("SpriteRenderer")]
    [SerializeField] private SpriteRenderer _sp;
    [Header("Sprite")]
    [SerializeField] private Sprite _mutu;
    [Header("ゲームオーバー時の背景")]
    [SerializeField] private SpriteRenderer _gSp;
    [Header("死亡理由")]
    [SerializeField] private Text _text;
    [Header("妨害")]
    [SerializeField] private Physics2DRaycaster _phy;

    private CancellationTokenSource _source;
    private CancellationToken _token;
    private string _message;

    private void OnEnable() => EventBus.Subscribe<GameOverEvent>(this, RecieveGameOver);

    private void OnDisable() => EventBus.AllUnSubscribe(this);

    private void RecieveGameOver(GameOverEvent g)
    {
        _message = g._deathText;
        _source = new CancellationTokenSource();
        _token = _source.Token;
        GameOverDirection(_token).Forget();
    }

    private async UniTask GameOverDirection(CancellationToken c)
    {
        _phy.enabled = false;
        _sp.sprite = _mutu;
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        _gSp.enabled = true;
        await UniTask.Delay(TimeSpan.FromSeconds(1f), cancellationToken: c);

        foreach (var m in _message.ToCharArray())
        {
            _text.text += m;
            await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
        }

        _phy.enabled = true;
        await UniTask.WaitUntil(() => Mouse.current.leftButton.wasPressedThisFrame, cancellationToken: c);

        SceneManager.LoadScene("WearGameScene");
    }
}
