using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CorrectCloth", menuName = "Cloth/CorrectCloth")]
internal class CorrectCloth : ScriptableObject
{
    [Header("答え集")]
    [SerializeField] private List<CorrectPartsSetting> _setttings = new List<CorrectPartsSetting>();

    /// <summary>正解が入っているリスト</summary>
    public List<CorrectPartsSetting> CorrectList => _setttings;
}

[Serializable]
internal struct CorrectPartsSetting
{
    [Header("頭")]
    [SerializeField] private ClothItem _head;
    [Header("体")]
    [SerializeField] private ClothItem _body;
    [Header("足")]
    [SerializeField] private ClothItem _foot;

    public ClothItem Head => _head;
    public ClothItem Body => _body;
    public ClothItem Foot => _foot;
}
