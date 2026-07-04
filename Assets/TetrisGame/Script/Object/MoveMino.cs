using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveMino : DoorMino
{
    [Header("SO")]
    [SerializeField] private Mino _mino;
    [Header("回転軸")]
    [SerializeField] private Transform _rotate;
    [Header("自分自身")]
    [SerializeField] private GameObject _myObject;

    private readonly List<Vector2> _currentPos = new();
    private Vector2 _currentRotatePos;

    private MoveActions _actions;

    private PushType _pushType = PushType.None;
    private bool _isRun;

    private CancellationTokenSource _source;
    private CancellationToken _token;

    private event Action MoveAction;

    private void OnEnable()
    {
        InsideEnterAction += EnterAction;
        MoveAction += PlayClip;
        EventBus.Subscribe<TimerEvent>(this, ReceiveTimer);
    }

    private void OnDisable()
    {
        InsideEnterAction -= EnterAction;
        MoveAction -= PlayClip;
        EventBus.AllUnSubscribe(this);
    }

    /// <summary>
    /// 入力受付
    /// </summary>
    private void Awake()
    {
        PleaseAwake();

        //入力
        _actions = new MoveActions();

        _actions.InputMove.Left.started += OnLeft;
        _actions.InputMove.Right.started += OnRight;
        _actions.InputMove.Down.started += OnDown;
        _actions.InputMove.LeftRotate.started += OnLeftRotate;
        _actions.InputMove.RightRotate.started += OnRightRotate;
        _actions.InputMove.Hold.started += OnHold;

        _actions.Enable();

        //初期位置
        foreach (var v in _mino.MSetting)
        {
            var m = _mino.SpawnPosition + v.DisplacePostion;
            _currentPos.Add(m);
        }

        _currentRotatePos = _mino.SpawnPosition + _mino.DisplaceRotate;

        _source = new CancellationTokenSource();
        _token = _source.Token;
    }

    private void OnDestroy() => _actions?.Dispose();

    /// <summary>入室時</summary>
    private void EnterAction()
    {
        transform.position = _mino.SpawnPosition;
        _isRun = false;
        _pushType = PushType.All;
        AutoFall(_token).Forget();
    }

    /// <summary>退室時</summary>
    private void FinishAction()
    {
        _pushType = PushType.None;
        EventBus.Publish<SendPositionEvent>(new SendPositionEvent(_currentPos, _mino.SpawnSprite));
        Delete();
    }

    /// <summary>時間切れ</summary>
    private void ReceiveTimer(TimerEvent t)
    {
        _source.Cancel();
        Destroy(_myObject);
    }

    /// <summary>
    /// ホールド
    /// </summary>
    /// <param name="context"></param>
    private void OnHold(InputAction.CallbackContext context)
    {
        if (_isHold)
            return;

        EventBus.Publish<HoldAction>(new HoldAction());
        _source.Cancel();
        Destroy(_myObject);
    }

    /// <summary>
    /// 左
    /// </summary>
    /// <param name="context"></param>
    private void OnLeft(InputAction.CallbackContext context)
    {
        MoveAction.Invoke();
        if (_pushType != PushType.None)
        {
            bool isGoing = true;

            foreach (var v in _currentPos)
            {
                if (!MinoConfirm.JudgeLeft(v))
                {
                    isGoing = false;
                    break;
                }
            }

            if (isGoing)
            {
                var v = transform.position;
                transform.position = new Vector2(v.x - 1, v.y);

                for (int i = 0; i < _currentPos.Count; i++)
                {
                    var newV = _currentPos[i];
                    _currentPos[i] = new Vector2(newV.x - 1, newV.y);
                }

                var r = _currentRotatePos;
                _currentRotatePos = new Vector2(r.x - 1, r.y);
            }
        }
    }

    /// <summary>
    /// 右
    /// </summary>
    /// <param name="context"></param>
    private void OnRight(InputAction.CallbackContext context)
    {
        MoveAction.Invoke();
        if (_pushType != PushType.None)
        {
            bool isGoing = true;

            foreach (var v in _currentPos)
            {
                if (!MinoConfirm.JudgeRight(v))
                {
                    isGoing = false;
                    break;
                }
            }

            if (isGoing)
            {
                var v = transform.position;
                transform.position = new Vector2(v.x + 1, v.y);

                for (int i = 0; i < _currentPos.Count; i++)
                {
                    var newV = _currentPos[i];
                    _currentPos[i] = new Vector2(newV.x + 1, newV.y);
                }//現在位置を更新する

                //回転軸も更新する
                var r = _currentRotatePos;
                _currentRotatePos = new Vector2(r.x + 1, r.y);
            }
        }
    }

    /// <summary>
    /// 下
    /// </summary>
    /// <param name="context"></param>
    private void OnDown(InputAction.CallbackContext context)
    {
        MoveAction.Invoke();
        if (_pushType == PushType.All)
        {
            bool isGoing = true;

            foreach (var v in _currentPos)
            {
                if (!MinoConfirm.JudgeUnder(v))
                {
                    isGoing = false;
                    _pushType = PushType.ProUnder;
                    UnderConfirm().Forget();
                    break;
                }
            }

            if (isGoing)
            {
                var v = transform.position;
                transform.position = new Vector2(v.x, v.y - 1);

                for (int i = 0; i < _currentPos.Count; i++)
                {
                    var newV = _currentPos[i];
                    _currentPos[i] = new Vector2(newV.x, newV.y - 1);
                }

                var r = _currentRotatePos;
                _currentRotatePos = new Vector2(r.x, r.y - 1);
            }
        }
    }

    /// <summary>
    /// 左回転
    /// </summary>
    /// <param name="context"></param>
    private void OnLeftRotate(InputAction.CallbackContext context)
    {
        MoveAction.Invoke();
        if (_pushType != PushType.None)
        {
            bool isGoing = true;
            List<Vector2> vList = new();

            foreach (var v in _currentPos)
            {
                if (MinoConfirm.JudgeLeftRotate(v, _currentRotatePos, out Vector2 ve))
                    vList.Add(ve);
                else
                {
                    isGoing = false;
                    break;
                }
            }

            if (isGoing)
            {
                transform.RotateAround(_rotate.position, Vector3.forward, 90f);
                _currentPos.Clear();
                _currentPos.AddRange(vList);
            }
        }
    }

    /// <summary>
    /// 右回転
    /// </summary>
    /// <param name="context"></param>
    private void OnRightRotate(InputAction.CallbackContext context)
    {
        MoveAction.Invoke();
        if (_pushType != PushType.None)
        {
            bool isGoing = true;
            List<Vector2> vList = new();

            foreach (var v in _currentPos)
            {
                if (MinoConfirm.JudgeRightRotate(v, _currentRotatePos, out Vector2 ve))
                    vList.Add(ve);
                else
                {
                    isGoing = false;
                    break;
                }
            }

            if (isGoing)
            {
                transform.RotateAround(_rotate.position, Vector3.forward, -90f);
                _currentPos.Clear();
                _currentPos.AddRange(vList);
            }
        }
    }

    /// <summary>自動落下</summary>
    private async UniTask AutoFall(CancellationToken token)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: token);
        try
        {
            while (!_token.IsCancellationRequested)
            {
                bool isGoing = true;

                foreach (var v in _currentPos)
                {
                    if (!MinoConfirm.JudgeUnder(v))
                    {
                        _pushType = PushType.ProUnder;
                        UnderConfirm().Forget();
                        isGoing = false;
                        break;
                    }
                }

                if (isGoing)
                {
                    var v = transform.position;
                    transform.position = new Vector2(v.x, v.y - 1);//配列と位置のy座標は逆

                    for (int i = 0; i < _currentPos.Count; i++)
                    {
                        var newV = _currentPos[i];
                        _currentPos[i] = new Vector2(newV.x, newV.y - 1);
                    }

                    var r = _currentRotatePos;
                    _currentRotatePos = new Vector2(r.x, r.y - 1);
                }

                await UniTask.Delay(TimeSpan.FromSeconds(1f), cancellationToken: token);
            }
        }
        catch (OperationCanceledException)
        {
            return;
        }
    }

    /// <summary>
    /// 下に落ちるときの確認用メソッド
    /// </summary>
    /// <returns></returns>
    private async UniTask UnderConfirm()
    {
        if (!_isRun)//念のため、同時に走らないように
        {
            _isRun = true;
            _source.Cancel();
            bool isGoing = true;

            await UniTask.Delay(TimeSpan.FromSeconds(0.5f));

            foreach (var v in _currentPos)
            {
                if (!MinoConfirm.JudgeUnder(v))
                {
                    isGoing = false;
                    break;
                }
            }

            if (isGoing)
            {
                _isRun = false;
                _source = new CancellationTokenSource();
                _token = _source.Token;
                AutoFall(_token).Forget();
                _pushType = PushType.All;
            }
            else
            {
                FinishAction();
            }
        }
    }
}

public enum PushType
{
    None,
    All,
    ProUnder
}

public readonly struct HoldAction : IGameEvent { }
