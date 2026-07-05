using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Cloth本体の処理を記述するところ(ランタイム)
/// </summary>
internal class R_ObjectSetting : MonoBehaviour
{
    private bool _isDrag;
    private D_ObjectSetting _setting;

    private void Awake()
    {
        _setting = new D_ObjectSetting();
    }

    /// <summary>
    ///  マウスドラッグ時設定を渡す
    /// </summary>
    public void DragMouseForSetting()
    {
        if (!_isDrag)
        {
            _isDrag = true;
            _setting.DragMouse();
            if (TryGetComponent<SpriteRenderer>(out var c))
            {
                c.sprite = _setting.MyClothSetting.Sprite;
                c.enabled = true;
            }
        }
    }

    /// <summary>
    /// マウスドラッグ時
    /// </summary>
    public void DragMouse()
    {
        var mouse = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 mousePos = new(mouse.x, mouse.y);
        transform.position = mousePos;
    }

    /// <summary>
    /// マウスを離した時
    /// </summary>
    public void RevertMouse()
    {
        _setting.RevertMouse();
        _isDrag = false;
    }

    /// <summary>
    /// プール側に離されたか伝える用
    /// </summary>
    /// <returns></returns>
    public bool WaitUntilRevertMouse()
    {
        return _setting.WaitUntilRevertMouse();
    }

    /// <summary>
    /// プール側に設定してもらう用
    /// </summary>
    /// <param name="item"></param>
    public void ReceiveSetting(ClothItem item)
    {
        _setting.MyCloth(item);
    }

    /// <summary>
    /// プールに値を戻してもらう
    /// </summary>
    public void ReturnValue()
    {
        _setting.ReturnValue();
    }
}
