using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ClothItemList", menuName = "Cloth/ClothItemList")]
internal class ClothList : ScriptableObject
{
    [Header("パーツごとのリスト")]
    [SerializeField] private PartsList[] _list;

    public PartsList[] PartsLists => _list;
}

[Serializable]
internal struct PartsList
{
    [Header("Parts")]
    [SerializeField] private Parts _parts;
    [Header("Parts集")]
    [SerializeField] private ClothItem[] _setttings;

    public Parts Parts => _parts;
    public ClothItem[] ClothItemList => _setttings;
}