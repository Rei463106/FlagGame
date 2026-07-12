using UnityEngine;

[CreateAssetMenu(fileName = "ClearFlag", menuName = "Stage/ClearFlag")]
public class ClearFlag : ScriptableObject, IResultFlag, IFlag
{
    [Header("現在のフラグ")]
    [SerializeField] ClearFlags _clearFlag = ClearFlags.None;

    public ClearFlags Flag => _clearFlag;

    public void ChangeFlag(ClearFlags c) => _clearFlag = c;
}

public interface IResultFlag
{
    public ClearFlags Flag { get; }
}

public interface IFlag
{
    public void ChangeFlag(ClearFlags c);
}

public enum ClearFlags
{
    None,
    Clear,
    Second
}
