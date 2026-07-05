using UnityEngine;
using UnityEngine.UI;

public class CommentManager : MonoBehaviour
{
    [Header("セリフ&表情集")]
    [SerializeField] private Comment _comment;
    [Header("表情")]
    [SerializeField] private Image _image;
    [Header("コメント")]
    [SerializeField] private Text _commentText;

    private void OnEnable() => EventBus.Subscribe<SendScoreEvent>(this, ReceiveScore);
    private void OnDisable() => EventBus.AllUnSubscribe(this);

    private void ReceiveScore(SendScoreEvent s)
    {
        if (s._combo % 5 == 0 && s._combo != 0)
            SetSpriteComment(_comment.SpecialSprite, $"{s._combo}コンボ!?{_comment.FComboComment}");
        else if (s._line == 4)
            SetSpriteComment(_comment.SpecialSprite, $"{_comment.TComment}");
        else if (s._combo > 1)
            SetSpriteComment(_comment.Sprite[Random.Range(0, _comment.Sprite.Length)], $"{s._combo}コンボ!{_comment.ComboComment[Random.Range(0, _comment.ComboComment.Length)]}");
        else if (s._line > 0)
            SetSpriteComment(_comment.Sprite[Random.Range(0, _comment.Sprite.Length)], $"{s._line}列消し!{_comment.DeleteComment[Random.Range(0, _comment.DeleteComment.Length)]}");
    }

    private void SetSpriteComment(Sprite sprite, string text)
    {
        _image.sprite = sprite;
        _commentText.text = text;
    }
}
