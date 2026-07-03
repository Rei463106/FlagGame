using System;
using UnityEngine;

/// <summary>
/// ミノを選ぶ
/// </summary>
public class MinoSelect : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] private MinoData[] _minoPrefabArray;
    [Header("Next")]
    [SerializeField] private SpriteRenderer _sp;

    private int _preNumber;

    protected void PleaseAwake() => FlowNext();

    protected GameObject SendMino(out int number)//スポーン時に呼ぶ
    {
        var p = _preNumber;
        number = p;
        FlowNext();
        return _minoPrefabArray[p].MObject;
    }

    protected GameObject HoldMino(int p)//ホールドしたものを出すときに呼ぶ
    {
        return _minoPrefabArray[p].MObject;
    }

    protected Sprite HoldSprite(int p)
    {
        return _minoPrefabArray[p].MSprite;
    }

    private void FlowNext()
    {
        _preNumber = SelectNumber();
        DisplayMino();
    }

    private int SelectNumber()
    {
        return UnityEngine.Random.Range(0, _minoPrefabArray.Length);
    }

    private void DisplayMino() => _sp.sprite = _minoPrefabArray[_preNumber].MSprite;
}

[Serializable]
public struct MinoData
{
    [Header("Prefab")]
    [SerializeField] private GameObject _minoPrefab;
    [Header("Sprite")]
    [SerializeField] private Sprite _sprite;

    public GameObject MObject => _minoPrefab;
    public Sprite MSprite => _sprite;
}
