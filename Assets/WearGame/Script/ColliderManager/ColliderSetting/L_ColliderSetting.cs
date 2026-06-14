using UnityEngine;

internal class L_ColliderSetting : MonoBehaviour
{
    [Header("自分のパーツ")]
    [SerializeField] private Parts _myParts;

    private bool _isHide = true;
    private ClothItem _item;
    private Sprite _currentSprite;
    private Sprite _oldSprite;

    public Sprite CurrentSprite => _currentSprite;
    public Sprite OldSprite => _oldSprite;

    private void OnEnable()
    {
        EventBus.Subscribe<DragGiveRevertEvent>(this, ReceiveMousePos);
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
        _oldSprite = _currentSprite;
        _currentSprite = _item.Sprite;
        _isHide = false;
    }

    private void ReceiveMousePos(DragGiveRevertEvent d)
    {
        _isHide = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_isHide)
        {
            collision.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            TryGetComponent<SpriteRenderer>(out var c);
            c.sprite = _currentSprite;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!_isHide)
        {
            collision.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            TryGetComponent<SpriteRenderer>(out var c);
            c.sprite = _oldSprite;
        }
    }
}
