
/// <summary>
/// スコアを加算する
/// </summary>
public class D_Score
{
    private int _score;

    public void NormalAddScore(int score)
    {
        _score += score;
        EventBus.Publish<ScoreEvent>(new ScoreEvent(_score));
    }

    public void DustScore(int score)
    {
        _score += score;
        EventBus.Publish<ScoreEvent>(new ScoreEvent(_score));
    }
}
