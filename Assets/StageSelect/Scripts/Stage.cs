using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Stage : MonoBehaviour
{
    [Header("クリアしたかどうか")]
    [SerializeField] private ScriptableObject _clearFlag;
    [Header("移動先")]
    [SerializeField] private string _nextScene;
    [Header("UIの表示")]
    [SerializeField] private MonoBehaviour _stageUI;
    [Header("コライダー")]
    [SerializeField] private Collider2D _co;
    [Header("今のライン")]
    [SerializeField] private List<MoveDirectionSetting> _moveDirection = new();
    [Header("クリア後開くライン")]
    [SerializeField] private List<MoveDirectionSetting> _cMoveDirection = new();
    [Header("PlayerInput")]
    [SerializeField] private PlayerInput _playerI;
    [Header("効果音")]
    [SerializeField] private AudioClip _clip;
    [Header("フェード用")]
    [SerializeField] private SpriteRenderer _sp;

    private IPlayer StageUI => (IPlayer)_stageUI;
    private IResultFlag ResultFlag => (IResultFlag)_clearFlag;

    private Dictionary<MoveDirecrion, LineRenderer> _lDic = new();
    private bool _isInside;

    private void Awake()
    {
        StageUI.PleaseAwake();

        if (ResultFlag.Flag == ClearFlags.Clear)
        {
            foreach (var item in _cMoveDirection)
                _moveDirection.Add(item);

            LineRenderer[] lArray = new LineRenderer[_cMoveDirection.Count];
            for (int i = 0; i < _cMoveDirection.Count; lArray[i] = _cMoveDirection[i].Line, i++) { }
            EventBus.Publish<SendLine>(new SendLine(lArray));
        }

        _playerI.actions["Decision"].started += OnStart;

        foreach (var m in _moveDirection)
            _lDic.TryAdd(m.MType, m.Line);
    }

    private void OnEnable()
    {
        EventBus.Subscribe<MovingEvent>(this, ReceiveMove);
        EventBus.Subscribe<SendMenuBackEvent>(this, ReceiveDirectionFinish);
    }
    private void OnDisable()
    {
        
        EventBus.AllUnSubscribe(this);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!_isInside)
        {
            _isInside = true;
            StageUI.Appear(true);
            StageUI.ChangeStageText();
            EventBus.Publish<SendMoveDirection>(new SendMoveDirection(_lDic));
        }
    }

    private void ReceiveMove(MovingEvent m)
    {
        if (m._isMove)
        {
            _co.enabled = false;
            _isInside = false;
            StageUI.Appear(false);
        }
        else
            _co.enabled = true;
    }

    private bool _isStart;//何回も押せないようにする

    private void ReceiveDirectionFinish(SendMenuBackEvent s) => _isStart = false;

    private void OnStart(InputAction.CallbackContext c)
    {
        if (_isInside && !_isStart)
            StartAnim().Forget();
    }

    private async UniTask StartAnim()
    {
        _isStart = true;
        _playerI.actions["Decision"].started -= OnStart;
        GetComponent<AudioSource>().PlayOneShot(_clip);
        _playerI.enabled = false;
        EventBus.Publish<SendStart>(new SendStart());
        Tween tween = _sp.DOFade(1f, 2f);
        await tween.AsyncWaitForCompletion();
        SceneManager.LoadScene(_nextScene);
    }
}

[Serializable]
public class MoveDirectionSetting
{
    [Header("タイプ")]
    [SerializeField] private MoveDirecrion _mType;
    [Header("Line")]
    [SerializeField] private LineRenderer _line;

    public MoveDirecrion MType => _mType;
    public LineRenderer Line => _line;
}

public readonly struct SendMoveDirection : IGameEvent
{
    public readonly Dictionary<MoveDirecrion, LineRenderer> LDic;

    public SendMoveDirection(Dictionary<MoveDirecrion, LineRenderer> d) => LDic = d;
}

public readonly struct SendLine : IGameEvent
{
    public readonly LineRenderer[] _renderer;
    public SendLine(LineRenderer[] renderer) => _renderer = renderer;
}

public readonly struct SendStart : IGameEvent { }

public enum MoveDirecrion
{
    Up,
    Down,
    Left,
    Right
}

