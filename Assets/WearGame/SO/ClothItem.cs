using UnityEngine;

[CreateAssetMenu(fileName = "ClothItem", menuName = "Cloth/ClothItem")]
internal class ClothItem : ScriptableObject
{
    [Header("名前")]
    [SerializeField] private string _name;
    [Header("状態")]
    [SerializeField] private Parts _parts;
    [Header("イラスト")]
    [SerializeField] private Sprite _sprite;
    [Header("死亡フラグ")]
    [SerializeField] private bool _deathflag;
    [Header("死んだときの説明文")]
    [SerializeField] private string _expect;

    public string Name => _name;
    public Parts Parts => _parts;
    public Sprite Sprite => _sprite;
    public bool Deathflag => _deathflag;
    public string Expect => _expect;
}