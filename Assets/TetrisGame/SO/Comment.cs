using UnityEngine;

[CreateAssetMenu(fileName = "Comment", menuName = "MinoObjects/Comment")]
public class Comment : ScriptableObject
{
    [Header("表情Sprite")]
    [SerializeField] private Sprite[] _sprite;
    [Header("スペシャル")]
    [SerializeField] private Sprite _specialSprite;
    [Header("コンボ時")]
    [SerializeField] private string[] _comboComment;
    [Header("列を消した時")]
    [SerializeField] private string[] _deleteComment;
    [Header("5コンボ目")]
    [SerializeField] private string _fcomboComment;
    [Header("テトリス時")]
    [SerializeField] private string _tComment;

    public string[] ComboComment => _comboComment;
    public string[] DeleteComment => _deleteComment;
    public Sprite[] Sprite => _sprite;
    public string FComboComment => _fcomboComment;
    public string TComment => _tComment;
    public Sprite SpecialSprite => _specialSprite;
}
