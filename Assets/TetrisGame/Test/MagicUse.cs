
/// <summary>
/// 魔法を使った時の反応
/// </summary>
public class MagicUse : IMagic, IHuman
{
    string IMagicWord.MagicWord => "あめよふれ";

    public void Magic()
    {

    }

    public void Think()
    {

    }

    private void Charge() { }
}
