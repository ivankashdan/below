using System;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IListenCheck
{
    TutorialMessages.Prompt GetPrompt();
    InputAction GetAction();
    bool GetCheck();
    TutorialMessages.State GetState();
    TutorialMessages.State SetState(TutorialMessages.State state);
    
}


public class TutorialMessages : MonoBehaviour
{
    public event Action<Prompt> TutorialMessageActive;
    public event Action<Prompt> TutorialMessageCleared;
    public event Action<Prompt> TutorialMessageVisible;
    public event Action NoTutorialsActive;

    public enum State
    {
        inactive = 0,
        active = 1,
        cleared = 2
    }

    public enum Prompt
    {
        none = 0,
        hide = 1,
        gesture = 2,
        glide = 3,
        hop = 4,
        sprint = 5,
        sense = 6,
        move = 7,
        look = 8
    }

    

    //bool listenCheck;
    IListenCheck listenCheck;

    //Dictionary<Prompt, InputAction> promptInputActions;
    //Dictionary<Prompt, IListenCheck> promptChecks;

    IListenCheck[] tutorialPrompts;

    PromptUIManager promptUIManager;
    
    private void Awake()
    {
        tutorialPrompts = GetComponents<IListenCheck>();
        promptUIManager = FindAnyObjectByType<PromptUIManager>();
    }

    //void Start()
    //{
    //    //InitChecks();

    //    //InitInputActions();

    //    //SetPrompt(Prompt.sense, State.active);
    //}

    public Prompt activePrompt = Prompt.none;
    private void Update()
    {
        if (listenCheck != null)
        {
            activePrompt = listenCheck.GetPrompt();
        }
        else
        {
            activePrompt = Prompt.none;
        }


        ListenForInput();
    }
    void ListenForInput()
    {
        if (listenCheck != null)
        {
            if (listenCheck.GetCheck() == false) return;

            if (promptUIManager.IsTutorialUIVisible() == false) return;

            InputAction actionRequired = listenCheck.GetAction();

            if (actionRequired.activeValueType == typeof(Vector2))
            {
                Vector2 input = actionRequired.ReadValue<Vector2>();
                if (input != Vector2.zero)
                {
                    SetPrompt(listenCheck.GetPrompt(), State.cleared);
                }
            }
            else if (actionRequired.IsPressed())
            {
                SetPrompt(listenCheck.GetPrompt(), State.cleared);
            }
        }
    }

    //void ListenCleared()
    //{
    //    if (listenCheck != null)
    //    {
    //        Prompt listenPrompt = listenCheck.GetPrompt();

    //        SetPrompt(listenPrompt, State.cleared);

    //        if (IsThereActivePrompt() == false)
    //        {
    //            ClearListen();
    //        }
    //    }
    //}


    public bool IsListeningForPrompt(Prompt prompt)
    {
        if (listenCheck != null)
        {
            return listenCheck.GetPrompt() == prompt;  //listenForInput && listenPrompt == prompt;
        }
        return false;

    }

    public bool IsListening()
    {
        return listenCheck != null;  //listenForInput && listenPrompt == prompt;
    }


    void SetListen(Prompt prompt)
    {
        //listenForInput = true; // assign listening
        //listenPrompt = prompt;
        listenCheck = GetPrompt(prompt);
        //listenAction = GetPromptAction(prompt); // assign the action listening for

        TutorialMessageVisible?.Invoke(prompt);
        

        //if (promptChecks.TryGetValue(prompt, out var inputCheck))
        //{
        //    listenCheck = inputCheck;
        //}
        //else
        //{
        //    listenCheck = null;
        //}
    }

    void ClearListen()
    {
        //listenForInput = false;
        //listenAction = null;
        //listenPrompt = Prompt.none;//
        listenCheck = null;
    }


    public IListenCheck GetPrompt(Prompt prompt)
    {
        foreach (var entry in tutorialPrompts)
        {
            if (entry.GetPrompt() == prompt)
            {
                return entry;
            }
        }
        throw new SystemException("Requested prompt does not exist");
    }

    public State GetPromptState(Prompt prompt)
    {
        IListenCheck entry = GetPrompt(prompt);
        if (entry != null)
        {
            return entry.GetState();
        }
        throw new SystemException("Requested state does not exist");
    }

    public InputAction GetPromptAction(Prompt prompt)
    {
        IListenCheck entry = GetPrompt(prompt);
        if (entry != null)
        {
            return entry.GetAction();
        }
        throw new SystemException("Requested action does not exist");
    }

    public void SetPromptActiveIfInactive(Prompt prompt)
    {
        if (GetPromptState(prompt) == State.inactive)
        {
            SetPrompt(prompt, State.active);
        }
    }
    public void SetPromptInactiveIfActive(Prompt prompt)
    {
        if (GetPromptState(prompt) == State.active)
        {
            SetPrompt(prompt, State.inactive);
        }
    }
    public void SetPromptCleared(Prompt prompt)
    {
        SetPrompt(prompt, State.cleared);
    }

    public void SetPromptActive(Prompt prompt)
    {
        SetPrompt(prompt, State.active);
    }
  
    void SetPrompt(Prompt prompt, State state)
    {
        foreach (var entry in tutorialPrompts)
        {
            if (entry.GetPrompt() == prompt)
            {
                entry.SetState(state);
                break;
            }
        }

        switch (state)
        {
            case State.active:
                SetListen(prompt);
                TutorialMessageActive?.Invoke(prompt); // assign the UI 
                break;
            case State.cleared:
                if (IsThereActivePrompt())
                {
                    SetFirstActivePromptToListen();
                }
                else
                {
                    ClearListen();
                }
                TutorialMessageCleared?.Invoke(prompt);
                break;
            case State.inactive:
                if (IsThereActivePrompt())
                {
                    SetFirstActivePromptToListen();
                }
                else
                {
                    ClearListen();
                }
                break;
        }

        //Debug.Log(prompt.ToString() + "prompt : " + state.ToString());
        //Debug.Log(HowManyActivePrompts() + " prompts remaining");       
    }

    bool IsThereActivePrompt()
    {
        foreach (var entry in tutorialPrompts)
        {
            if (entry.GetState() == State.active)
            {
                return true;
            }
        }
        NoTutorialsActive?.Invoke(); //if no active prompts, tell UI to empty
        return false;

    }

    void SetFirstActivePromptToListen()
    {

        foreach (var entry in tutorialPrompts)
        {
            if (entry.GetState() == State.active)
            {
                SetListen(entry.GetPrompt());
                return;
            }
        }


    }


    //void InitInputActions()
    //{
    //    promptInputActions = new Dictionary<Prompt, InputAction>
    //    {
    //        { Prompt.hide, InputManager.controls.Gameplay.Hide},
    //        { Prompt.sense, InputManager.controls.Gameplay.ShellSense},
    //        { Prompt.hop,  InputManager.controls.Gameplay.Hop},
    //        { Prompt.gesture, InputManager.controls.Gameplay.Gesture},
    //        { Prompt.sprint, InputManager.controls.Gameplay.Sprint},
    //        { Prompt.glide, InputManager.controls.Gameplay.Glide},
    //        { Prompt.move, InputManager.controls.Gameplay.Move },
    //        {Prompt.look, InputManager.controls.Gameplay.Look }
    //    };
    //}

    //void InitChecks()
    //{
    //    promptChecks = new Dictionary<Prompt, IListenCheck>
    //    {
    //        { Prompt.hop, tutorialHop},
    //        { Prompt.sprint, tutorialSprint},
    //        { Prompt.sense, tutorialSense },

    //        //{ Prompt.gesture,  },
    //        //{ Prompt.glide,  },
    //        //{ Prompt.hide,  },
    //        //{ Prompt.move,  },
    //        //{ Prompt.look,  }
    //        //{ Prompt.empty,  },
    //    };
    //}


    //bool IsThereActivePrompt()
    //{
    //    foreach (var prompt in promptStates)
    //    {
    //        if (prompt.Value == State.active)
    //        {
    //            return true;
    //        }

    //    }
    //    return false;
    //}


    //int HowManyActivePrompts()
    //{
    //    int count = 0;

    //    foreach (var prompt in promptStates)
    //    {
    //        if (prompt.Value == State.active)
    //        {
    //            count++;
    //        }

    //    }
    //    return count;
    //}




    //public bool IsPromptActive(Prompt prompt)
    //{
    //    return promptStates.TryGetValue(prompt, out State state) && state == State.active;
    //}

    //public bool IsPromptCleared(Prompt prompt)
    //{
    //    return promptStates.TryGetValue(prompt, out State state) && state == State.cleared;
    //}


    //void TutorialMessage(Prompt prompt)
    //{
    //    TutorialMessageSent?.Invoke(prompt);

    //    //Sprite sprite = GetSprite(prompt);
    //    //currentSprite = sprite;
    //    //targetUI.UpdateTutorialImage(sprite);

    //    if (promptInputActions.TryGetValue(prompt, out var inputAction)) //needs to continuakky check for this
    //    {
    //        if (inputAction.IsPressed())
    //        {
    //            //currentSprite = controlSchemeUIManager.empty;
    //            //targetUI.ClearTutorialImage();
    //            SetPrompt(prompt, State.cleared);
    //        }
    //    }
    //}

    //private void OnEnable()
    //{
    //    NewCameraBehaviour.ZoomedOut += SprintPrompt;
    //}

    //private void OnDisable()
    //{
    //    NewCameraBehaviour.ZoomedOut -= SprintPrompt;
    //}

    //void Update()
    //{
    //    if (tutorialMessages)
    //    {
    //        if (!gesture) GestureConCheck();

    //        if (IsPromptCleared())

    //        if (!hopEnd && hopEndCon)
    //        {
    //            TutorialMessage(hopSprite, controller.Gameplay.Hop, ref hopEnd, ref hopEndCon);
    //        }
    //        else if (!glideSill && glideSillCon)
    //        {
    //            TutorialMessage(glideSprite, controller.Gameplay.Glide, ref glideSill, ref glideSillCon);
    //        }
    //        else if (!hop && hopCon)
    //        {
    //            TutorialMessage(hopSprite, controller.Gameplay.Hop, ref hop, ref hopCon);
    //        }
    //        else if (!gesture && gestureCon)
    //        {
    //            TutorialMessage(gestureSprite, controller.Gameplay.Gesture, ref gesture, ref gestureCon);
    //        }
    //        else if (!gestureBox && gestureBoxCon)
    //        {
    //            TutorialMessage(gestureSprite, controller.Gameplay.Gesture, ref gestureBox, ref gestureBoxCon);
    //        }
    //        else if (!sense && senseCon)
    //        {
    //            TutorialMessage(senseSprite, controller.Gameplay.ShellSense, ref sense, ref senseCon);
    //        }
    //        else if (!sprint && sprintCon)
    //        {
    //            TutorialMessage(sprintSprite, controller.Gameplay.Sprint, ref sprint, ref sprintCon);
    //        }
    //        else if (!hide && hideCon)
    //        {
    //            TutorialMessage(hideSprite, controller.Gameplay.Hide, ref hide, ref hideCon);
    //        }

    //        else if (!glide && glideCon)
    //        {
    //            TutorialMessage(glideSprite, controller.Gameplay.Glide, ref glide, ref glideCon);
    //        }

    //        else targetUI.ClearTutorialImage();
    //    }

    //}

    //void TutorialMessage(Sprite sprite, InputAction inputAction, ref bool tutorial, ref bool condition)
    //{
    //    targetUI.UpdateTutorialImage(sprite);

    //    if (inputAction.IsPressed()) //&& targetUI.IsTutorialImageShowing())
    //    {
    //        targetUI.ClearTutorialImage();
    //        tutorial = true;
    //        condition = true;
    //    }
    //}




    //bool
    //    hide, hideCon,
    //    //look, lookCon,
    //    //move, moveCon,
    //    sense, senseCon,
    //    hop, hopCon,
    //    gesture, gestureCon,
    //    gestureBox, gestureBoxCon,
    //    sprint, sprintCon,
    //    glide, glideCon,
    //    glideSill, glideSillCon,
    //    hopEnd, hopEndCon
    //    //drop, dropCon,
    //    //current
    //    ;

    //public void SenseTutorial() => senseCon = true;
    //public void GesturePrompt() => gestureCon = true;
    //public void SensePrompt(bool sense) => senseCon = sense;
    //public void GestureBoxPrompt(bool gesture) => gestureBoxCon = gesture;
    //public void HopPrompt(bool hop) => hopCon = hop;
    //public void GlidePrompt(bool glide) => glideCon = glide;
    //public void SprintPrompt() => sprintCon = true;
    //public void SprintPrompt(bool sprint) => sprintCon = sprint;
    //public void HidePrompt(bool hide) => hideCon = hide;
    //public void HopEndPrompt() => hopEndCon = true;
    //public void GlideSillPrompt() => glideSillCon = true;
    //public void GlideSillPrompt(bool glide) => glideSillCon = glide;


    //void GestureConCheck()
    //{
    //    if (IsUrchinNearAndVisible())
    //    {
    //        gestureCon = true;
    //    }
    //    else 
    //    {

    //        gestureCon = false;
    //    }
    //}

    //bool IsUrchinNearAndVisible()
    //{
    //    if (livestockBehaviours != null)
    //    {
    //        foreach (var livestock in livestockBehaviours)
    //        {
    //            if (livestock.IsPlayerCloseToUrchin() && livestock.CanPlayerSeeUrchin()) return true;
    //        }
    //    }
    //    //Debug.Log("player is not close to any urchins");

    //    return false;
    //}
}
