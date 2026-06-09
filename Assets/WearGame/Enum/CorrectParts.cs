using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CorrectParts", menuName = "Scriptable Objects/CorrectParts")]
internal class CorrectParts : ScriptableObject
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
    [SerializeField] private Cloth _head;
    [Header("体")]
    [SerializeField] private Cloth _body;
    [Header("足")]
    [SerializeField] private Cloth _foot;

    public Cloth Head => _head;
    public Cloth Body => _body;
    public Cloth Foot => _foot;
}
