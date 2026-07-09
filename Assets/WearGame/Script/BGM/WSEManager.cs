using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WSEManager : MonoBehaviour
{
    [Header("Correct")]
    [SerializeField] private AudioClip _cClip;
    [Header("装着時")]
    [SerializeField] private AudioClip _pClip;

    private void OnEnable()
    {
        EventBus.Subscribe<CorrectEvent>(this, ReceiveCorrect);
        EventBus.Subscribe<ObjectInsideEvent>(this, ReceiveInside);
    }

    private void OnDisable() => EventBus.AllUnSubscribe(this);
    private void ReceiveCorrect(CorrectEvent c) => GetComponent<AudioSource>().PlayOneShot(_cClip);
    private void ReceiveInside(ObjectInsideEvent o) => GetComponent<AudioSource>().PlayOneShot(_pClip);
}
