using UnityEngine;

/// <summary>
/// DragOriginのランタイム処理
/// </summary>
public class R_ObjectOrigin : MonoBehaviour
{
    [Header("SO")]
    [SerializeField] private ClothItem _setting;

    private D_ObjectOrigin _cOrigin = new D_ObjectOrigin();

    private void OnEnable()
    {
        EventBus.Subscribe<ClickInsideFinish>(this, RevertMouse);
        EventBus.Subscribe<ClickOutsideFinish>(this, RevertMouse);
    }
    private void OnDisable()
    {
        EventBus.AllUnSubscribe(this);
    }

    /// <summary>
    /// マウスが押された時(自分のところで)
    /// </summary>
    public void PushMouse()
    {
        if (!_cOrigin.IsMousePush)
        {
            Debug.Log("呼ばれてます");
            _cOrigin.PushMouse();
            EventBus.Publish<ClickStart>(new ClickStart(_setting));
        }
    }

    /// <summary>
    /// 内側・外側でマウスが離された時
    /// </summary>
    /// <param name="c"></param>
    public void RevertMouse(ClickInsideFinish c)
    {
        _cOrigin.RevertMouse();
    }

    public void RevertMouse(ClickOutsideFinish c)
    {
        _cOrigin.RevertMouse();
    }
}
