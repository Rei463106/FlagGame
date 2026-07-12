using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishManager : FinishDirection, IStateEvent
{
    [Header("クリアSO")]
    [SerializeField] private ClearFlag _cFlag;
    [Header("スコア")]
    [SerializeField] private MiniGameScore _score;

    public StateEnum State => StateEnum.Finish;

    public event Action<StateEnum> StateChanged;

    public void Starter() => WaitDirection().Forget();

    private void Awake() => StateMachine.Entry<FinishManager>(this);

    private async UniTask WaitDirection()
    {
        Direction();
        await UniTask.WaitUntil(() => _isFinish);
        if (_cFlag.Flag == ClearFlags.None)
            _cFlag.ChangeFlag(ClearFlags.Clear);
        else if (_cFlag.Flag == ClearFlags.Clear)
            _cFlag.ChangeFlag(ClearFlags.Second);

        _score.ChangeScore(ScoreManager._finalScore);

        SceneManager.LoadScene("StageSelect");
    }
}
