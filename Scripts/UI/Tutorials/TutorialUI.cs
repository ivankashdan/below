using System;
using UnityEngine;
using static TutorialMessages;

public class TutorialUI : MonoBehaviour, IControllerSwitch, IPromptUI
{
    public event Action TargetActive;
    public event Action TargetInactive;
    public Sprite GetSprite() => sprite;
    public string GetText() => text;
    public int GetPriority() => 0;

    public string hide = "Hide";
    public string sense = "Shell Sense";
    public string hop = "Jump";
    public string gesture = "Gesture";
    public string sprint = "Sprint";
    public string glide = "Glide";
    public string move = "Move";
    public string look = "Look";

    Sprite GetSprite(Prompt prompt)
    {
        ControlSchemeUIManager.SpriteSet currentSprites = controlSchemeUIManager.currentSprites;

        switch (prompt)
        {
            case Prompt.hide:
                return currentSprites.hide;
            case Prompt.sense:
                return currentSprites.sense;
            case Prompt.hop:
                return currentSprites.jump;
            case Prompt.gesture:
                return currentSprites.gesture;
            case Prompt.sprint:
                return currentSprites.sprint;
            case Prompt.glide:
                return currentSprites.glide;
            case Prompt.move:
                return currentSprites.move;
            case Prompt.look:
                return currentSprites.look;
            case Prompt.none:
                return null;

        }
        throw new SystemException("Sprite does not exist: " + prompt.ToString());
    }
    string GetText(Prompt prompt)
    {
        switch (prompt)
        {
            case Prompt.hide:
                return hide;
            case Prompt.sense:
                return sense;
            case Prompt.hop:
                return hop;
            case Prompt.gesture:
                return gesture;
            case Prompt.sprint:
                return sprint;
            case Prompt.glide:
                return glide;
            case Prompt.move:
                return move;
            case Prompt.look:
                return look;
            case Prompt.none:
                return null;
        }
        throw new SystemException("Text does not exist");
    }

    public void UpdateOnControllerSwitch()
    {
        if (IsActive() && lastTutorialActivated == Prompt.glide)
        {
            if (ControllerSwitchBehaviour.Instance.IsSchemeActive(ControlScheme.Keyboard))
            {
                prefixText.SetActive(false);
            }
            else if (ControllerSwitchBehaviour.Instance.IsSchemeActive(ControlScheme.Xbox360))
            {
                prefixText.SetActive(true);
            }
        }

        //if (!IsActive())
        //{
        sprite = GetSprite(lastTutorialActivated);
        //}
    }

    public GameObject prefixText;
    public bool IsActive() => active;
        //currentSprite == controlSchemeUIManager.empty || currentSprite == null;
    bool active;

    [HideInInspector] public Sprite sprite;
    [HideInInspector] public string text = "";

    Prompt lastTutorialActivated;

    TutorialMessages tutorialMessages;
    ControlSchemeUIManager controlSchemeUIManager;

    private void Awake()
    {
        tutorialMessages = FindAnyObjectByType<TutorialMessages>();
        controlSchemeUIManager = FindAnyObjectByType<ControlSchemeUIManager>();
    }

    private void OnEnable()
    {
        tutorialMessages.TutorialMessageVisible += OnTutorialVisible;
        //tutorialMessages.TutorialMessageCleared += OnTutorialCleared;
        tutorialMessages.NoTutorialsActive += OnNoTutorialsActive;
    }

    private void OnDisable()
    {
        tutorialMessages.TutorialMessageVisible -= OnTutorialVisible;
        //tutorialMessages.TutorialMessageCleared -= OnTutorialCleared;
        tutorialMessages.NoTutorialsActive -= OnNoTutorialsActive;
    }

    void OnTutorialVisible(Prompt prompt)
    {
        sprite = GetSprite(prompt);
        text = GetText(prompt);
        lastTutorialActivated = prompt;

        active = true;
        TargetActive?.Invoke();

        if (prompt == Prompt.glide && ControllerSwitchBehaviour.Instance.IsSchemeActive(ControlScheme.Xbox360))
        {
            prefixText.SetActive(true);
        }
    }

    void OnNoTutorialsActive()
    {
        active = false;
        TargetInactive?.Invoke();

        prefixText.SetActive(false);
        //ClearTutorial();
    }


    //void OnTutorialCleared(Prompt prompt)
    //{
    //    if (prompt == Prompt.glide && ControllerSwitchBehaviour.Instance.IsSchemeActive(ControlScheme.Xbox360))
    //    {
    //        prefixText.SetActive(false);
    //    }
       
    //    //active = false;
    //    //ClearTutorial();
    //}

   
    //void ClearTutorial()
    //{
    //    //currentSprite = controlSchemeUIManager.empty;
    //    active = false;
    //    //Debug.Log("Sprite cleared");
    //}

   
}
