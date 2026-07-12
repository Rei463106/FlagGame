using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSprite : MonoBehaviour
{
    [Header("PlayerInput")]
    [SerializeField] private PlayerInput _playerI;
    [Header("left")]
    [SerializeField] private Sprite _left;
    [Header("right")]
    [SerializeField] private Sprite _right;
    [Header("up")]
    [SerializeField] private Sprite _up;
    [Header("down")]
    [SerializeField] private Sprite _down;

    private void Awake()
    {
        _playerI.actions["Left"].started += OnLeft;
        _playerI.actions["Right"].started += OnRight;
        _playerI.actions["Up"].started += OnUp;
        _playerI.actions["Down"].started += OnDown;
    }

    private void OnEnable() => EventBus.Subscribe<SendStart>(this, ReceiveStart);

    private void OnDisable()
    { 
        EventBus.AllUnSubscribe(this);
        _playerI.actions["Left"].started -= OnLeft;
        _playerI.actions["Right"].started -= OnRight;
        _playerI.actions["Up"].started -= OnUp;
        _playerI.actions["Down"].started -= OnDown;
    }

    private void ReceiveStart(SendStart s)
    {
        _playerI.actions["Left"].started -= OnLeft;
        _playerI.actions["Right"].started -= OnRight;
        _playerI.actions["Up"].started -= OnUp;
        _playerI.actions["Down"].started -= OnDown;
    }

    private void OnLeft(InputAction.CallbackContext i) => GetComponent<SpriteRenderer>().sprite = _left;
    private void OnRight(InputAction.CallbackContext i) => GetComponent<SpriteRenderer>().sprite = _right;
    private void OnUp(InputAction.CallbackContext i) => GetComponent<SpriteRenderer>().sprite = _up;
    private void OnDown(InputAction.CallbackContext i) => GetComponent<SpriteRenderer>().sprite = _down;
}
