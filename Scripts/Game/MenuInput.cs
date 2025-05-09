using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using static PlayerControls;

public class MenuInput : MonoBehaviour, IMenuActions
{
    public static MenuInput Instance { get; private set; }

    //public static event Action SelectMenu;
    //public static event Action NavigateMenu;

    public float yInput;
    public float sensitivityReduced;

    Coroutine handleHoldCoroutine;

    MemoryTextSelection memoryTextSelection;
    BestiaryMenu bestiaryMenu;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        memoryTextSelection = FindAnyObjectByType<MemoryTextSelection>();
        bestiaryMenu = FindAnyObjectByType<BestiaryMenu>();
    }

    private void Start()
    {
        InputManager.controls.Menu.SetCallbacks(this);
    }

    public void OnPointMenu(InputAction.CallbackContext context)
    {

    }

    public void OnNavigateMenu(InputAction.CallbackContext context)
    {
        if (!MenuManager.Instance.IsMenuOpen(MenuManager.Menu.Memory))
        {
            //if (context.performed)
            //{
            //    NavigateMenu?.Invoke();
            //}

            //Debug.LogWarning("No menu currently open");
            return;
        }

        MenuManager.Menu currentMenu = MenuManager.Instance.GetCurrentMenu();

        if (currentMenu == MenuManager.Menu.Memory)
        {
            if (context.started)
            {
                handleHoldCoroutine = StartCoroutine(HandleHold(context));
            }
            else if (context.canceled)
            {
                StopCoroutine(handleHoldCoroutine);
                handleHoldCoroutine = null;
                memoryTextSelection.speed = 0;
                yInput = 0;
            }
        }
        //if (handleHoldCoroutine != null && context.canceled)
        //{
        //    StopCoroutine(handleHoldCoroutine);
        //    handleHoldCoroutine = null;
        //    memoryTextSelection.speed = 0;
        //    yInput = 0;
        //}


    }

    private IEnumerator HandleHold(InputAction.CallbackContext context)
    {
        //float fastThreshold = 0.5f;

        while (true)
        {
            if (!MenuManager.Instance.IsMenuOpen(MenuManager.Menu.Memory)) break;
           
            float inputY = context.ReadValue<Vector2>().y;
            yInput = inputY;

            float absoluteValue = Mathf.Abs(inputY);
            sensitivityReduced = Mathf.InverseLerp(0.5f, 1, absoluteValue);

            float lerpedSpeed = 
                Mathf.Lerp(memoryTextSelection.speedMin, memoryTextSelection.speedMax, sensitivityReduced);

            memoryTextSelection.speed = lerpedSpeed; 
                
            if (InputManager.controls.Menu.FastForwardMenu.IsPressed())
            {
                memoryTextSelection.speed = memoryTextSelection.speedFast;
            }

            if (inputY < 0)
            {
                memoryTextSelection.IncrementCharacter(Horizontal.Right);
            }
            else if (inputY > 0)
            {
                memoryTextSelection.IncrementCharacter(Horizontal.Left);
            }
            //Debug.Log(inputY);//
            yield return null; // Wait for the next frame
        }
    }

    public void OnFastForwardMenu(InputAction.CallbackContext context)
    {

    }

    public void OnSelectMenu(InputAction.CallbackContext context) 
    {
        //if (MenuManager.Instance.GetCurrentMenu() != MenuManager.Menu.Memory)
        //{
        //    if (context.performed)
        //    {
        //        SelectMenu?.Invoke();
        //    }
        //}
    }

    public void OnCloseMenu(InputAction.CallbackContext context)
    {
        //SetControlTarget(Actions.Gameplay);
        if (context.performed)
        {
            MenuManager.Instance.CloseMenu();
        }
    }

    public void OnBackMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            MenuManager.Instance.BackMenu();
        }
    }

    public void OnExitMenuMemory(InputAction.CallbackContext context)
    {
        if (!MenuManager.Instance.IsMenuOpen(MenuManager.Menu.Memory)) 
            return;

        if (context.performed)
        {
            MenuManager.Instance.CloseMenu();
        }
    }


    public void OnBestiaryMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (MenuManager.Instance.GetCurrentMenu() == MenuManager.Menu.Bestiary)
            {
                MenuManager.Instance.CloseMenu();
            }
            else
            {
                MenuManager.Instance.OpenMenu(MenuManager.Menu.Bestiary);
            }

        }
      
    }

    public void OnScrollMenu(InputAction.CallbackContext context)
    {
        if (MenuManager.Instance.IsMenuOpen(MenuManager.Menu.Bestiary))
        {
            if (context.performed)
            {
                float scrollValue = context.ReadValue<float>();

                if (scrollValue < 0)
                {
                    bestiaryMenu.PageIncrease();
                }
                else
                {
                    bestiaryMenu.PageDecrease();
                }
            }
        }
    }
}
