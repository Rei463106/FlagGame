using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class NActBase : MonoBehaviour, IActBase
{
    [Header("名前表示用Text")]
    [SerializeField] private Text _nText;
    [Header("セリフText")]
    [SerializeField] private Text _text;
    [Header("名前")]
    [SerializeField] private string _name;
    [Header("セリフ文章")]
    [SerializeField] private string _serihu;
    [Header("Spriteたち")]
    [SerializeField] private NSelfActBase[] _acts;

    private INNovelEvent[] NNovel => _acts;

    public bool IsComplete => _isComplete;

    private CancellationTokenSource _source;
    private bool _isComplete;
    private int _count;

    private void OnPush(InputAction.CallbackContext c)
    {
        if (_count == 0)
            _source.Cancel();
        _count++;
    }

    public void ConnectAct()
    {
        InputManager.EntryInput("Push", OnPush);
        foreach (var i in NNovel)
            i.Execute();
        SetText().Forget();
    }

    private async UniTask SetText()
    {
        _text.text = "";
        _nText.text = _name;
        _source = new CancellationTokenSource();

        try
        {
            foreach (var i in _serihu)
            {
                _text.text += i.ToString();
                await UniTask.Delay(TimeSpan.FromSeconds(TextTime()), cancellationToken: _source.Token);
            }
        }
        catch (OperationCanceledException)
        {
            _text.text = _serihu;
        }
        finally
        {
            await UniTask.WaitUntil(() => _count > 1);
            foreach (var i in NNovel)
                i.DestroyObj();
            InputManager.OutInput("Push", OnPush);
            _isComplete = true;
        }
    }

    protected virtual float TextTime() => 1f;
}
