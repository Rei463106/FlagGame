using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private static PlayerInput _pi;

    private void Start() => _pi = GetComponent<PlayerInput>();

    public static void EntryInput(string i, Action<InputAction.CallbackContext> action) => _pi.actions[(i)].started += action;
    public static void OutInput(string i, Action<InputAction.CallbackContext> action) => _pi.actions[(i)].started -= action;
}
