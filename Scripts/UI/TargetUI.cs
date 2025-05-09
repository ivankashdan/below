using System;
using UnityEngine;

public class TargetUI : MonoBehaviour, IControllerSwitch, IPromptUI
{
    public event Action TargetActive;
    public event Action TargetInactive;

    public Sprite GetSprite() => sprite;
    public string GetText() => text;
    public int GetPriority() => 1;

    public void UpdateOnControllerSwitch()
    {
        //if (!IsActive())
        //{
            sprite = controlSchemeUIManager.currentSprites.interact;
        //}
    }


    public string text = "Interact";

    public bool IsActive() => active;
    bool active;
    //currentSprite == controlSchemeUIManager.empty || currentSprite == null;

    //public Image uiImage;

    [HideInInspector] public Sprite sprite;

    //public bool IsTutorialImageShowing() => uiImage.sprite == tutorialImage;
    //public void UpdateTutorialImage(Sprite sprite) => tutorialImage = sprite;
    //public void ClearTutorialImage() => tutorialImage = null;

    //Sprite tutorialImage;
    
    //ShellSense shellSense;
    InteractionBehaviour interactionBehaviour;
    //MemoryUI memoryUI;
    ControlSchemeUIManager controlSchemeUIManager;

    private void Awake()
    {
        //shellSense = FindAnyObjectByType<ShellSense>();
        interactionBehaviour = FindAnyObjectByType<InteractionBehaviour>();
        //memoryUI = FindAnyObjectByType<MemoryUI>();
        controlSchemeUIManager = FindAnyObjectByType<ControlSchemeUIManager>();

    }

    private void Start()
    {
        UpdateOnControllerSwitch();
    }

    private void OnEnable()
    {
        interactionBehaviour.InteractableSelected += OnInteractableSelected;

    }

    private void OnDisable()
    {
        interactionBehaviour.InteractableSelected -= OnInteractableSelected;
    }

    void OnInteractableSelected() //need to update the content of this
    {
        if (interactionBehaviour.HasSelected())
        {
            //if (interactionBehaviour.IsSelectedUrchin())
            //{

            //    currentSprite = controlSchemeUIManager.currentSprites.gesture;
            //}
            //else
            //{
                sprite = controlSchemeUIManager.currentSprites.interact;
                active = true;

                TargetActive?.Invoke();
            //Debug.Log("interactable sprite added");
            //}
        } 
        else
        {
            active = false;

            TargetInactive?.Invoke();
            //currentSprite = controlSchemeUIManager.empty;
        }

        //if (GameState.Instance.IsCutscenePlaying()
        // //|| GameState.Instance.IsCardDisplayed() 
        // || shellSense.IsShellSenseActive())
        //{
        //    uiImage.sprite = controlSchemeUIManager.empty;
        //}
        //else if (memoryUI.IsMemoryOpen())
        //{
        //    uiImage.sprite = controlSchemeUIManager.currentSprites.exitRead;
        //}
        //else if (interactionBehaviour.HasTarget())
        //{
        //    uiImage.sprite = controlSchemeUIManager.currentSprites.interact;
        //}
        //else if (tutorialImage != null)
        //{
        //    uiImage.sprite = tutorialImage;
        //}
        //else
        //{
        //    uiImage.sprite = controlSchemeUIManager.empty;
        //}
    }
}
