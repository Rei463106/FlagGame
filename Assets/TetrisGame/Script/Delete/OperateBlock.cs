using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 受け取った位置にブロックを作り出す
/// </summary>
public class OperateBlock : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] private GameObject _minoprefab;

    private readonly Dictionary<VectorInfo, GameObject> _minoDic = new();//位置とブロックの辞書

    private void OnEnable() => EventBus.Subscribe<SendPositionEvent>(this, ReceiveArray);
    private void OnDisable() => EventBus.AllUnSubscribe(this);

    /// <summary>
    /// 受け取り次第、そこにブロックを作る
    /// </summary>
    /// <param name="s"></param>
    private void ReceiveArray(SendPositionEvent s)
    {
        foreach (var item in s._positions)
        {
            var i = Instantiate(_minoprefab);
            i.transform.position = item;
            VectorInfo info = new(item);
            _minoDic?.TryAdd(info, i);
        }
    }

    /// <summary>
    /// ブロックを消す
    /// </summary>
    protected void DeleteArray()
    {
        var d = MinoArray.MArraySetting;

        for (int i = 0; i < d.GetLength(0); i++)
        {
            bool complete = true;

            for (int j = 0; j < d.GetLength(1); j++)
            {
                if (!d[i, j].IsExist)
                {
                    complete = false;
                    break;
                }
            }

            if (complete)
            {
                List<VectorInfo> list = new();

                foreach (var item in _minoDic.Keys)
                {
                    if (item.Current.y == -i)
                    {
                        Destroy(_minoDic[item]);
                        list.Add(item);
                    }
                }

                foreach (var it in list)
                    _minoDic.Remove(it);

                //下につめる
                foreach (var item in _minoDic.Keys)
                    if (item.Current.y > -i)
                        item.MoveVector();

                //念のため盤面を更新
                EventBus.Publish<UpdatePositionEvent>(new UpdatePositionEvent(SendPosition()));
            }
        }
    }

    /// <summary>
    /// ブロックを下降させる
    /// </summary>
    /// <returns></returns>
    protected void FallDown()
    {
        foreach (var item in _minoDic.Keys)

            _minoDic[item].transform.DOMove(new Vector3(item.Current.x, item.Current.y, 0), 2f);
    }

    protected List<Vector2> SendPosition()
    {
        List<Vector2> pList = new();

        foreach (var item in _minoDic.Keys)
            pList.Add(item.Current);

        return pList;
    }
}

public class VectorInfo
{
    public Vector2 Current { get; private set; }

    public VectorInfo(Vector2 after) => Current = after;

    public void MoveVector()
    {
        var a = Current;
        a.y -= 1;
        Current = a;
    }
}
