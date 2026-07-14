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
    [Header("CharcterObj")]
    [SerializeField] private CharacterObj _chr;
    [Header("NormalNovel")]
    [SerializeField] private NNovel[] _normal;

    private Dictionary<ButtonSetting, Buttons> _buttonDic = new();
    private List<CharacterObj> _chrList = new();
    private bool _isComplete;
    private int _index;
    private Buttons _currentB;
    private CharacterObj _currentC;

    public bool IsComplete => _isComplete;
    private int Result => (_index % _setting.Length + _setting.Length) % _setting.Length;

    public void ConnectAct()
    {
        InputManager.EntryInput("Up", OnUp);
        InputManager.EntryInput("Down", OnDown);
        InputManager.EntryInput("Push", OnPush);
        CreateButton();
        CreateBack();
    }

    private void CreateButton()
    {
        for (int i = 0; i < _setting.Length; i++)
        {
            _currentB = Instantiate(_buttons);
            _buttonDic.Add(_setting[i], _currentB);
            _currentB.transform.SetParent(_pObj.transform, false);
            _currentB.ChangeText(_setting[i].Explain);
        }
    }

    private void CreateBack()
    {
        for (int i = 0; i < _normal.Length; i++)
        {
            _currentC = Instantiate(_chr);
            _currentC.SetSprite(_normal[i].Sprite);
            _currentC.SetPosition(_normal[i].Pos);
            _currentC.ChangeA(0.4f);
            _chrList.Add(_currentC);
        }
    }

    private void ChangeSize()
    {
        var button = _buttonDic.TryGetValue(_setting[Result], out var s);
        s.ChangeSize(s.FirstISize.x * 1.3f, s.FirstISize.y * 1.3f, s.FirstTSize + 5);
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
        List<ButtonSetting> list = new();
        foreach (var item in _buttonDic.Keys)
            list.Add(item);
        foreach (var item in list)
            _buttonDic[item].DestroyObj();
        _buttonDic.Clear();
        foreach (var item in _chrList)
            item.DestroyObj();
        _chrList.Clear();

        EventBus.Publish<NActSendEvent>(new NActSendEvent(_setting[Result].ActBase));
        InputManager.OutInput("Up", OnUp);
        InputManager.OutInput("Down", OnDown);
        InputManager.OutInput("Push", OnPush);

        _isComplete = true;
    }
}

public readonly struct NActSendEvent : IGameEvent
{
    public readonly NActBase[] _nAct;
    public NActSendEvent(NActBase[] nAct) => _nAct = nAct;
}


