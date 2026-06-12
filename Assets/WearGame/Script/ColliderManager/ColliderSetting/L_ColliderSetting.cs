using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class L_ColliderSetting : MonoBehaviour
{
    private Sprite _currentSprite;
    private bool _isExecute;

    private void OnEnable()
    {
        EventBus.Subscribe<DragGiveSetting>(this, ReceiveClothInfo);
        EventBus.Subscribe<ClickInsideFinish>(this, ReceiveInsideInfo);
        EventBus.Subscribe<ClickOutsideFinish>(this, ReceiveOutSideInfo);
        GetComponent<SpriteRenderer>().sprite = null;
    }

    private void OnDisable()
    {
        EventBus.AllUnSubscribe(this);
    }

    private void ReceiveClothInfo(DragGiveSetting s)
    {
        _currentSprite = s.Setting.Sprite;
    }

    private void ReceiveInsideInfo(ClickInsideFinish c)
    {
        DelayFlag().Forget();
    }

    private void ReceiveOutSideInfo(ClickOutsideFinish c)
    {
        DelayFlag().Forget();
    }

    private async UniTask DelayFlag()
    {
        if (!_isExecute)
        {
            _isExecute = true;
            //ここでイベ呼んでみる？
        }
        await UniTask.Delay(TimeSpan.FromSeconds(2f));
        _isExecute = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = _currentSprite;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!_isExecute)
        {
            collision.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<SpriteRenderer>().sprite = null;
        }
    }
}
