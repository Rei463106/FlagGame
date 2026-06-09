using UnityEngine;

public class R_DragOrigin : MonoBehaviour
{
    [Header("SO")]
    [SerializeField] private ClothSetting _setting;

    private C_DragOrigin _cOrigin = new C_DragOrigin();
   

    public void PushMouse()
    {
        //一回しか処理できないようにする
        if (!_cOrigin.IsDragging)
        {
            _cOrigin.PushMouse();
            
            //Instantiate処理
            //Cloth渡す処理
        }
    }

    public void RevertMouse()
    {
        _cOrigin.RevertMouse();
    }
}
