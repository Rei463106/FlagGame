using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ノベル部分のまとめ役
/// </summary>
public class NovelManager : MonoBehaviour
{
    [Header("ActBaseズ")]
    [SerializeField] private List<MonoBehaviour> _actBases;
    [Header("FadePanel")]
    [SerializeField] private SpriteRenderer _sp;

    private IActBase[] ActBase => _actBases.OfType<IActBase>().ToArray();

    Queue<IActBase> _actQueue = new();

    private void OnEnable() => EventBus.Subscribe<NActSendEvent>(this, ReceiveNAct);
    private void OnDisable() => EventBus.AllUnSubscribe(this);

    private void Awake() => ExecuteAct().Forget();

    private async UniTask ExecuteAct()
    {
        Tween tween = _sp.DOFade(0.5f, 3f);
        await tween.AsyncWaitForCompletion();

        foreach (var act in ActBase)
            _actQueue.Enqueue(act);

        while (_actQueue.Count > 0)
        {
            var q = _actQueue.Dequeue();
            q.ConnectAct();
            await UniTask.WaitUntil(() => q.IsComplete);
        }

        Tween tween2 = _sp.DOFade(1f, 3f);
        await tween2.AsyncWaitForCompletion();
        SceneManager.LoadScene("StageSelect");
    }

    private void ReceiveNAct(NActSendEvent n)
    {
        Queue<IActBase> improveQ = new();
        foreach (var a in n._nAct)
            improveQ.Enqueue(a);
        foreach (var b in _actQueue)
            improveQ.Enqueue(b);
        _actQueue = improveQ;
    }
}
