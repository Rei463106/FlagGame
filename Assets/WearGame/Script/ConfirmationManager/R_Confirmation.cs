using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class R_Confirmation : MonoBehaviour
{
    private Dictionary<Parts, ClothItem> _receiveDic = new Dictionary<Parts, ClothItem>();//お手本を入れる
    private Dictionary<Parts, ClothItem> _comfirmationDic = new Dictionary<Parts, ClothItem>();//現状を入れる

    private ClothItem _receive;

    private void OnEnable()
    {
        EventBus.Subscribe<DragGiveSettingEvent>(this, ReceiveDragEvent);
        EventBus.Subscribe<ObjectInsideEvent>(this, ReceiveInside);
        EventBus.Subscribe<ModelChangeEvent>(this, ReceiveModel);
        EventBus.Subscribe<DustBoxEvent>(this, ReceiveDustBox);
    }

    private void OnDisable()
    {
        EventBus.AllUnSubscribe(this);
    }
    private void Start()
    {
        foreach (Parts parts in Enum.GetValues(typeof(Parts)))
            _comfirmationDic?.TryAdd(parts, null);
    }

    private void ReceiveDragEvent(DragGiveSettingEvent d)
    {
        _receive = d.Setting;
    }

    private void ReceiveInside(ObjectInsideEvent o)
    {
        _comfirmationDic[_receive.Parts] = _receive;

        bool correct = true;

        foreach (Parts parts in Enum.GetValues(typeof(Parts)))
        {
            if (_receiveDic[parts] != _comfirmationDic[parts])
            {
                correct = false;
                break;
            }
            else
                continue;
        }

        if (correct)
        {
            EventBus.Publish<CorrectEvent>(new CorrectEvent());

            var keys = _comfirmationDic.Keys.ToList();
            foreach (var i in keys)
                _comfirmationDic[i] = null;
        }
    }

    private void ReceiveDustBox(DustBoxEvent d)
    {
        var keys = _comfirmationDic.Keys.ToList();
        foreach (var i in keys)
            _comfirmationDic[i] = null;
    }

    private void ReceiveModel(ModelChangeEvent m)
    {
        foreach (Parts parts in Enum.GetValues(typeof(Parts)))
        {
            _receiveDic[parts] = m._modelSelectDic[parts];
        }
    }
}
