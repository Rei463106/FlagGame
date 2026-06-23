using UnityEngine;

public class MinoSelect : MonoBehaviour, IReset
{
    [Header("SO")]
    [SerializeField] private Mino _mino;

    private MinoSetting _mSet;
    protected GameObject Obj { get; private set; }

    private void Awake()
    {
        ResetMino();
    }

    public void ResetMino()//リセット時に呼ぶ
    {
        _mSet = _mino.MSetting[Random.Range(0, _mino.MSetting.Length - 1)];
        EventBus.Publish<MinoSpawnEvent>(new MinoSpawnEvent(_mSet));
        Obj = _mSet.MinoPrefab;//ランダムに選ぶ
    }
}
