using System;
using UnityEngine;

/// <summary>
/// Minoの初期設定
/// </summary>
[CreateAssetMenu(fileName = "Mino", menuName = "MinoObjects/Mino")]
public class Mino : ScriptableObject
{
    [Header("MinoSetting")]
    [SerializeField] private MinoSetting[] _setting;

    public MinoSetting[] MSetting => _setting;
}

[Serializable]
public struct MinoSetting
{
    [Header("Minoの名前")]
    [SerializeField] private string _name;
    [Header("MinoPrefab")]
    [SerializeField] private GameObject _minoPrefab;
    [Header("スポーン位置:X")]
    [SerializeField] private int _spawnX;
    [Header("スポーン位置:Y")]
    [SerializeField] private int _spawnY;
    [Header("回転軸のスポーン位置とのずれ：X")]
    [SerializeField] private int _rotateX;
    [Header("回転軸のスポーン位置とのずれ：Y")]
    [SerializeField] private int _rotateY;
    [Header("SpawnSetting")]
    [SerializeField] private SpawnSetting[] _setting;

    public GameObject MinoPrefab => _minoPrefab;
    public int SpawnX => _spawnX;
    public int SpawnY => _spawnY;
    public int RotateX => _rotateX;
    public int RotateY => _rotateY;
    public SpawnSetting[] SSetting => _setting;
}

[Serializable]
public struct SpawnSetting
{
    [Header("スポーン位置とのずれ:X")]
    [SerializeField] private int _displacementX;
    [Header("スポーン位置とのずれ:Y")]
    [SerializeField] private int _displacementY;

    public int DisplacementX => _displacementX;
    public int DisplacementY => _displacementY;
}
