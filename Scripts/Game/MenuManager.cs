using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static event Action<Menu> MenuOpened;
    public static event Action<Menu> MenuClosed;
    //public static event Action MenuBaseActivated;
    public static event Action MenuBaseDeactivated;
    public static MenuManager Instance { get; private set; }
    
    public GameObject main;
    public GameObject controls;
    public GameObject bestiary;
    public GameObject memory;

    bool menusOpen;

    public enum Menu
    {
        Main,
        Controls,
        Bestiary,
        Memory
    }

    static Dictionary<Menu, GameObject> menuImagePairs;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        menuImagePairs = new Dictionary<Menu, GameObject>()
        {
            {Menu.Main, main},
            {Menu.Controls, controls},
            {Menu.Bestiary, bestiary},
            {Menu.Memory, memory }
        };

    }

  
    public void OpenBestiary() => OpenMenu(Menu.Bestiary);
    public void OpenControls() => OpenMenu(Menu.Controls);


    public void OpenMenu(Menu menu)
    {
        if (menusOpen == false)
        {
            SetMenuBase(true);
        }
        else if (IsMenuOpen(menu))
        {
            Debug.Log("Menu open called, but menu already open: " + menu.ToString());
            return;
        }
        else if (menusOpen)
        {
            Menu currentMenu = GetCurrentMenu();
            ShowMenuPanel(GetCurrentMenu(), false);
            MenuClosed?.Invoke(currentMenu);
        }

        ShowMenuPanel(menu, true);
        MenuOpened?.Invoke(menu);
        Debug.Log("Menu opened: " + menu.ToString());
    }

    public void BackMenu()
    {
        switch (GetCurrentMenu())
        {
            case Menu.Controls:
                OpenMenu(Menu.Main);
                break;
            case Menu.Main:
            case Menu.Memory:
            case Menu.Bestiary:
                CloseMenu();
                break;
        }
    }

    public void CloseMenu()
    {
        if (menusOpen)
        {
            CloseMenu(GetCurrentMenu());
        }
    }

    void CloseMenu(Menu menu)
    {
        if (IsMenuOpen(menu))
        {
            ShowMenuPanel(menu, false);
            MenuClosed?.Invoke(menu);
            Debug.Log("Menu closed: " + menu.ToString());

            SetMenuBase(false);
        }
        else
        {
            Debug.Log("Menu close called, but menu not open: " + menu.ToString());
        }

    }

    void SetMenuBase(bool value)
    {
        MenuSettings(value);
        menusOpen = value;

        if (value == false)
        {
            CloseMenu();
            //HideAllMenuPanels();
            MenuBaseDeactivated?.Invoke();
            Debug.Log("Menu base closed");
        }
        else
        {
            //MenuBaseActivated?.Invoke();
            Debug.Log("Menu base opened");
        }
    }

    void ShowMenuPanel(Menu menu, bool value)
    {
        if (menuImagePairs.TryGetValue(menu, out GameObject panel))
        {
            panel.SetActive(value);
        }
    }

    //void HideAllMenuPanels()
    //{
    //    foreach (var panel in menuImagePairs.Values)
    //    {
    //        panel.SetActive(false);
    //    }
    //}

    void MenuSettings(bool menu)
    {
        if (menu)
        {
            InputManager.Instance.SetState(InputManager.State.Menu);
        }
        else
        {
            InputManager.Instance.SetStateDelayed(InputManager.State.Gameplay);
        }

        Time.timeScale = menu ? 0 : 1;
    }

  
    public Menu GetCurrentMenu()
    {
        foreach (var pair in menuImagePairs)
        {
            if (pair.Value.activeSelf)
            {
                return pair.Key;
            }
        }
        throw new Exception("No menu currently open");
    }

    public bool IsMenuOpen(Menu menu)
    {
        if (menuImagePairs.TryGetValue(menu, out GameObject panel))
        {
            return panel.activeSelf;
        }
        return false;
    }

    static public bool IsAnyMenuOpen()
    {
        foreach (var panel in menuImagePairs.Values)
        {
            if (panel.activeSelf)
            {
                return true;
            }
        }
        return false;
    }




}