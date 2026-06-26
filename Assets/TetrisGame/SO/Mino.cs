using System;
using UnityEngine;

/// <summary>
/// Minoの初期設定
/// </summary>
[CreateAssetMenu(fileName = "Mino", menuName = "MinoObjects/Mino")]
public class Mino : ScriptableObject
{
    [Header("スポーン位置")]
    [SerializeField] private Vector2 _spawnPosition;
    [Header("回転軸,ずれ")]
    [SerializeField] private Vector2 _displaceRotate;
    [Header("SpawnSetting")]
    [SerializeField] private MinoSetting[] _setting;

    public Vector2 SpawnPosition => _spawnPosition;
    public Vector2 DisplaceRotate => _displaceRotate;
    public MinoSetting[] MSetting => _setting;
}

[Serializable]
public struct MinoSetting
{
    [Header("ブロック,ずれ")]
    [SerializeField] private Vector2 _diaplacePosition;

    public Vector2 DisplacePostion => _diaplacePosition;
}
