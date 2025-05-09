using UnityEngine;
using UnityEngine.InputSystem;

public class CursorBehaviour : ControllerSwitchBehaviour
{

    //protected override void Awake()
    //{
    //    base.Awake();
    //    Cursor.visible = false;
    //}

    //protected override void OnEnable()
    //{
    //    base.OnEnable();
    //    MenuManager.MenuOpened += OnMenuOpened;
    //    MenuManager.MenusClosed += OnMenuClosed;
    //}

    //protected override void OnDisable()
    //{
    //    base.OnDisable();
    //    MenuManager.MenuOpened -= OnMenuOpened;
    //    MenuManager.MenusClosed -= OnMenuClosed;
    //}

    //void OnMenuOpened(MenuManager.Menu menu)
    //{
    //    //Debug.Log("Menu opened");
    //    Cursor.visible = true;
    //}
    //void OnMenuClosed()
    //{
    //    //Debug.Log("Menu closed");
    //    Cursor.visible = false;
    //}


    //protected override void KeyboardConnected()
    //{
    //    Debug.Log("keyboard connected");
    //    if (MenuManager.IsAnyMenuOpen())
    //    {
    //        Cursor.visible = true;
    //    }
    //}

    //protected override void GamepadConnected()
    //{
    //    Debug.Log("gamepad connected");
    //    if (MenuManager.IsAnyMenuOpen())
    //    {
    //        Cursor.visible = false;
    //    }
    //}


}
