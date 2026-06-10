
/// <summary>
/// DataOriginのデータ処理
/// </summary>
internal class D_ObjectOrigin
{
    private bool _isMousePush = false;//マウスを押したかのフラグ

    public bool IsMousePush => _isMousePush;

    /// <summary>
    /// マウスが押されたら
    /// </summary>
    public void PushMouse()
    {
        _isMousePush = true;
    }

    /// <summary>
    /// マウスが離されたら
    /// </summary>
    public void RevertMouse()
    {
        _isMousePush = false;
    }
}
