using UnityEngine;

/// <summary>
/// ミノを選ぶ
/// </summary>
public class MinoSelect : MonoBehaviour
{
    [Header("SO")]
    [SerializeField] private Mino _mino;

    private MinoSetting _mSet;

    protected GameObject ResetMino()//スポーン時に呼ぶ
    {
        _mSet = _mino.MSetting[Random.Range(0, _mino.MSetting.Length - 1)];
        GameObject obj = _mSet.MinoPrefab;
        return obj;
    }
}
