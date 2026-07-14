using UnityEngine;

[CreateAssetMenu(fileName = "Novel", menuName = "NNovel/Novel")]
public class NNovel : ScriptableObject
{
    [Header("Sprite")]
    [SerializeField] private Sprite _sprite;
    [Header("位置")]
    [SerializeField] private Vector2 _pos;

    public Sprite Sprite => _sprite;
    public Vector2 Pos => _pos;
}
