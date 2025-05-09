using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    float delayedEnable = 0.2f;

    [HideInInspector] public static PlayerControls controls;


    State currentState;
    public enum State
    {
        None,
        Gameplay,
        Menu,
        Memory
    }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        controls = new PlayerControls();
    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();
        controls.Menu.Enable();
        //controls.Memory.Enable();
    }

    private void OnDisable()
    {
        controls.Gameplay.Disable();
        controls.Menu.Disable();
        //controls.Memory.Disable();
    }

    public void SetState(State value)
    {
        ClearState();
        EnableActionSet(value);
    }

    public State GetState()
    {
        return currentState;
    }

    public void SetStateDelayed(State value)
    {
        StopAllCoroutines();
        StartCoroutine(DelayedEnableCoroutine(value));
    }

    IEnumerator DelayedEnableCoroutine(State value)
    {
        ClearState();

        yield return new WaitForSeconds(delayedEnable);

        EnableActionSet(value);
    }
   
    void ClearState()
    {
        controls.Menu.Disable();
        controls.Gameplay.Disable();
        //controls.Memory.Disable();
    }

    void EnableActionSet(State target)
    {
        switch (target)
        {
            case State.Gameplay:
                controls.Gameplay.Enable();
                break;
            case State.Menu:
                controls.Menu.Enable();
                break;
            //case State.Memory:
            //    controls.Memory.Enable();
            //    break;
            case State.None:
                break;
        }
        currentState = target;
    }

 


    //private void Update()
    //{
    //    if (GameState.Instance.IsPlayerInControl()) //replace with event subscription
    //    {
    //        if (AreActionsSuspended())
    //        {
    //            SuspendActions(false);
    //        }
    //    }
    //    else
    //    {
    //        if (!AreActionsSuspended())
    //        {
    //            SuspendActions(true);
    //        }
    //    }
    //}

    //bool AreActionsSuspended()
    //{
    //    return !controls.Gameplay.enabled && !controls.Menu.enabled;
    //}



    //public void SuspendActions(bool value)
    //{
    //    if (value)
    //    {
    //        ClearActionSets();
    //    }
    //    else
    //    {
    //        SetActionSet(GetAppropriateState());
    //    }
    //}

    //Actions GetAppropriateState()
    //{
    //    return MenuManager.IsAnyMenuOpen() ? Actions.Menu : Actions.Gameplay ;
    //}
}
