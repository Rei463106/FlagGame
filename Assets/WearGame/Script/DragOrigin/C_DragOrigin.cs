internal class C_DragOrigin 
{
    private bool _isDragging = false;

    public bool IsDragging => _isDragging;

    /// <summary>
    /// マウスが押されたら
    /// </summary>
    public void PushMouse()
    {
        _isDragging = true;
    }

    /// <summary>
    /// マウスが離されたら
    /// </summary>
    public void RevertMouse()
    {
        _isDragging = false;
    }
}
