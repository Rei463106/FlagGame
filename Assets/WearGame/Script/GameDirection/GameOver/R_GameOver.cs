using UnityEngine;

/// <summary>
/// ランタイム中のゲームオーバーの処理
/// </summary>
public class R_GameOver : MonoBehaviour
{
    private ClothItem _item;

    private void OnEnable()
    {
        EventBus.Subscribe<DragGiveSettingEvent>(this, ReceiveDragSetting);
        EventBus.Subscribe<ObjectInsideEvent>(this, ReceiveInside);
    }
    private void OnDisable()
    {
        EventBus.AllUnSubscribe(this);
    }

    private void ReceiveDragSetting(DragGiveSettingEvent d)
    {
        _item = d.Setting;
    }

    private void ReceiveInside(ObjectInsideEvent i)
    {
        if (_item.Deathflag)
        {
            EventBus.Publish<GameOverEvent>(new GameOverEvent(_item.Expect));
            Debug.Log("GameOver");
        }
    }
}
