using UnityEngine;

public class R_ModelSelecter : MonoBehaviour
{
    [Header("ClothList")]
    [SerializeField] private ClothList _list;
    private D_ModelSelecter _selecter;

    private void OnEnable()
    {
        EventBus.Subscribe<CorrectEvent>(this, RenewalItem);
    }

    private void OnDisable()
    {
        EventBus.AllUnSubscribe(this);
    }

    private void Start()
    {
        _selecter = new D_ModelSelecter(_list);
        _selecter.SelectItemBase();
    }

    /// <summary>
    /// ゴミ箱
    /// </summary>
    public void DustBox()
    {
        _selecter.SelectItemDust();
    }

    /// <summary>
    /// 正解時用
    /// </summary>
    /// <param name="c"></param>
    public void RenewalItem(CorrectEvent c)
    {
        _selecter.SelectItemBase();
    }
}
