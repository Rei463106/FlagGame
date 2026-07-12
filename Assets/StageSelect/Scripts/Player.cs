using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("PlayerInput")]
    [SerializeField] private PlayerInput _playerI;
    [Header("初期位置")]
    [SerializeField] private Vector2 _firstPos;

    private bool _isMove = true;
    private static bool _isFirst = true;
    private static Vector2 _finalPos;

    private void Awake()
    {
        if (_isFirst)
        {
            _isFirst = false;
            transform.position = _firstPos;
        }
        else
            transform.position = _finalPos;

        _playerI.actions["Left"].started += OnLeft;
        _playerI.actions["Right"].started += OnRight;
        _playerI.actions["Up"].started += OnUp;
        _playerI.actions["Down"].started += OnDown;
    }

    private void OnEnable()
    {
        EventBus.Subscribe<SendMoveDirection>(this, ReceiveLine);
        EventBus.Subscribe<SendStart>(this, ReceiveStart);
        EventBus.Subscribe<SendMenuBackEvent>(this, ReceiveDirectionFinish);
    }

    private void OnDisable()
    {
        _playerI.actions["Left"].started -= OnLeft;
        _playerI.actions["Right"].started -= OnRight;
        _playerI.actions["Up"].started -= OnUp;
        _playerI.actions["Down"].started -= OnDown;
        EventBus.AllUnSubscribe(this);
    }

    private Dictionary<MoveDirecrion, LineRenderer> _ldic = new();

    private void ReceiveLine(SendMoveDirection s) => _ldic = s.LDic;

    private async UniTask WaitMove(Vector3[] path, float time)
    {
        Tween tween = transform.DOPath(path, time);
        await tween.AsyncWaitForCompletion();
        _isMove = true;
        EventBus.Publish<MovingEvent>(new MovingEvent(false));
    }

    private void ReceiveInput(MoveDirecrion m, float time)
    {
        if (_ldic.TryGetValue(m, out var t))
        {
            var line = t;
            var path = new Vector3[line.positionCount];
            for (int i = 0; i < path.Length; path[i] = line.GetPosition(i), i++) { }
            _finalPos = new Vector2(path[path.Length - 1].x, path[path.Length - 1].y);
            if (transform.position != path[path.Length - 1])
            {
                _isMove = false;
                EventBus.Publish<MovingEvent>(new MovingEvent(true));
                WaitMove(path, time).Forget();
            }
        }
    }

    private void ReceiveStart(SendStart s)
    { 
        _isMove = false;
        _playerI.actions["Left"].started -= OnLeft;
        _playerI.actions["Right"].started -= OnRight;
        _playerI.actions["Up"].started -= OnUp;
        _playerI.actions["Down"].started -= OnDown;
    }

    private void ReceiveDirectionFinish(SendMenuBackEvent s) => _isMove = true;

    private void OnLeft(InputAction.CallbackContext c)
    {
        if (_isMove) ReceiveInput(MoveDirecrion.Left, 1f);
    }

    private void OnRight(InputAction.CallbackContext c)
    {
        if (_isMove) ReceiveInput(MoveDirecrion.Right, 1f);
    }

    private void OnUp(InputAction.CallbackContext c)
    {
        if (_isMove) ReceiveInput(MoveDirecrion.Up, 1f);
    }

    private void OnDown(InputAction.CallbackContext c)
    {
        if (_isMove) ReceiveInput(MoveDirecrion.Down, 1f);
    }
}

public struct MovingEvent : IGameEvent
{
    public readonly bool _isMove;
    public MovingEvent(bool isMove) => _isMove = isMove;
}
