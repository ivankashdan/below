using UnityEngine;
using UnityEngine.EventSystems;

public class MainControllerBehaviour : ControllerSwitchBehaviour
{

    //public GameObject firstSelectedGameObject;

    //protected override void OnEnable()
    //{
    //    base.OnEnable();
    //    MenuManager.MenuOpened += OnMenuOpened;
    //}

    //protected override void OnDisable()
    //{
    //    base.OnDisable();
    //    MenuManager.MenuOpened -= OnMenuOpened;
    //}

    //void OnMenuOpened(MenuManager.Menu menu)
    //{
    //    if (MenuManager.Instance.IsMenuOpen(MenuManager.Menu.Main))
    //    {
    //        UpdateNavigationForController();
    //    }
    //}
        
    //protected override void GamepadConnected()
    //{
    //    if (MenuManager.Instance.IsMenuOpen(MenuManager.Menu.Main))
    //    {
    //        EventSystem.current.sendNavigationEvents = true;
    //        EventSystem.current.SetSelectedGameObject(firstSelectedGameObject);    
    //    }

    //}

    //protected override void KeyboardConnected()
    //{
    //    if (MenuManager.Instance.IsMenuOpen(MenuManager.Menu.Main))
    //    {
    //        EventSystem.current.sendNavigationEvents = false;
    //        EventSystem.current.SetSelectedGameObject(null);
    //    }
    //}
   


}
