using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ClothItemList", menuName = "Cloth/ClothItemList")]
internal class ClothList : ScriptableObject
{
    [Header("答え集")]
    [SerializeField] private List<ClothItem> _setttings = new List<ClothItem>();

    /// <summary>正解が入っているリスト</summary>
    public List<ClothItem> ClothItemList => _setttings;
}