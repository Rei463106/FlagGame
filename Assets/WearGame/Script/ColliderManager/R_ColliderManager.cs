using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

internal class R_ColliderManager : MonoBehaviour
{
    [Header("対応表")]
    [SerializeField] private TypeSetting[] _settings;

    private bool _isInSend;
    private bool _isOutSend;

    private Dictionary<Parts, GameObject> _colliderDic = new Dictionary<Parts, GameObject>();
    private ClothItem _item;
    private CancellationTokenSource _source;

    private void OnEnable()
    {
        EventBus.Subscribe<DragGiveSettingEvent>(this, ReceiveDragSetting);
        EventBus.Subscribe<ObjectInsideEvent>(this, ReceiveInside);
        EventBus.Subscribe<ObjectOutsideEvent>(this, ReceiveOutside);
    }

    private void OnDisable()
    {
        EventBus.AllUnSubscribe(this);
    }

    private void Start()
    {
        foreach (var pair in _settings)
        {
            _colliderDic[pair.Parts] = pair.Object;
        }
        _source = new CancellationTokenSource();
        WaitUntilflag(_source.Token).Forget();
    }

    private void ReceiveDragSetting(DragGiveSettingEvent d)
    {
        _item = d.Setting;
        foreach (var k in _colliderDic.Keys)
        {
            if (k == _item.Parts)
            {
                _colliderDic[k].GetComponent<R_ColliderSetting>().ReceiveSettingInfo(_item);
                _colliderDic[k].GetComponent<L_ColliderSetting>().ReceiveSettingInfo(_item);
                break;
            }
        }
    }

    private async UniTask WaitUntilflag(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            await UniTask.WaitUntil(() => _isInSend || _isOutSend);

            if (_isInSend)
            {
                _colliderDic[_item.Parts].GetComponent<SpriteRenderer>().sprite =
                     _colliderDic[_item.Parts].GetComponent<L_ColliderSetting>().CurrentSprite;
            }
            else
            {
                _colliderDic[_item.Parts].GetComponent<SpriteRenderer>().sprite =
                     _colliderDic[_item.Parts].GetComponent<L_ColliderSetting>().OldSprite;
            }

            _isInSend = false;
            _isOutSend = false;
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

[Serializable]
internal struct TypeSetting
{
    [Header("Parts")]
    [SerializeField] private Parts _parts;
    [Header("GameObject")]
    [SerializeField] private GameObject _object;

    public Parts Parts => _parts;
    public GameObject Object => _object;
}


