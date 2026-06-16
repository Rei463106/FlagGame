using System;
using System.Collections.Generic;

internal class D_ModelSelecter
{
    private ClothList _list;
    private Dictionary<Parts, ClothItem[]> _modelSelectOriginDic = new Dictionary<Parts, ClothItem[]>();//元を入れるための辞書
    private Dictionary<Parts, ClothItem> _modelSelectDic = new Dictionary<Parts, ClothItem>();

    public D_ModelSelecter(ClothList list)
    {
        _list = list;

        foreach (var item in _list.PartsLists)
        {
            _modelSelectOriginDic?.TryAdd(item.Parts, item.ClothItemList);
        }
    }

    /// <summary>
    /// アイテムを選択して送信(ゴミ箱用)
    /// </summary>
    public void SelectItemDust()
    {
        EventBus.Publish<DustBoxEvent>(new DustBoxEvent());
        SelectItemBase();     
    }

    /// <summary>
    /// 正解時用
    /// </summary>
    /// <param name="c"></param>
    public void SelectItem(CorrectEvent c)
    {
        SelectItemBase();
    }

    /// <summary>
    /// お手本選出
    /// </summary>
    public void SelectItemBase()
    {
        foreach (Parts item in Enum.GetValues(typeof(Parts)))
        {
            _modelSelectDic[item] = _modelSelectOriginDic[item][UnityEngine.Random.Range(0, _modelSelectOriginDic[item].Length)];
        }//Partsごとに選出

        EventBus.Publish<ModelChangeEvent>(new ModelChangeEvent(_modelSelectDic));
    }
}
