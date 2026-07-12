using UnityEngine;
using UnityEngine.UI;

public abstract class StageUIBase : MonoBehaviour
{
    [Header("背景")]
    [SerializeField] private Image _backImage;
    [Header("ステージ番号")]
    [SerializeField] private Text _stageNumber;
    [Header("タイトル")]
    [SerializeField] private Text _title;
    [Header("決定ボタン")]
    [SerializeField] private Image _dicImage;

    protected void ChangeText(string num, string title)
    {
        _stageNumber.text = num;
        _title.text = title;
    }

    protected void ChangeAppear(bool appear)
    {
        _backImage.enabled = appear;
        _stageNumber.enabled = appear;
        _title.enabled = appear;
        _dicImage.enabled = appear;
    }

    public abstract void Score(bool appear);
}
