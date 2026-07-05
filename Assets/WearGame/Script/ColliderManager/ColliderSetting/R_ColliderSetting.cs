using UnityEngine;
using UnityEngine.InputSystem;

public class R_ColliderSetting : MonoBehaviour
{
    [Header("自分のタイプ")]
    [SerializeField] private Parts _myParts;
    [Header("SpriteRenderer")]
    [SerializeField] private SpriteRenderer _sp;
    [Header("Collider2D")]
    [SerializeField] private Collider2D _co;

    private bool _isOK;
    private Sprite _oldSprite;
    private ClothItem _myItem;

    private void OnEnable()
    {
        EventBus.Subscribe<DragGiveSettingEvent>(this, Reception);
        EventBus.Subscribe<RevertMouseEvent>(this, ReceiveRevert);
        EventBus.Subscribe<CorrectEvent>(this, ReceiveCorrect);
        EventBus.Subscribe<DustBoxEvent>(this, ReceiveDustBox);
    }

    private void OnDisable() => EventBus.AllUnSubscribe(this);

    private void Reception(DragGiveSettingEvent d)
    {
        if (d.Setting.Parts == _myParts)
        {
            _isOK = true;
            _myItem = d.Setting;
        }
        else
            _isOK = false;
    }

    private void ReceiveRevert(RevertMouseEvent r)
    {
        if (_isOK)
        {
            _isOK = false;
            var left = _co.bounds.min.x;
            var right = _co.bounds.max.x;
            var bottom = _co.bounds.min.y;
            var top = _co.bounds.max.y;

            var mouse = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector2 mousePos = new(mouse.x, mouse.y);

            if (left <= mousePos.x && mousePos.x <= right && bottom <= mousePos.y && mousePos.y <= top)
            {
                _sp.sprite = _myItem.Sprite;
                _oldSprite = _sp.sprite;
                EventBus.Publish<ObjectInsideEvent>(new ObjectInsideEvent());
            }
        }
    }

    private void ReceiveCorrect(CorrectEvent c)
    {
        _oldSprite = null;
        _sp.sprite = null;
    }

    private void ReceiveDustBox(DustBoxEvent d)
    {
        _oldSprite = null;
        _sp.sprite = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isOK)
        {
            collision.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            _sp.sprite = _myItem.Sprite;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_isOK)
        {
            collision.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            _sp.sprite = _oldSprite;
        }
    }
}
