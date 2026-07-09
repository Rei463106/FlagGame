using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
/// <summary>
/// ゲームスタート時の演出
/// </summary>
public class L_GameStart : MonoBehaviour
{
    [Header("TTalking")]
    [SerializeField] private TTalking _talking;
    [Header("フェードアウト用")]
    [SerializeField] private SpriteRenderer _fadeSp;
    [Header("吹き出し")]
    [SerializeField] private SpriteRenderer _hukidasi;
    [Header("表情を変える用")]
    [SerializeField] private SpriteRenderer _faceSp;
    [Header("テキスト")]
    [SerializeField] private Text _text;
    [Header("時報テキスト")]
    [SerializeField] private Text _sText;
    [Header("妨害")]
    [SerializeField] private Physics2DRaycaster _phy;
    [Header("スタート音")]
    [SerializeField] private AudioClip _clip;

    private static bool _isFirst = true;
    private void Start() => StartTask().Forget();

    private async UniTask StartTask()
    {
        if (_isFirst)
        {
            _isFirst = false;
            _phy.enabled = false;
            Tween tween = _fadeSp.DOFade(0.7f, 2f);
            await tween.AsyncWaitForCompletion();
            _hukidasi.enabled = true;
            _faceSp.enabled = true;
            var t = _talking.TSettings.FirstOrDefault(x => x.TalkType == TTalkEnum.First);
            Queue<TTalkContents> tQueue = new();
            foreach (var item in t.Contents)
                tQueue.Enqueue(item);
            while (tQueue.Count > 0)
            {
                var d = tQueue.Dequeue();
                _faceSp.sprite = d.Sprite;
                foreach (var item in d.Comment)
                {
                    _text.text += item;
                    await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
                }
                await UniTask.Delay(TimeSpan.FromSeconds(2f));
                _text.text = null;
            }
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
            _text.text = "";
            _hukidasi.enabled = false;
            _faceSp.enabled = false;
            Tween tween2 = _fadeSp.DOFade(0f, 1f);
            await tween2.AsyncWaitForCompletion();

        }
        else
        {
            _phy.enabled = false;
            Tween tween2 = _fadeSp.DOFade(0f, 2f);
            await tween2.AsyncWaitForCompletion();
        }
        GetComponent<AudioSource>().PlayOneShot(_clip);
        _sText.text = "Start!!!";
        await UniTask.Delay(TimeSpan.FromSeconds(2f));
        _sText.text = "";
        EventBus.Publish<WearGameStartEvent>(new WearGameStartEvent());
        _phy.enabled = true;
        Debug.Log("始まりました");
    }
}
