using UnityEngine;
using UnityEngine.InputSystem;
using static PlayerControls;

public class GameplayInput : MonoBehaviour, IGameplayActions
{
    public static GameplayInput Instance { get; private set; }

    AnimationBehaviour animationBehaviour;
    InteractionBehaviour interactionBehaviour;
    PlayerState playerState;
    ShellCheck shellCheck;
    GroundCheck groundCheck;
    ShellSense shellSense;
    MemoryMenu memoryMenu;
    HUDManager hudManager;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        animationBehaviour = FindAnyObjectByType<AnimationBehaviour>();
        interactionBehaviour = FindAnyObjectByType<InteractionBehaviour>();
        playerState = FindAnyObjectByType<PlayerState>();
        shellCheck = FindAnyObjectByType<ShellCheck>();
        groundCheck = FindAnyObjectByType<GroundCheck>();
        shellSense = FindAnyObjectByType<ShellSense>();
        memoryMenu = FindAnyObjectByType<MemoryMenu>();
        hudManager = FindAnyObjectByType<HUDManager>();

    }

    private void Start()
    {
        InputManager.controls.Gameplay.SetCallbacks(this);

        InputManager.Instance.SetState(InputManager.State.Gameplay);
    }
    private void Update()
    {
        OnGlide();
    }

    void OnGlide()
    {
        if (playerState.IsInState(PlayerState.State.falling) && shellCheck.DoesShellHaveGlideAbility())
        {
            //if (!groundCheck.IsWithinDistanceToGround(groundCheck.glideDistance))
            //{
                if (InputManager.controls.Gameplay.Glide.IsPressed())
                {
                    playerState.SetState(PlayerState.State.gliding);
                }
            //}
        }
        else if (playerState.IsInState(PlayerState.State.gliding))
        {
            if (InputManager.controls.Gameplay.Glide.WasReleasedThisFrame())
            {
                playerState.SetState(PlayerState.State.falling);
            }
        }
    }

    public void OnGlide(InputAction.CallbackContext context) { }
    public void OnLook(InputAction.CallbackContext context) 
    {
        //if (context.performed)
        //{
        //    if (shellSense.IsShellSenseActive())
        //    {
        //        float inputX = context.ReadValue<Vector2>().x;

        //        if (inputX > 0)
        //        {

        //        }

        //        shellSense.  context.ReadValue<Vector2>();
        //    }
        //}
    }
    public void OnSprint(InputAction.CallbackContext context) { }
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            HandleInput.Instance.SetRawInput(context.ReadValue<Vector2>());
        }
        else if (context.canceled)
        {
            HandleInput.Instance.SetRawInput(Vector2.zero);
        }
    }


    public void OnHop(InputAction.CallbackContext context)
    {
        if (playerState.IsInState(PlayerState.State.ground))
        {
            if (!animationBehaviour.IsAnimationPlaying(AnimationBehaviour.Animation.Jump))
            {
                if (context.performed)
                {
                    playerState.SetState(PlayerState.State.jumping);
                }
            }
        }
    }




    public void OnGesture(InputAction.CallbackContext context)
    {
        if (playerState.IsInState(PlayerState.State.ground))
        {
            if (!animationBehaviour.IsAnimationPlaying(AnimationBehaviour.Animation.Gesture))
            {
                if (context.performed)
                {
                    playerState.SetState(PlayerState.State.gesturing);
                }
            }
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (memoryMenu.IsMemoryOpen())
            {
                
                memoryMenu.SetState(MemoryMenu.State.none);
                Debug.Log("Memory: exit pressed");
                
            }
            else if (hudManager.IsVisible)
            {
                interactionBehaviour.InteractAction();
            }
        }

      
    }

    public void OnShellSense(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (playerState.IsInState(PlayerState.State.ground))
            {
                playerState.SetState(PlayerState.State.sensing);
            }
            else if (playerState.IsInState(PlayerState.State.sensing))
            {
                playerState.SetState(PlayerState.State.ground);
            }
        }
    }

    public void OnHide(InputAction.CallbackContext context)
    {
        if (playerState.IsInState(PlayerState.State.ground) && shellCheck.IsAShellEquipped())
        {
            if (!animationBehaviour.IsAnimationPlaying(AnimationBehaviour.Animation.Hide))
            {
                if (context.performed)
                {
                    playerState.SetState(PlayerState.State.hiding);
                }
            }
        }
    }


    public void OnMenu(InputAction.CallbackContext context) //Esc //Start
    {
        if (context.performed)
        {
            if (ControllerSwitchBehaviour.Instance.IsSchemeActive(ControlScheme.Keyboard))
            {
                MenuManager.Instance.OpenMenu(MenuManager.Menu.Main);
            }
            else if (ControllerSwitchBehaviour.Instance.IsSchemeActive(ControlScheme.Xbox360))
            {
                if (!BestiaryUINotification.Instance.IsVisible)
                {
                    MenuManager.Instance.OpenMenu(MenuManager.Menu.Main);
                }
            }
        }
    }

    public void OnBestiary(InputAction.CallbackContext context) //Tab //Start
    {
        if (context.performed)
        {
            if (ControllerSwitchBehaviour.Instance.IsSchemeActive(ControlScheme.Keyboard))
            {
                MenuManager.Instance.OpenMenu(MenuManager.Menu.Bestiary);
                if (BestiaryUINotification.Instance.IsVisible)
                {
                    BestiaryUINotification.Instance.ClearNotification();
                }
            }
            else if (ControllerSwitchBehaviour.Instance.IsSchemeActive(ControlScheme.Xbox360))
            {
                if (BestiaryUINotification.Instance.IsVisible)
                {
                    MenuManager.Instance.OpenMenu(MenuManager.Menu.Bestiary);
                    BestiaryUINotification.Instance.ClearNotification();
                }
            }
        }
    }

    //public void OnExitMemory(InputAction.CallbackContext context)
    //{
    //    if (memoryMenu.IsMemoryOpen())
    //    {
    //        if (context.performed)
    //        {
    //            memoryMenu.SetState(MemoryMenu.State.none);
    //            Debug.Log("Memory: exit pressed");
    //        }
    //    }
    //}

    public void OnRead(InputAction.CallbackContext context)
    {
        if (memoryMenu.IsMemoryOpen())
        {
            if (context.performed)
            {
                MemoryMenu.State state = memoryMenu.GetState();

                switch (state)
                {
                    case MemoryMenu.State.none:
                        break;
                    case MemoryMenu.State.viewing:
                        memoryMenu.SetState(MemoryMenu.State.reading);
                        break;
                    case MemoryMenu.State.reading:
                        memoryMenu.SetState(MemoryMenu.State.viewing);
                        break;
                }

                Debug.Log("Memory: read pressed");
            }
        }
    }


}
