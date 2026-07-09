using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class L_GameCorrect : MonoBehaviour
{
    [Header("SpriteRenderer")]
    [SerializeField] private SpriteRenderer _sp;
    [Header("Sprite")]
    [SerializeField] private Sprite _normal;
    [Header("Sprite")]
    [SerializeField] private Sprite _niko;
    [Header("時報テキスト")]
    [SerializeField] private Text _sText;
    [Header("妨害")]
    [SerializeField] private Physics2DRaycaster _phy;

    private void OnEnable() => EventBus.Subscribe<CorrectEvent>(this, ReceiveCorrect);

    private void OnDisable() => EventBus.AllUnSubscribe(this);

    private void ReceiveCorrect(CorrectEvent c) => CorrectTask().Forget();

    private async UniTask CorrectTask()
    {
        _phy.enabled = false;
        _sp.sprite = _niko;
        _sText.color = Color.blue;
        _sText.text = "Correct!!!";
        await UniTask.Delay(TimeSpan.FromSeconds(2f));
        _sp.sprite = _normal;
        _sText.text = "";
        _phy.enabled = true;
    }

}
