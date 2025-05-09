using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ControlSchemeUIManager : MonoBehaviour, IControllerSwitch
{
    [Serializable]
    public struct SpriteSet
    {
        public Sprite sense;
        public Sprite jump;
        public Sprite interact;
        public Sprite gesture;
        public Sprite sprint;
        public Sprite glide;
        public Sprite hide;
        public Sprite move;
        public Sprite look;

        public Sprite select;
        public Sprite exit;
        public Sprite escape;

        public Sprite read;

        public Sprite bestiary;

        public Sprite controlScheme;
    }
    public void UpdateOnControllerSwitch()
    {
        if (!IsUIEmpty())
        {
            controlSchemeImage.sprite = currentSprites.controlScheme;
        }
    }

    public bool IsUIEmpty() => controlSchemeImage.sprite == empty || controlSchemeImage.sprite == null;

    public static ControlSchemeUIManager Instance { get; private set; }



    //[Serializable]
    //public struct SpriteSetPrompt
    //{
    //    public Sprite sense;
    //    public Sprite hop;
    //    public Sprite gesture;
    //    public Sprite sprint;
    //    public Sprite glide;
    //    public Sprite hide;
    //    public Sprite interact;
    //    public Sprite read;
    //}


  

    //public Dictionary<Sprite, string> promptStringDictionary;
    

    public Sprite empty;
    //public SpriteSetPrompt keyboardPrompts;
    //public SpriteSetPrompt gamepadPrompts;
    //[HideInInspector] public SpriteSetPrompt currentPrompts;

    [Space(10)]
    public SpriteSet keyboardSprites;
    public SpriteSet gamepadSprites;
    [HideInInspector] public SpriteSet currentSprites;

    [Header("UI Objects")]
    public Image controlSchemeImage;

    PlayerInput playerInput;
    string currentControlScheme;


    Dictionary<string, SpriteSet> spriteSetDictionary;



    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        playerInput = FindAnyObjectByType<PlayerInput>();
        //tutorialMessages = FindAnyObjectByType<TutorialMessages>();
        //targetUI = FindAnyObjectByType<TargetUI>();
        //abilityTrigger = FindAnyObjectByType<AbilityTrigger>();

        spriteSetDictionary = new Dictionary<string, SpriteSet>()
        {
            {"Keyboard", keyboardSprites},
            {"Xbox360", gamepadSprites}
        };

        //promptStringDictionary = new Dictionary<Sprite, string>()
        //{
        //    { currentSprites.sense, "Sense" },
        //    { currentSprites.hop, "Hop" },
        //    { currentSprites.gesture, "Gesture" },
        //    { currentSprites.sprint, "Sprint" },
        //    { currentSprites.glide, "Glide" },
        //    { currentSprites.hide, "Hide" },
        //    { currentSprites.interact, "Interact" },
        //    { currentSprites.read, "Read" },
            
        //};



        UpdateSpriteSet(playerInput.currentControlScheme);

    }

    private void Start()
    {
        OnControlsChanged(playerInput);
    }

    //public string GetTextFromPrompt(Sprite promptSprite)
    //{
    //    if (promptStringDictionary.TryGetValue(promptSprite, out string promptString))
    //    {
    //        return promptString;
    //    }
    //    return null;
    //}

    void OnEnable()
    {
        playerInput.onControlsChanged += OnControlsChanged;
    }

    void OnDisable()
    {
        playerInput.onControlsChanged -= OnControlsChanged;
    }

    //public void UpdateSpriteSet()
    //{
    //    if (playerInput != null)
    //    {
    //        UpdateSpriteSet(playerInput.currentControlScheme);
    //    }
    //    else throw new System.Exception("playerInput not assigned yet");
    //}

    void OnControlsChanged(PlayerInput input)
    {
        if (input.currentControlScheme != currentControlScheme)
        {
            currentControlScheme = input.currentControlScheme;
            UpdateSpriteSet(currentControlScheme);
        }

        UpdateAllControlIcons();
    }
 
    void UpdateAllControlIcons()
    {
        var controlSchemeIcons = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IControllerSwitch>();
        foreach (var item in controlSchemeIcons)
        {
            item.UpdateOnControllerSwitch();
        }
    }

    void UpdateSpriteSet(string controlScheme)
    {
        if (spriteSetDictionary.TryGetValue(controlScheme, out SpriteSet spriteSet))
        {
            currentSprites = spriteSet;
            Debug.Log("UI sprite set assigned: " + controlScheme);
        }
    }

    





    //TutorialMessages tutorialMessages;
    //TargetUI targetUI;
    //AbilityTrigger abilityTrigger;

    //[Serializable]
    //public struct PromptSprite
    //{
    //    public TutorialMessages.Prompt prompt;
    //    public Sprite sprite;
    //}

    //void UpdateSpriteSet(string controlScheme)
    //{
    //    if (controlScheme == "Keyboard")
    //    {


    //        tutorialMessages.senseSprite = keyboardSenseSprite;
    //        tutorialMessages.hopSprite = keyboardHopSprite;
    //        tutorialMessages.gestureSprite = keyboardGestureSprite;
    //        tutorialMessages.sprintSprite = keyboardSprintSprite;
    //        tutorialMessages.glideSprite = keyboardGlideSprite;
    //        tutorialMessages.hideSprite = keyboardHideSprite;

    //        targetUI.interactSprite = keyboardInteractSprite;
    //        targetUI.memorySprite = keyboardExitReadSprite;

    //        abilityTrigger.spriteOn = keyboardAbilitySprite;

    //        controlSchemeImage.sprite = keyboardControlSchemeSprite;
    //    }
    //    else if (controlScheme == "Xbox360")
    //    {
    //        tutorialMessages.senseSprite = gamepadSenseSprite;
    //        tutorialMessages.hopSprite = gamepadHopSprite;
    //        tutorialMessages.gestureSprite = gamepadGestureSprite;
    //        tutorialMessages.sprintSprite = gamepadSprintSprite;
    //        tutorialMessages.glideSprite = gamepadGlideSprite;
    //        tutorialMessages.hideSprite = gamepadHideSprite;

    //        targetUI.interactSprite = gamepadInteractSprite;
    //        targetUI.memorySprite = gamepadExitReadSprite;

    //        abilityTrigger.spriteOn = gamepadAbilitySprite;

    //        controlSchemeImage.sprite = gamepadControlSchemeSprite;
    //    }
    //    Debug.Log("UI sprite set assigned: " + controlScheme);
    //}


    //[Header("Keyboard Sprites")]
    //public Sprite keyboardSenseSprite;
    //public Sprite keyboardHopSprite;
    //public Sprite keyboardGestureSprite;
    //public Sprite keyboardSprintSprite;
    //public Sprite keyboardGlideSprite;
    //public Sprite keyboardHideSprite;

    //public Sprite keyboardInteractSprite;
    //public Sprite keyboardExitReadSprite;
    //public Sprite keyboardAbilitySprite;
    //public Sprite keyboardControlSchemeSprite;

    //[Header("Gamepad Sprites")]
    //public Sprite gamepadSenseSprite;
    //public Sprite gamepadHopSprite;
    //public Sprite gamepadGestureSprite;
    //public Sprite gamepadSprintSprite;
    //public Sprite gamepadGlideSprite;
    //public Sprite gamepadHideSprite;

    //public Sprite gamepadInteractSprite;
    //public Sprite gamepadExitReadSprite;
    //public Sprite gamepadAbilitySprite;
    //public Sprite gamepadControlSchemeSprite;


}
