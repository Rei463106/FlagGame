using UnityEngine;

/// <summary>
/// 実際に魔法を使う
/// </summary>
public class RMagicUse : MonoBehaviour
{
    private IMagic _magicUse;//魔法使い側
    private IHuman _human;

    private void Start()
    {
        //生成担当のStaticClassを作っても良いかも？
        _magicUse = new MagicUse();
        _human = new MagicUse();
    }

    private void Reaction()
    {
        _magicUse.Magic();
        _human.Think();
    }

}
