using System.Collections.Generic;
using UnityEngine;

internal class D_ModelSelecter
{
    private ClothList _list;
    private List<ClothItem> _headItem = new List<ClothItem>();
    private List<ClothItem> _bodyItem = new List<ClothItem>();
    private List<ClothItem> _footItem = new List<ClothItem>();

    public D_ModelSelecter(ClothList list)
    {
        _list = list;
        foreach (var item in _list.ClothItemList)
        {
            if (item.Parts == Parts.Head)
                _headItem?.Add(item);
            else if (item.Parts == Parts.Body)
                _bodyItem?.Add(item);
            else if (item.Parts == Parts.Foot)
                _footItem?.Add(item);
        }
    }

    /// <summary>
    /// アイテムを選択して送信(ゴミ箱・最初用)
    /// </summary>
    public void SelectItem()
    {
        var head = _headItem[Random.Range(0, _headItem.Count)];
        var body = _bodyItem[Random.Range(0, _bodyItem.Count)];
        var foot = _footItem[Random.Range(0, _footItem.Count)];

        EventBus.Publish<ModelChangeEvent>(new ModelChangeEvent(head, body, foot));
    }

    /// <summary>
    /// 正解時用
    /// </summary>
    /// <param name="c"></param>
    public void SelectItem(CorrectEvent c)
    {
        var head = _headItem[Random.Range(0, _headItem.Count)];
        var body = _bodyItem[Random.Range(0, _bodyItem.Count)];
        var foot = _footItem[Random.Range(0, _footItem.Count)];

        EventBus.Publish<ModelChangeEvent>(new ModelChangeEvent(head, body, foot));
    }
}
