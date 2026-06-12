using System;
using System.Collections.Generic;
using UnityEngine;

internal class R_ColliderManager : MonoBehaviour
{
    [Header("リスト")]
    [SerializeField] private List<ColliderSetting> _settings = new List<ColliderSetting>();

    private Dictionary<Parts, Collider2D> _colliderDic = new Dictionary<Parts, Collider2D>();

    public Dictionary<Parts, Collider2D> ColliderDic => _colliderDic;
    private void OnEnable()
    {
        foreach (var s in _settings)
        {
            _colliderDic[s.Parts] = s.Collider;
        }

        EventBus.Subscribe<DragGiveSetting>(this, SubscribeSetting);
    }

    private void OnDisable()
    {
        EventBus.AllUnSubscribe(this);
    }

    private void SubscribeSetting(DragGiveSetting s)
    {
        foreach (var i in _colliderDic.Keys)
        {
            if (i == s.Setting.Parts)
            {
                _colliderDic[i].enabled = true;
            }
            else
            {
                _colliderDic[i].enabled = false;
            }
        }
    }
}

[Serializable]
internal struct ColliderSetting
{
    [Header("タイプ")]
    [SerializeField] private Parts _parts;
    [Header("Collider")]
    [SerializeField] private Collider2D _collider;

    public Parts Parts => _parts;
    public Collider2D Collider => _collider;
}
