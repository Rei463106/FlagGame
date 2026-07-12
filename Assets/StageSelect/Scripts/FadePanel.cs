using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class FadePanel : MonoBehaviour
{
    [Header("パネル")]
    [SerializeField] private SpriteRenderer _sp;

    private void Awake() => WaitFade(0, 2).Forget();

    private async UniTask WaitFade(int a, int time)
    {
        Tween tween = _sp.DOFade(a, time);
        await tween.AsyncWaitForCompletion();
    }

}
