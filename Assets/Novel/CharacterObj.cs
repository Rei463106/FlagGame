using UnityEngine;

public class CharacterObj : MonoBehaviour
{
    [Header("GameObject")]
    [SerializeField] private GameObject _myObject;
    [Header("SpriteRenderer")]
    [SerializeField] private SpriteRenderer _sp;

    public void SetSprite(Sprite sp) => _sp.sprite = sp;
    public void SetPosition(Vector2 v) => transform.position = v;
    public void DestroyObj() => Destroy(_myObject);

    public void ChangeA(float a)
    {
        Color c = _sp.color;
        c.a = a;
        _sp.color = c;
    }
}
