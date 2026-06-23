using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 実際に魔法を使う
/// </summary>
public class RMagicUse : MonoBehaviour
{
    private IMagic _magicUse;//魔法使い側
    private IHuman _human;
    private IEnumerable<int> _in;

    private void Start()
    {
        //生成担当のStaticClassを作っても良いかも？
        _magicUse = new MagicUse();
        _human = new MagicUse();
        _in = new List<int>();//実質やってることはこれと同じ
        _in.GetEnumerator();
    }

    private void Reaction()
    {
        _magicUse.Magic();
        _human.Think();
    }

}
