using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

internal class R_ColliderManager : MonoBehaviour
{
    [Header("対応表")]
    [SerializeField] private ForTypeDic _settings;

    private bool _isInSend;
    private bool _isOutSend;

    private Dictionary<Parts, Setting> _colliderDic = new Dictionary<Parts, Setting>();
    private ClothItem _item;
    private CancellationTokenSource _source;

    private void OnEnable()
    {
        EventBus.Subscribe<DragGiveSettingEvent>(this, ReceiveDragSetting);
        EventBus.Subscribe<ObjectInsideEvent>(this, ReceiveInside);
        EventBus.Subscribe<ObjectOutsideEvent>(this, ReceiveOutside);
        EventBus.Subscribe<CorrectEvent>(this, ReceiveCorrect);
        EventBus.Subscribe<DustBoxEvent>(this, ReceiveDustBox);
    }

    private void OnDisable()
    {
        EventBus.AllUnSubscribe(this);
    }

    private void Start()
    {
        foreach (var pair in _settings.Settings)
        {
            _colliderDic[pair.Parts] = pair.Setting;
        }
        _source = new CancellationTokenSource();
        WaitUntilflag(_source.Token).Forget();
    }

    private void ReceiveCorrect(CorrectEvent co)
    {
        ForCorrectTask(_source.Token).Forget();
    }

    private async UniTask ForCorrectTask(CancellationToken ca)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(2));//演出により秒数変わるかも

        foreach (var c in _colliderDic.Keys)
        {
            _colliderDic[c].Sp.sprite = null;
        }

        //ここでイベント発行すればよさそうだけど…
    }

    private void ReceiveDustBox(DustBoxEvent d)
    {
        foreach (var c in _colliderDic.Keys)
        {
            _colliderDic[c].Sp.sprite = null;
        }
    }

    private void ReceiveDragSetting(DragGiveSettingEvent d)
    {
        _item = d.Setting;
        foreach (var k in _colliderDic.Keys)
        {
            if (k == _item.Parts)
            {
                _colliderDic[k].RColliderSetting.ReceiveSettingInfo(_item);
                _colliderDic[k].LColliderSetting.ReceiveSettingInfo(_item);
                break;
            }
        }
    }

    private async UniTask WaitUntilflag(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            await UniTask.WaitUntil(() => _isInSend || _isOutSend);

            if (_item != null)
            {
                if (_isInSend)
                {
                    _colliderDic[_item.Parts].Sp.sprite =
                         _colliderDic[_item.Parts].LColliderSetting.CurrentSprite;
                }
                else
                {
                    _colliderDic[_item.Parts].Sp.sprite =
                         _colliderDic[_item.Parts].LColliderSetting.OldSprite;
                }
                _isInSend = false;
                _isOutSend = false;
            }

            await UniTask.Yield();
        }
    }

    private void ReceiveInside(ObjectInsideEvent o)
    {
        _isInSend = true;
    }

    private void ReceiveOutside(ObjectOutsideEvent o)
    {
        _isOutSend = true;
    }
}

//以下、取得用

[Serializable]
internal struct ForTypeDic
{
    [Header("SettingList")]
    [SerializeField] private PartsSetting[] _setting;

    public PartsSetting[] Settings => _setting;
}

[Serializable]
internal struct PartsSetting
{
    [Header("Parts")]
    [SerializeField] private Parts _parts;
    [Header("Setting")]
    [SerializeField] private Setting _setting;

    public Parts Parts => _parts;
    public Setting Setting => _setting;
}

[Serializable]
internal struct Setting
{
    [Header("SpriteRenderer")]
    [SerializeField] private SpriteRenderer _sp;
    [Header("R_Collider")]
    [SerializeField] private R_ColliderSetting _setting;
    [Header("L_Collider")]
    [SerializeField] private L_ColliderSetting _colliderSetting;

    public SpriteRenderer Sp => _sp;
    public L_ColliderSetting LColliderSetting => _colliderSetting;
    public R_ColliderSetting RColliderSetting => _setting;
}


