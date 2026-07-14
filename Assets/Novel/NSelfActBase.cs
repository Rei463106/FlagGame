using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public class NSelfActBase : MonoBehaviour, INNovelEvent
{
    [Header("CharcterObj")]
    [SerializeField] private CharacterObj _chr;
    [Header("NormalNovel")]
    [SerializeField] private NNovel _normal;

    private CancellationTokenSource _cts;
    private CharacterObj _current;

    private void SetSetting()
    {
        _current = Instantiate(_chr);
        _current.SetSprite(_normal.Sprite);
        _current.SetPosition(_normal.Pos);
    }

    public void Execute()
    {
        SetSetting();
        _cts = new CancellationTokenSource();
        Direction(_chr, _cts.Token).Forget();
    }

    public void DestroyObj()
    {
        _cts.Cancel();
        _current.DestroyObj();
    }

    protected virtual UniTask Direction(CharacterObj c, CancellationToken token)
    {
        return UniTask.CompletedTask;
    }
}
