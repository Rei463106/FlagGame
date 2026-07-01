using UnityEngine;

/// <summary>
/// Minoの配列
/// </summary>
public class MinoArray : MonoBehaviour
{
    [Header("行")]
    [SerializeField] private int _line;
    [Header("列")]
    [SerializeField] private int _corumn;

    private static MinoArraySetting[,] _minoArray;

    public static MinoArraySetting[,] MArraySetting => _minoArray;

    /// <summary>配列の作成</summary>
    protected void PleaseAwake()
    {
        _minoArray = new MinoArraySetting[_line, _corumn];

        for (int i = 0; i <= _line - 1; i++)
        {
            for (int j = 0; j <= _corumn - 1; j++)
            {
                _minoArray[i, j] = new MinoArraySetting(new Vector2(j, -i));//(x,y)で揃える
            }
        }
    }

    /// <summary>
    /// 対象だけtrueにする
    /// </summary>
    /// <param name="v"></param>
    protected void UpdateArray(Vector2 v)
    {
        foreach (var m in _minoArray)
        {
            if (m._wallBlock == v)
            {
                m.ChangeExist(true);
                break;
            }
        }
    }

    /// <summary>
    /// 一旦全部falseにする
    /// </summary>
    protected void ResetArray()
    {
        foreach (var item in _minoArray)
            item.ChangeExist(false);
    }
}

/// <summary>
/// ミノの設定
/// </summary>
public class MinoArraySetting
{
    public readonly Vector2 _wallBlock;
    private bool _isExist = false;

    public bool IsExist => _isExist;

    public MinoArraySetting(Vector2 wallBlock) => _wallBlock = wallBlock;
    public void ChangeExist(bool c) => _isExist = c;
}
