using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [Header("PlayerAction")]
    [SerializeField] private static PlayerInput _pi;

    public static void EntryInput(string i, Action<InputAction.CallbackContext> action) => _pi.actions[(i)].started += action;
    public static void OutInput(string i, Action<InputAction.CallbackContext> action) => _pi.actions[(i)].started -= action;
}
