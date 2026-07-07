/// <summary>
/// Cloth本体の処理を記述するところ(データ)
/// </summary>
internal class D_ObjectSetting
{
    private ClothItem _myCloth;
    private bool _isComplete;

    public ClothItem MyClothSetting => _myCloth;

    /// <summary>
    /// 自分が何か設定する
    /// </summary>
    /// <param name="cloth"></param>
    public void MyCloth(ClothItem cloth)
    {
        _myCloth = cloth;
    }

    /// <summary>
    /// 物体がドラッグされた時
    /// </summary>
    public void DragMouse()
    {
        EventBus.Publish<DragGiveSettingEvent>(new DragGiveSettingEvent(_myCloth));
    }

    /// <summary>
    /// マウスが離されるかどうか見張る
    /// </summary>
    /// <returns></returns>
    public bool WaitUntilRevertMouse()
    {
        return _isComplete;
    }

    /// <summary>
    /// マウスが離された時
    /// </summary>
    public void RevertMouse()
    {
        EventBus.Publish<RevertMouseEvent>(new RevertMouseEvent());
        _isComplete = true;//完了を知らせる
    }

    /// <summary>
    /// 値を元に戻してもらう
    /// </summary>
    public void ReturnValue()
    {
        _isComplete = false;
    }
}
