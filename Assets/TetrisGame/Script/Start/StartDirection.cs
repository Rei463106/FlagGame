using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StartDirection : MonoBehaviour
{
    [Header("演出用")]
    [SerializeField] private TTalking _talking;
    [Header("表情")]
    [SerializeField] private Image _image;
    [Header("テキスト")]
    [SerializeField] private Text _text;
    [Header("コールテキスト")]
    [SerializeField] private Text _callText;
    [Header("ボイス")]
    [SerializeField] private AudioClip _clip;
    [Header("オーディオ")]
    [SerializeField] private AudioSource _source;
    [Header("フェード")]
    [SerializeField] private SpriteRenderer _fadeImage;

    protected bool _isFinish;

    private void Start() => Direction();
    private void Direction() => DirectionBase().Forget();

    private async UniTask DirectionBase()
    {
        Tween tween = _fadeImage.DOFade(0f,2f);
        await tween.AsyncWaitForCompletion();

        _callText.text = "";
        var c = _talking.TSettings.FirstOrDefault(x => x.TalkType == TTalkEnum.First);
        Queue<TTalkContents> tQueue = new();

        foreach (var item in c.Contents)
            tQueue.Enqueue(item);

        await UniTask.Delay(TimeSpan.FromSeconds(1f));

        while (tQueue.Count > 0)
        {
            var d = tQueue.Dequeue();
            SetFaceComment(d.Sprite, d.Comment);
            await UniTask.Delay(TimeSpan.FromSeconds(2f));
        }

        _callText.text = "Start!!";
        _source.PlayOneShot(_clip);
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        _callText.text = "";
        _isFinish = true;
    }

    private void SetFaceComment(Sprite sprite, string text)
    {
        _image.sprite = sprite;
        _text.text = text;
    }
}
