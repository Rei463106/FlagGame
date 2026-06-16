using UnityEngine;
using UnityEngine.UI;

public class L_Score : MonoBehaviour
{
    [Header("ScoreText")]
    [SerializeField] private Text _text;

    private void OnEnable()
    {
        EventBus.Subscribe<ScoreEvent>(this, ReceiveScore);
    }

    private void OnDisable()
    {
        EventBus.AllUnSubscribe(this);
    }

    private void ReceiveScore(ScoreEvent s)
    {
        _text.text = s._score.ToString();
    }
}
