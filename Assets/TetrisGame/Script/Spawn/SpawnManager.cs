using System;
using UnityEngine;

/// <summary>
/// Minoをスポーンさせる処理
/// </summary>
public class SpawnManager : MinoSelect, IStateEvent
{
    public event Action<StateEnum> StateChanged;

    public StateEnum State => StateEnum.Spawn;

    private void Awake()
    {
        PleaseAwake();
        StateMachine.Entry<SpawnManager>(this);
    }

    private void OnEnable() => EventBus.Subscribe<HoldAction>(this, HoldWaiter);
    private void OnDisable() => EventBus.AllUnSubscribe(this);

    private GameObject _currentPrefab;
    private int _currentNumber;
    private int _holdNumber = -1;

    public void Starter()
    {
        _isHold = false;
        _currentPrefab = Instantiate(SendMino(out int p));
        _currentNumber = p;
        RegistrationMino();
        StateChanged?.Invoke(StateEnum.Confirm);
    }

    private void RegistrationMino()
    {
        if (_currentPrefab.TryGetComponent<DoorMino>(out var d))
        {
            if (d == null) return;
            var enterMino = d;
            enterMino.Enter(_isHold, () => Destroy(_currentPrefab));
        }
    }

    [Header("HoldSprite")]
    [SerializeField] private SpriteRenderer _sprite;

    private bool _isHold;

    private void HoldWaiter(HoldAction h)
    {
        if (!_isHold)
        {
            _isHold = true;
            var g = _holdNumber;
            _holdNumber = _currentNumber;

            if (g != -1)
                _currentPrefab = Instantiate(HoldMino(g));
            else
                _currentPrefab = Instantiate(SendMino(out var p));

            _sprite.sprite = HoldSprite(_holdNumber);
            RegistrationMino();
        }
    }
}
