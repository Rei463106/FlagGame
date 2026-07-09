using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WBGMManager : MonoBehaviour
{
    private void OnEnable()
    {
        EventBus.Subscribe<WearGameStartEvent>(this, ReceiveStart);
        EventBus.Subscribe<GameOverEvent>(this, ReceiveGameOver);
    }

    private void OnDisable() => EventBus.AllUnSubscribe(this);
   
    private void ReceiveStart(WearGameStartEvent s) => GetComponent<AudioSource>().Play();
    private void ReceiveGameOver(GameOverEvent g) => GetComponent<AudioSource>().Stop();

}
