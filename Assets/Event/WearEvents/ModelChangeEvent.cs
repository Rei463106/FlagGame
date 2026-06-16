
using System.Collections.Generic;

/// <summary>
/// お手本を選択する時に呼ぶイベント
/// </summary>
internal readonly struct ModelChangeEvent : IGameEvent
{
    public readonly Dictionary<Parts, ClothItem> _modelSelectDic;

    public ModelChangeEvent(Dictionary<Parts, ClothItem> modelSelectDic)
    {
        _modelSelectDic = modelSelectDic;
    }
}
