
/// <summary>
/// お手本を選択する時に呼ぶイベント
/// </summary>
internal readonly struct ModelChangeEvent : IGameEvent
{
    public readonly ClothItem _head;
    public readonly ClothItem _body;
    public readonly ClothItem _foot;

    public ModelChangeEvent(ClothItem h, ClothItem b, ClothItem f)
    {
        _head = h;
        _body = b;
        _foot = f;
    }
}
