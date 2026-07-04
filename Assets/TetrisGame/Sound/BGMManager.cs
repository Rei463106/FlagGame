using UnityEngine;

public class BGMManager : MonoBehaviour
{
    private AudioSource _source;

    private void Awake() => _source = GetComponent<AudioSource>();

    private void OnEnable() => EventBus.Subscribe<StartEvent>(this, ReceiveStart);

    private void OnDisable() => EventBus.AllUnSubscribe(this);

    private void ReceiveStart(StartEvent s) => _source.Play();
}
