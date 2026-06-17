
/// <summary>
/// ゲームオーバー時に呼ぶイベント
/// </summary>
public readonly struct GameOverEvent : IGameEvent
{
    public readonly string _deathText;

    public GameOverEvent(string text)
    {
        _deathText = text;
    }
}
