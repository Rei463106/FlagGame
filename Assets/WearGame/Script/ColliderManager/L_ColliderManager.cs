using UnityEngine;

public class L_ColliderManager : MonoBehaviour
{
    [Header("R_ColliderManager")]
    [SerializeField] private R_ColliderManager _manager;

    private ClothItem _currentItem;

    private void OnEnable()
    {
        EventBus.Subscribe<DragGiveSetting>(this, SubscribeSetting);
        EventBus.Subscribe<ClickInsideFinish>(this, InsideSubscribe);
        EventBus.Subscribe<ClickOutsideFinish>(this, OutSideSubscribe);
    }

    private void OnDisable()
    {
        EventBus.AllUnSubscribe(this);
    }

    private void SubscribeSetting(DragGiveSetting s)
    {
        _currentItem = s.Setting;
    }

    private void InsideSubscribe(ClickInsideFinish c)
    {
        _manager.ColliderDic[_currentItem.Parts].gameObject.GetComponent<SpriteRenderer>().sprite = _currentItem.Sprite;
    }

    private void OutSideSubscribe(ClickOutsideFinish c)
    {
        Debug.Log("こっち");
        _manager.ColliderDic[_currentItem.Parts].gameObject.GetComponent<SpriteRenderer>().sprite = null;
    }
}
