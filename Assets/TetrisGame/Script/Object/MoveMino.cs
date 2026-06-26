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

    private CancellationTokenSource _source;
    private CancellationToken _token;
    private CancellationTokenSource _CSource;
    private CancellationToken _CToken;


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
        _CSource = new CancellationTokenSource();
        _CToken = _CSource.Token;
    }

    private void OnDestroy()
    {
        _actions?.Dispose();
    }

    private void OnLeft(InputAction.CallbackContext context)
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

    private void OnRight(InputAction.CallbackContext context)
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

    private void OnDown(InputAction.CallbackContext context)
    {
        if (_pushUnderType == PushUnder.Move)
        {
            bool isGoing = true;

            foreach (var v in _minoPositions)
            {
                if (!MinoConfirm.JudgeUnder((int)v.x, (int)v.y))
                {
                    isGoing = false;
                    _pushUnderType = PushUnder.None;
                    UnderConfirm(_CToken).Forget();
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
        else
        {
            _CSource.Cancel();
            ObjectFinish();
        }//確認をしてるけどもう一回押されたら終わり
    }

    private void OnLeftRotate(InputAction.CallbackContext context)
    {

    }

    private void OnRightRotate(InputAction.CallbackContext context)
    {

    }

    /// <summary>入室時登録用</summary>
    private void EnterAction()
    {
        transform.position = _mino.SpawnPosition;
        AutoFall(_token).Forget();
    }

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
                        UnderConfirm(_CToken).Forget();
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
    private async UniTask UnderConfirm(CancellationToken token)
    {
        if (!_isRun)//念のため、同時に走らないように
        {
            _isRun = true;
            try
            {
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
                }
                else
                {
                    ObjectFinish();
                }
            }
            catch (OperationCanceledException)
            {
                return;
            }
            finally
            {
                _isRun = false;
            }
        }
    }

    /// <summary>
    /// 終了時
    /// </summary>
    private void ObjectFinish()
    {
        EventBus.Publish<UpdatePositionEvent>(new UpdatePositionEvent());
        Delete();
    }
}

public enum PushUnder
{
    None,
    Move
}
