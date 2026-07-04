using UnityEngine;

public class MinoSound : MonoBehaviour
{
    [Header("動く時の音")]
    [SerializeField] private AudioClip _moveClip;

    private AudioSource _source;
    protected void PleaseAwake() => _source = GetComponent<AudioSource>();
    protected void PlayClip() => _source.PlayOneShot(_moveClip);
}
