using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ミノを選ぶ
/// </summary>
public class MinoSelect : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] private List<GameObject> _minoPrefab = new();

    private int _preNumber = -1;

    protected GameObject MakeMino()//スポーン時に呼ぶ
    {
        if (_preNumber < _minoPrefab.Count - 1)
            _preNumber++;
        else
            _preNumber = 0;

        return Instantiate(_minoPrefab[_preNumber]);
    }
}
