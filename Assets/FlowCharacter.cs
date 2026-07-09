using DG.Tweening;
using UnityEngine;

public class FlowCharacter : MonoBehaviour
{
    private void Awake() => transform.DOMoveY(transform.position.y + 0.3f, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    private void OnDestroy() => transform.DOKill();
}
