using System;
using System.Collections.Generic;
using UnityEngine;

public class L_ModelSelecter : MonoBehaviour
{
    [Header("見た目の登録")]
    [SerializeField] private List<LooksSetting> _settingList = new List<LooksSetting>();

    private Dictionary<Parts, SpriteRenderer> _spDic = new Dictionary<Parts, SpriteRenderer>();

    private void OnEnable()
    {
        EventBus.Subscribe<ModelChangeEvent>(this, ReceiveSelecter);
    }

    private void OnDisable()
    {
        EventBus.AllUnSubscribe(this);
    }

    private void Start()
    {
        foreach (var part in _settingList)
        {
            _spDic?.TryAdd(part.Parts, part.SP);
        }
    }

    /// <summary>
    /// 変更通知を受け取り見た目を変える
    /// </summary>
    /// <param name="m"></param>
    private void ReceiveSelecter(ModelChangeEvent m)
    {
        foreach (Parts item in Enum.GetValues(typeof(Parts)))
        {
            _spDic[item].sprite = m._modelSelectDic[item].Sprite;
        }
    }
}

[Serializable]
internal struct LooksSetting
{
    [Header("Parts")]
    [SerializeField] private Parts _parts;
    [Header("SpriteRenderer")]
    [SerializeField] private SpriteRenderer _sp;

    public Parts Parts => _parts;
    public SpriteRenderer SP => _sp;
}
