using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class L_GameFinish : MonoBehaviour
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
    [Header("フィニッシュ音")]
    [SerializeField] private AudioClip _clip;
    [Header("クリアSO")]
    [SerializeField] private ClearFlag _cFlag;
    [Header("スコア")]
    [SerializeField] private MiniGameScore _score;

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
        GetComponent<AudioSource>().PlayOneShot(_clip);
        _sText.text = "Finish!!!";
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        _sText.text = "";
        Tween tween = _fadeSp.DOFade(0.7f, 2f);
        await tween.AsyncWaitForCompletion();
        _hukidasi.enabled = true;
        _faceSp.enabled = true;
        var t = _talking.TSettings.FirstOrDefault(x => x.TalkType == TTalkEnum.Finish);
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
        await UniTask.Delay(TimeSpan.FromSeconds(0.3f));
        _text.text = "";
        _hukidasi.enabled = false;
        _faceSp.enabled = false;
        Tween tween2 = _fadeSp.DOFade(1f, 2f);
        await tween2.AsyncWaitForCompletion();

        if (_cFlag.Flag == ClearFlags.None)
            _cFlag.ChangeFlag(ClearFlags.Clear);
        else if (_cFlag.Flag == ClearFlags.Clear)
            _cFlag.ChangeFlag(ClearFlags.Second);

        _score.ChangeScore(HaveScore.Score);
        SceneManager.LoadScene("StageSelect");
    }
}
