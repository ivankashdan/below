using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public enum ControlScheme
{
    Keyboard,
    Xbox360
}

public class ControllerSwitchBehaviour : MonoBehaviour
{
    public static ControllerSwitchBehaviour Instance { get; private set; }

    public GameObject mainFirstSelectedGameObject;

    string controlSchemeNameKeyboard = "Keyboard";
    string controlScemeNameGamepad = "Xbox360";

    PlayerInput playerInput;

    Dictionary<ControlScheme, string> schemeStringDictionary;

     

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        playerInput = FindAnyObjectByType<PlayerInput>();
        Cursor.visible = false;

        schemeStringDictionary = new Dictionary<ControlScheme, string>()
        {
            { ControlScheme.Keyboard, controlSchemeNameKeyboard },
            { ControlScheme.Xbox360, controlScemeNameGamepad }
        };
    }

    void OnEnable()
    {
        playerInput.onControlsChanged += OnControlsChanged;
        MenuManager.MenuOpened += OnMenuOpened;
        MenuManager.MenuBaseDeactivated += OnMenuClosed;
    }

    void OnDisable()
    {
        playerInput.onControlsChanged -= OnControlsChanged;
        MenuManager.MenuOpened -= OnMenuOpened;
        MenuManager.MenuBaseDeactivated -= OnMenuClosed;
    }
    void Start()
    {
        OnControlsChanged(playerInput);
    }

    public bool IsSchemeActive(ControlScheme scheme)
    {
         schemeStringDictionary.TryGetValue(scheme, out string schemeString);

         return playerInput.currentControlScheme == schemeString;
    }

    void OnMenuOpened(MenuManager.Menu menu)
    {
        Cursor.visible = true;
        UpdateNavigationForController();
    }
    void OnMenuClosed()
    {
        Cursor.visible = false;
        UpdateNavigationForController();
    }

    void OnControlsChanged(PlayerInput input)
    {
        UpdateNavigationForController();
    }

    void UpdateNavigationForController()
    {
        if (MenuManager.IsAnyMenuOpen())
        {
            if (IsSchemeActive(ControlScheme.Keyboard)) // keyboard
            {
                Cursor.visible = true;

                EventSystem.current.sendNavigationEvents = false;

                EventSystem.current.SetSelectedGameObject(null);
            }
            else if (IsSchemeActive(ControlScheme.Xbox360)) //gamepad
            {
                Cursor.visible = false;

                EventSystem.current.sendNavigationEvents = true;

                MenuManager.Menu currentMenu = MenuManager.Instance.GetCurrentMenu();

                switch (currentMenu)
                {
                    case MenuManager.Menu.Main:
                        EventSystem.current.SetSelectedGameObject(mainFirstSelectedGameObject);
                        break;
                    case MenuManager.Menu.Controls:
                        break;
                    case MenuManager.Menu.Bestiary:
                        SelectVisibleEntry();
                        break;
                }

            }
        }
    }

    void SelectVisibleEntry()
    {
        string currentBestiaryEntryTitle = BestiaryMenu.Instance.title.text;
        if (currentBestiaryEntryTitle != null && currentBestiaryEntryTitle != "")
        {
            BestiaryScriptableObject reference = BestiaryManager.GetReferenceFromTitle(currentBestiaryEntryTitle);
            BestiarySelection.Instance.ShowAndSelectEntry(reference);
            Debug.Log(currentBestiaryEntryTitle + " selected");
        }
    }


}
