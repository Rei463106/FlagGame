using UnityEngine;

/// <summary>
/// Cloth本体の処理を記述するところ(見た目)
/// </summary>
public class L_ObjectSetting : MonoBehaviour
{
    private void OnEnable()
    {
        EventBus.Subscribe<ClickStart>(this, ChangeLook);
    }

    private void OnDisable()
    {
        EventBus.AllUnSubscribe(this);
    }

    private void ChangeLook(ClickStart c)
    {
       // GetComponent<SpriteRenderer>().sprite = c.Setting.Sprite;
    }
}
