using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cloth", menuName = "Scriptable Objects/Cloth")]
internal class Cloth : ScriptableObject
{
    [Header("リスト")]
    [SerializeField] private List<ClothSetting> _clothSetting = new List<ClothSetting>();

    /// <summary>パーツの情報が入ってるリスト</summary>
    public List<ClothSetting> ClothList => _clothSetting;
}

[Serializable]
internal struct ClothSetting
{
    [Header("名前")]
    [SerializeField] private string _name;
    [Header("状態")]
    [SerializeField] private Parts _parts;
    [Header("イラスト")]
    [SerializeField] private Sprite _sprite;

    public string Name => _name;
    public Parts Parts => _parts;
    public Sprite Sprite => _sprite;
}
