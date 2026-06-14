using UnityEngine;

internal class R_ColliderSetting : MonoBehaviour
{
    [Header("自分のパーツ")]
    [SerializeField] private Parts _myParts;

    private ClothItem _item;

    private void OnEnable()
    {
        EventBus.Subscribe<RevertMouseEvent>(this, ReceiveRevertMouse);
    }

    private void OnDisable()
    {
        EventBus.AllUnSubscribe(this);
    }

    /// <summary>
    /// Managerから情報を受け取る
    /// </summary>
    public void ReceiveSettingInfo(ClothItem item)
    {
        _item = item;
    }

    /// <summary>
    /// 内外どっちで離されたかの判定
    /// </summary>
    /// <param name="r"></param>
    private void ReceiveRevertMouse(RevertMouseEvent r)
    {
        if (_item != null && _item.Parts == _myParts)
        {
            TryGetComponent<Collider2D>(out var c);
            var left = c.bounds.min.x;
            var right = c.bounds.max.x;
            var bottom = c.bounds.min.y;
            var top = c.bounds.max.y;

            if (left <= r._vector.x && r._vector.x <= right && bottom <= r._vector.y && r._vector.y <= top)
                EventBus.Publish<ObjectInsideEvent>(new ObjectInsideEvent());
            else
                EventBus.Publish<ObjectOutsideEvent>(new ObjectOutsideEvent());
        }
    }
}
