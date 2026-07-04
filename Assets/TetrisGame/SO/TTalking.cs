using System;
using UnityEngine;

[CreateAssetMenu(fileName = "TTalking", menuName = "MinoObjects/TTalking")]
public class TTalking : ScriptableObject
{
    [Header("トークリスト")]
    [SerializeField] private TTalkingSetting[] _settings;

    public TTalkingSetting[] TSettings => _settings;
}

[Serializable]
public struct TTalkingSetting
{
    [Header("どの会話？")]
    [SerializeField] private TTalkEnum _talkType;
    [Header("内容")]
    [SerializeField] private TTalkContents[] _con;

    public TTalkEnum TalkType => _talkType;
    public TTalkContents[] Contents => _con;
}

[Serializable]
public struct TTalkContents
{
    [Header("表情")]
    [SerializeField] private Sprite _sprite;
    [Header("セリフ")]
    [SerializeField] private string _comment;

    public Sprite Sprite => _sprite;
    public string Comment => _comment;
}

public enum TTalkEnum
{
    First,
    Second,
    Finish
}
