using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Cloth本体の処理を記述するところ(ランタイム)
/// </summary>
public class R_ObjectSetting : MonoBehaviour
{
    private D_ObjectSetting _setting;
    private void Start()
    {
        _setting = new D_ObjectSetting();     
    }

    public void DragMouse()
    {
        Debug.Log("ドラッグ");
        if (!_setting.IsDrag)
        {
            _setting.DragMouse();
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = true;
        }
        var mouse = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 mousePos = new Vector2(mouse.x, mouse.y);
        transform.position = mousePos;
    }

    public void RevertMouse()
    {
        // _setting.RevertMouse();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _setting.InCollision();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _setting.OutCollision();
    }
}
