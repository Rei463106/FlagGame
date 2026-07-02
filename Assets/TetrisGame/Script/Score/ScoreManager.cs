using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [Header("固定値")]
    [SerializeField] private int _fixedScore;
    [Header("ScoreText")]
    [SerializeField] private Text _scoreText;
    [Header("AddScoreText")]
    [SerializeField] private Text _addScoreText;

    private int _addScore;
    private int _score;

    private void OnEnable() => EventBus.Subscribe<SendScoreEvent>(this, ReceiveDeleteInfo);

    private void ReceiveDeleteInfo(SendScoreEvent s)
    {
        AddScore(_fixedScore, s._line, s._combo);
        ScoreDirection();
    }

    private void AddScore(int fix, int line, int combo)
    {
        _addScore = fix * (line + combo);
        _score += _addScore;
    }

    private void ScoreDirection()
    {
        if (_addScore == 0) return;

        _addScoreText.text = $"+{_addScore}";
        _addScoreText.DOFade(1f, 0f);
        var s = DOTween.Sequence();
        var t = _addScoreText.gameObject.transform.position;
        s.Append(_addScoreText.gameObject.transform.DOMoveY(t.y + 2f, 1f)).Join(_addScoreText.DOFade(0f, 1f)).Append(_addScoreText.gameObject.transform.DOMoveY(t.y, 0));

        var addScore = 0;
        DOTween.To(() => addScore, x => { addScore = x; _scoreText.text = $"{_score + addScore}"; }, _addScore, 2f);
    }
}
