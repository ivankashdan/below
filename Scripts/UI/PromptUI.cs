using UnityEngine;

abstract public class PromptUI : MonoBehaviour, IControllerSwitch
{
    public MenuManager.Menu menu;

    protected ControlSchemeUIManager controlSchemeUIManager;
    protected PromptUIManager promptUIManager;
    //MenuManager menuManager;
    //PlayerInput playerInput;
    protected virtual void Awake()
    {
        //playerInput = FindAnyObjectByType<PlayerInput>();
        controlSchemeUIManager = FindAnyObjectByType<ControlSchemeUIManager>();
        promptUIManager = FindAnyObjectByType<PromptUIManager>();
        //menuManager = FindAnyObjectByType<MenuManager>();

        //playerInput = FindAnyObjectByType<PlayerInput>();


    }

    private void OnEnable()
    {
        MenuManager.MenuOpened += OnMenuOpened;

    }
    private void OnDisable()
    {
        MenuManager.MenuOpened -= OnMenuOpened;
    }

    //private void Start()
    //{
    //    UpdateOnControllerSwitch();
    //}

    void OnMenuOpened(MenuManager.Menu menu)
    {
        if (menu == this.menu)
        {
            UpdateOnControllerSwitch();
        }

        //if (menu == MenuManager.Menu.Main)
        //{

        //}
    }

    public virtual void UpdateOnControllerSwitch() { }

 


}
