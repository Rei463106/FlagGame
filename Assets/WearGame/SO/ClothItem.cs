using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ClothItem", menuName = "Cloth/ClothItem")]
internal class ClothItem : ScriptableObject
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