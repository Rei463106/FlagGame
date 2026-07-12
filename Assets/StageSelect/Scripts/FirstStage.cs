using System.Collections.Generic;
using UnityEngine;

public class FirstStage : MonoBehaviour
{
    [Header("今のライン")]
    [SerializeField] private List<MoveDirectionSetting> _moveDirection = new();

    private Dictionary<MoveDirecrion, LineRenderer> _lDic = new();

    private void Awake()
    {
        foreach (var move in _moveDirection)
            _lDic.TryAdd(move.MType, move.Line);
    }

    private void OnTriggerStay2D(Collider2D collision) => EventBus.Publish<SendMoveDirection>(new SendMoveDirection(_lDic));
}
