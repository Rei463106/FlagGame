using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonManager : MonoBehaviour, IActBase
{
    [Header("ボタン設定ズ")]
    [SerializeField] private ButtonSetting[] _setting;
    [Header("生成物")]
    [SerializeField] private Buttons _buttons;
    [Header("親オブジェクト")]
    [SerializeField] private GameObject _pObj;

    private Dictionary<ButtonSetting, Buttons> _buttonDic = new();
    private bool _isComplete;
    private int _index;
    private Buttons _current;

    public bool IsComplete => _isComplete;
    private int Result => _index % _setting.Length;

    public void ConnectAct()
    {
        InputManager.EntryInput("Up", OnUp);
        InputManager.EntryInput("Down", OnDown);
        InputManager.EntryInput("Push", OnPush);
        CreateButton();
    }

    private void CreateButton()
    {
        for (int i = 0; i < _setting.Length; i++)
        {
            _current = Instantiate(_buttons);
            _buttonDic.Add(_setting[i], _current);
            _current.transform.parent = _pObj.transform;
            _current.ChangeText(_setting[i].Explain);
        }
    }

    private void ChangeSize()
    {
        var button = _buttonDic.TryGetValue(_setting[Result], out var s);
        s.ChangeSize(s.FirstISize.x * 2, s.FirstISize.y * 2, s.FirstTSize * 2);
        foreach (var t in _buttonDic.Keys)
        {
            if (!t.Equals(_setting[Result]))
                _buttonDic[t].ChangeSize(_buttonDic[t].FirstISize.x, _buttonDic[t].FirstISize.y, _buttonDic[t].FirstTSize);
        }
    }

    private void OnUp(InputAction.CallbackContext i)
    {
        _index--;
        ChangeSize();
    }

    private void OnDown(InputAction.CallbackContext i)
    {
        _index++;
        ChangeSize();
    }

    private void OnPush(InputAction.CallbackContext i)
    {
        EventBus.Publish<NActSendEvent>(new NActSendEvent(_setting[Result].ActBase));
        InputManager.OutInput("Up", OnUp);
        InputManager.OutInput("Down", OnDown);
        InputManager.OutInput("Push", OnPush);

        List<ButtonSetting> list = new();
        foreach (var item in _buttonDic.Keys)
            list.Add(item);
        foreach (var item in list)
            Destroy(_buttonDic[item]);
        _buttonDic.Clear();

        _isComplete = true;
    }
}

public readonly struct NActSendEvent : IGameEvent
{
    public readonly NActBase[] _nAct;
    public NActSendEvent(NActBase[] nAct) => _nAct = nAct;
}
