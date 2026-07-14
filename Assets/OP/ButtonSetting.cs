using System;
using UnityEngine;

[Serializable]
public struct ButtonSetting
{
    [Header("NActBase")]
    [SerializeField] private NActBase[] _actBase;
    [Header("好感度")]
    [SerializeField] private int _love;
    [Header("選択文")]
    [SerializeField] private string _explain;

    public NActBase[] ActBase => _actBase;
    public int Love => _love;
    public string Explain => _explain;
}
