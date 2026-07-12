using UnityEngine;
using UnityEngine.UI;

public class MiniGameStageUI : StageUIBase, IPlayer
{
    [Header("ステージ番号")]
    [SerializeField] private string _stageNumberS;
    [Header("ステージ名")]
    [SerializeField] private string _stageNameS;
    [Header("ScoreSO")]
    [SerializeField] private MiniGameScore _Mscore;
    [Header("スコア説明")]
    [SerializeField] private Text _scoreEx;
    [Header("スコア")]
    [SerializeField] private Text _score;

    private static int _currentScore = 0;

    public void PleaseAwake() => SubScore(_Mscore.Score);

    public void ChangeStageText() => ChangeText(_stageNumberS, _stageNameS);

    public void Appear(bool a)
    {
        Score(a);
        ChangeAppear(a);
    }

    public override void Score(bool a)
    {
        _score.enabled = a;
        _scoreEx.enabled = a;
    }

    private void SubScore(int score)
    {
        if (score >= _currentScore)
        {
            _score.text = score.ToString();
            _currentScore = score;
        }
    }
}
