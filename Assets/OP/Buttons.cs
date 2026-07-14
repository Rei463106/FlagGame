using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    [Header("Image")]
    [SerializeField] private Image _buttonImage;
    [Header("RectTransform")]
    [SerializeField] private RectTransform _rectTransform;
    [Header("Text")]
    [SerializeField] private Text _text;
    [Header("Image初期値")]
    [SerializeField] private Vector2 _fSize;
    [Header("Text初期値")]
    [SerializeField] private int _tSize;

    public Vector2 FirstISize => _fSize;
    public int FirstTSize => _tSize;

    public void ChangeText(string text) => _text.text = text;
    public void ChangeSize(float width, float height, int fontsize)
    {
        _rectTransform.sizeDelta = new Vector2(width, height);
        _text.fontSize = fontsize;
    }
}
