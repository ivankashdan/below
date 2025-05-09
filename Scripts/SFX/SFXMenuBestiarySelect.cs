using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class SFXMenuBestiarySelect : OneShotTrack
{
    public bool active = false;

    //MenuManager menuManager;

    //protected override void Awake()
    //{
    //    base.Awake();
    //    menuManager = FindAnyObjectByType<MenuManager>();
    //}

    //private void OnEnable()
    //{
    //    InputManager.controls.Menu.NavigateMenu.started += OnNavigateMenu;
    //    //BestiarySlot.SlotSelected += OnSlotSelected;
    //}

    BestiaryScriptableObject currentRef;
    //BestiarySlot previousSlot;

    private void OnEnable()
    {
        MenuManager.MenuOpened += OnMenuOpened;
        MenuManager.MenuClosed += OnMenuClosed;
        BestiarySlot.SlotSelected += OnSlotSelected;
    }

    //private void Start()
    //{
    //    //MenuInput.NavigateMenu += OnNavigateMenu;
    //    //InputManager.controls.Menu.NavigateMenu.performed += OnNavigateMenu;
    //    //InputManager.controls.Menu.PointMenu.performed += OnPointMenu;
       
    //}

    private void OnDisable()
    {
        //MenuInput.NavigateMenu -= OnNavigateMenu;
        //InputManager.controls.Menu.NavigateMenu.performed -= OnNavigateMenu;
        //InputManager.controls.Menu.PointMenu.performed -= OnPointMenu;
        MenuManager.MenuOpened -= OnMenuOpened;
        MenuManager.MenuClosed -= OnMenuClosed;
        BestiarySlot.SlotSelected -= OnSlotSelected;
    }

    void OnNavigateMenu(
        //InputAction.CallbackContext context
        )
    {
        PlayTrack();
    }

    //void OnPointMenu(InputAction.CallbackContext context)
    //{
    //    PlayTrack();
    //}

    void OnMenuOpened(MenuManager.Menu menu)
    {
        if (menu == MenuManager.Menu.Bestiary)
        {
            Debug.Log("Bestiary opened");
            //active = true;
            StopAllCoroutines();
            StartCoroutine(OpeningDelayCoroutine());
        }
    }

    void OnMenuClosed(MenuManager.Menu menu)
    {
        if (menu == MenuManager.Menu.Bestiary)
        {
            Debug.Log("Bestiary closed");

            //StopAllCoroutines();
            //StartCoroutine(ClosingDelayCoroutine());

            active = false;
        }
    }

    void OnSlotSelected(BestiarySlot slot)
    {
        //previousSlot = currentSlot;
       
        if (currentRef != slot.storedReference)
        {
            if (active)
            {
                PlayTrack();
            }


            currentRef = slot.storedReference;
        }
        
    }

    IEnumerator OpeningDelayCoroutine()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        active = true;
    }

    //IEnumerator ClosingDelayCoroutine()
    //{
    //    yield return new WaitForSeconds(0.1f);
    //    active = false;
    //}

}
