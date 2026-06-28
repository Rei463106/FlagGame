using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ミノを選ぶ
/// </summary>
public class MinoSelect : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] private List<GameObject> _minoPrefab = new();

    protected GameObject MakeMino()//スポーン時に呼ぶ
    {
        return _minoPrefab[Random.Range(0, _minoPrefab.Count)];
    }
}
