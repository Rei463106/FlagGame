using UnityEngine;

public class CharacterObj : MonoBehaviour
{
    [Header("SpriteRenderer")]
    [SerializeField] private SpriteRenderer _sp;

    public void SetSprite(Sprite sp) => _sp.sprite = sp;
    public void SetPosition(Vector2 v) => transform.position = v;
}
