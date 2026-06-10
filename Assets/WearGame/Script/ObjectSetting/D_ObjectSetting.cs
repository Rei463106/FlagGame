/// <summary>
/// Cloth本体の処理を記述するところ(データ)
/// </summary>
public class D_ObjectSetting
{
    private bool _isDrag;
    private bool _isIncollider;

    public bool IsDrag => _isDrag;

    /// <summary>
    /// 物体がドラッグされた時
    /// </summary>
    public void DragMouse()
    {
        _isDrag = true;
    }

    /// <summary>
    /// マウスが離された時
    /// </summary>
    public void RevertMouse()
    {
        if (_isIncollider)
            EventBus.Publish<ClickInsideFinish>(new ClickInsideFinish());
        else
            EventBus.Publish<ClickOutsideFinish>(new ClickOutsideFinish());
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
