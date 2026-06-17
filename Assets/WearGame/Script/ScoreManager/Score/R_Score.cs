using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// お手本受け取り→その状態でゴミ箱イベ呼ばれたら加算
/// </summary>
public class R_Score : MonoBehaviour
{
    [Header("通常ポイント")]
    [SerializeField] private int _normalPoint;
    [Header("ゴミ箱ポイント")]
    [SerializeField] private int _dustPoint;

    private D_Score _dScore;
    private Dictionary<Parts, ClothItem> _modelDic = new Dictionary<Parts, ClothItem>();

    private void OnEnable()
    {
        EventBus.Subscribe<ModelChangeEvent>(this, ReceiveModel);
        EventBus.Subscribe<DustBoxEvent>(this, ReceiveDustBox);
        EventBus.Subscribe<CorrectEvent>(this, ReceiveCorrect);
        EventBus.Subscribe<WearGameFinishEvent>(this, ReceiveGameFinish);
    }

    private void OnDisable()
    {
        EventBus.AllUnSubscribe(this);
    }

    private void Start()
    {
        _dScore = new D_Score();
    }

    private void ReceiveModel(ModelChangeEvent m)
    {
        foreach (Parts item in Enum.GetValues(typeof(Parts)))
        {
            _modelDic[item] = m._modelSelectDic[item];
        }
    }

    private void ReceiveDustBox(DustBoxEvent d)
    {
        foreach (var i in _modelDic.Keys)
        {
            if (_modelDic[i].Deathflag)
                _dScore.DustScore(_dustPoint);
        }
    }

    private void ReceiveCorrect(CorrectEvent c)
    {
        var flag = false;

        foreach (var i in _modelDic.Keys)
        {
            if (_modelDic[i].Deathflag)
            {
                flag = true;
                break;
            }
        }

        if (!flag)
            _dScore.NormalAddScore(_normalPoint);
    }

    private void ReceiveGameFinish(WearGameFinishEvent w)
    {
        HaveScore.Subst(_dScore.Score);
    }
}

public static class HaveScore
{
    private static int _score;

    public static int Score => _score;

    public static void Subst(int score)
    {
        _score = score;
    }
}
