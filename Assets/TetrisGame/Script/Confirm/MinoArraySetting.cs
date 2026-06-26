using TMPro;
using UnityEngine;

/// <summary>
/// Minoの配列
/// </summary>
public class MinoArraySetting : MonoBehaviour
{
    [Header("行")]
    [SerializeField] private int _line;
    [Header("列")]
    [SerializeField] private int _corumn;

    private static bool[,] _minoArray;

    protected static bool[,] MinoArray => _minoArray;

    /// <summary>配列の作成</summary>
    private void Awake()
    {
        _minoArray = new bool[_line, _corumn];

        for (int i = 0; i <= _line - 1; i++)
        {
            for (int j = 0; j <= _corumn - 1; j++)
            {
                _minoArray[i, j] = false;
            }
        }
    }

    protected void UpdateArray(int line, int corumn)
    {
        _minoArray[line, corumn] = true;
    }
}
