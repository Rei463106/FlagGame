using UnityEngine;

/// <summary>
/// Cloth本体の処理を記述するところ(データ)
/// </summary>
public class D_ObjectSetting
{
    private GameObject _myObject;
    private bool _isDrag;
    private bool _isIncollider;

    public bool IsDrag => _isDrag;

    public D_ObjectSetting(GameObject myObject)
    {
        _myObject = myObject;
    }

    /// <summary>
    /// 物体がドラッグされた時
    /// </summary>
    public void DragMouse()
    {
        _isDrag = true;
        EventBus.Publish<DragGiveObject>(new DragGiveObject(_myObject));
    }

    /// <summary>
    /// マウスが離された時
    /// </summary>
    public void RevertMouse()
    {
        _isDrag = false;
        if (_isIncollider)
            EventBus.Publish<ClickInsideFinish>(new ClickInsideFinish(_myObject));
        else
            EventBus.Publish<ClickOutsideFinish>(new ClickOutsideFinish(_myObject));
    }

    /// <summary>
    /// コライダー内に入った時の処理
    /// </summary>
    public void InCollision()
    {
        _isIncollider = true;
    }

    /// <summary>
    /// コライダーの外に出た時の処理
    /// </summary>
    public void OutCollision()
    {
        _isIncollider = false;
    }
}
