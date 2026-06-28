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

    private List<Vector2> _minoPositions = new();
    private Vector2 _rotatePosition;
    private MoveActions _actions;
    private PushUnder _pushUnderType = PushUnder.Move;
    private bool _isRun;
    private bool _isPush = true;

    private CancellationTokenSource _source;
    private CancellationToken _token;

    private void OnEnable()
    {
        InsideEnterAction += EnterAction;
    }

    private void OnDisable()
    {
        InsideEnterAction -= EnterAction;
    }

    /// <summary>
    /// 入力受付
    /// </summary>
    private void Awake()
    {
        //入力
        _actions = new MoveActions();

        _actions.InputMove.Left.started += OnLeft;
        _actions.InputMove.Right.started += OnRight;
        _actions.InputMove.Down.started += OnDown;
        _actions.InputMove.LeftRotate.started += OnLeftRotate;
        _actions.InputMove.RightRotate.started += OnRightRotate;

        _actions.Enable();

        //初期位置
        foreach (var v in _mino.MSetting)
        {
            var m = _mino.SpawnPosition + v.DisplacePostion;
            _minoPositions.Add(new Vector2(m.x, -m.y));
        }

        _rotatePosition = _mino.SpawnPosition + _mino.DisplaceRotate;

        _source = new CancellationTokenSource();
        _token = _source.Token;
    }

    private void OnDestroy()
    {
        _actions?.Dispose();
    }

    private void OnLeft(InputAction.CallbackContext context)
    {
        if (_isPush)
        {
            bool isGoing = true;

            foreach (var v in _minoPositions)
            {
                if (!MinoConfirm.JudgeLeft((int)v.x, (int)v.y))
                {
                    isGoing = false;
                    break;
                }
            }

            if (isGoing)
            {
                var v = transform.position;
                transform.position = new Vector2(v.x - 1, v.y);

                for (int i = 0; i < _minoPositions.Count; i++)
                {
                    var newV = _minoPositions[i];
                    _minoPositions[i] = new Vector2(newV.x - 1, newV.y);
                }//現在位置を更新する

                //回転軸も更新する
                var r = _rotatePosition;
                _rotatePosition = new Vector2(r.x - 1, r.y);
            }
        }
    }

    private void OnRight(InputAction.CallbackContext context)
    {
        if (_isPush)
        {
            bool isGoing = true;

            foreach (var v in _minoPositions)
            {
                if (!MinoConfirm.JudgeRight((int)v.x, (int)v.y))
                {
                    isGoing = false;
                    break;
                }
            }

            if (isGoing)
            {
                var v = transform.position;
                transform.position = new Vector2(v.x + 1, v.y);

                for (int i = 0; i < _minoPositions.Count; i++)
                {
                    var newV = _minoPositions[i];
                    _minoPositions[i] = new Vector2(newV.x + 1, newV.y);
                }//現在位置を更新する

                //回転軸も更新する
                var r = _rotatePosition;
                _rotatePosition = new Vector2(r.x + 1, r.y);
            }
        }
    }

    private void OnDown(InputAction.CallbackContext context)
    {
        if (_pushUnderType == PushUnder.Move || _isPush)
        {
            bool isGoing = true;

            foreach (var v in _minoPositions)
            {
                if (!MinoConfirm.JudgeUnder((int)v.x, (int)v.y))
                {
                    isGoing = false;
                    _pushUnderType = PushUnder.None;
                    UnderConfirm().Forget();
                    break;
                }
            }

            if (isGoing)
            {
                var v = transform.position;
                transform.position = new Vector2(v.x, v.y - 1);

                for (int i = 0; i < _minoPositions.Count; i++)
                {
                    var newV = _minoPositions[i];
                    _minoPositions[i] = new Vector2(newV.x, newV.y + 1);
                }//現在位置を更新する

                //回転軸も更新する
                var r = _rotatePosition;
                _rotatePosition = new Vector2(r.x, r.y - 1);
            }
        }
    }

    private void OnLeftRotate(InputAction.CallbackContext context)
    {
        if (_isPush)
        {
            bool isGoing = true;
            List<Vector2> vList = new();

            foreach (var v in _minoPositions)
            {
                if (!MinoConfirm.JudgeLeftRotate((int)v.x, (int)v.y, (int)_rotatePosition.x, (int)-_rotatePosition.y, out int c, out int l))
                {
                    vList.Add(new Vector2(c, l));
                }
                else
                {
                    isGoing = false;
                    break;
                }
            }

            if (isGoing)
            {
                //左に90度回転させる
                transform.Rotate(new Vector3(0, 0, 90));
                _minoPositions.Clear();
                _minoPositions.AddRange(vList);
            }
        }
    }

    private void OnRightRotate(InputAction.CallbackContext context)
    {
        if (_isPush)
        {
            bool isGoing = true;
            List<Vector2> vList = new();

            foreach (var v in _minoPositions)
            {
                if (MinoConfirm.JudgeRightRotate((int)v.x, (int)v.y, (int)_rotatePosition.x, (int)-_rotatePosition.y, out int c, out int l))
                {
                    vList.Add(new Vector2(c, l));
                }
                else
                {
                    isGoing = false;
                    break;
                }
            }

            if (isGoing)
            {
                //左に90度回転させる
                transform.Rotate(new Vector3(0, 0, -90));
                _minoPositions.Clear();
                _minoPositions.AddRange(vList);
            }
        }
    }

    /// <summary>入室時登録用</summary>
    private void EnterAction()
    {
        _isRun = false;
        transform.position = _mino.SpawnPosition;
        _pushUnderType = PushUnder.Move;
        AutoFall(_token).Forget();
        _isPush = true;
    }

    /// <summary>自動落下</summary>
    private async UniTask AutoFall(CancellationToken token)
    {
        try
        {
            while (!_token.IsCancellationRequested)
            {
                bool isGoing = true;

                foreach (var v in _minoPositions)
                {
                    if (!MinoConfirm.JudgeUnder((int)v.x, (int)v.y))
                    {
                        isGoing = false;
                        _pushUnderType = PushUnder.None;
                        UnderConfirm().Forget();
                        break;
                    }
                }

                if (isGoing)
                {
                    var v = transform.position;
                    transform.position = new Vector2(v.x, v.y - 1);//配列と位置のy座標は逆

                    for (int i = 0; i < _minoPositions.Count; i++)
                    {
                        var newV = _minoPositions[i];
                        _minoPositions[i] = new Vector2(newV.x, newV.y + 1);
                    }

                    var r = _rotatePosition;
                    _rotatePosition = new Vector2(r.x, r.y - 1);
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
            await UniTask.Delay(TimeSpan.FromSeconds(1f));//1秒だけ待って確かめ

            bool isGoing = true;
            foreach (var v in _minoPositions)
            {
                if (!MinoConfirm.JudgeUnder((int)v.x, (int)v.y))
                {
                    isGoing = false;
                    break;
                }
            }

            if (isGoing)
            {
                AutoFall(_token).Forget();//なかったら再び動かす
                _pushUnderType = PushUnder.Move;//成功時は再び押せるように
                _isRun = false;
            }
            else
            {
                ObjectFinish();
            }
        }
    }

    /// <summary>
    /// 終了時
    /// </summary>
    private void ObjectFinish()
    {
        _isPush = false;
        EventBus.Publish<UpdatePositionEvent>(new UpdatePositionEvent());
        Delete();
    }
}

public enum PushUnder
{
    None,
    Move
}
