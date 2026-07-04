using UnityEngine;

public class SEManager : MonoBehaviour
{
    [Header("ホールド音")]
    [SerializeField] private AudioClip _holdClip;
    [Header("置いた時の音")]
    [SerializeField] private AudioClip _putClip;
    [Header("３列以下")]
    [SerializeField] private AudioClip _deleteClip;
    [Header("テトリス")]
    [SerializeField] private AudioClip _tDeleteClip;

    private AudioSource _source;

    private void Awake() => _source = GetComponent<AudioSource>();

    private void OnEnable()
    {
        EventBus.Subscribe<HoldAction>(this, ReceiveHold);
        EventBus.Subscribe<SendPositionEvent>(this, ReceivePut);
        EventBus.Subscribe<SendScoreEvent>(this, ReceiveScore);
    }

    private void OnDisable() => EventBus.AllUnSubscribe(this);

    private void ReceiveHold(HoldAction action) => _source.PlayOneShot(_holdClip);
    private void ReceivePut(SendPositionEvent s) => _source.PlayOneShot(_putClip);
    private void ReceiveScore(SendScoreEvent s)
    {
        if (s._line == 4)
            _source.PlayOneShot(_tDeleteClip, 10f);
        else if (s._line >= 1 && s._line <= 3)
            _source.PlayOneShot(_deleteClip);
    }
}

