using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintSeekShells : MonoBehaviour, IControllerSwitch
{
    public ControlSchemeUIManager controlSchemeUIManager;

    //public string message = "Hint: seek shells";
    public GameObject prompt;
    public Image image;

    public float readingTime = 2f;
    public bool showHint = true;

    enum State
    {
        Hidden,
        Visible,
        Cleared
    }

    State currentState;
    float elapsedTime;

    ShellSense sense;

    private void Awake()
    {
        sense = FindAnyObjectByType <ShellSense>();
        controlSchemeUIManager = FindAnyObjectByType<ControlSchemeUIManager>();

        //Text text = prompt.GetComponentInChildren<Text>();
        //text.text = message;
    }

    private void OnEnable()
    {
        sense.SenseActivated += OnSenseActivated;
        sense.SenseDeactivated += OnSenseDeactivated;
    }
    private void OnDisable()
    {
        sense.SenseActivated -= OnSenseActivated;
        sense.SenseDeactivated -= OnSenseDeactivated;
    }

    void OnSenseActivated()
    {
        if (elapsedTime > readingTime)
        {
            ShowHint(false);
            currentState = State.Cleared;
        }
        else
        {
            ShowHint(true);
        }

    }

    void OnSenseDeactivated()
    {


        ShowHint(false);
    }


    private void Start()
    {

        ShowHint(false);
    }

    private void Update()
    {
        if (!showHint) return;

        if (currentState == State.Visible)
        {
            elapsedTime += Time.deltaTime;
        }

        //if (elapsedTime > readingTime)
        //{
        //    ShowHint(false);
        //    currentState = State.Cleared;
        //}

        //if (GameState.Instance.IsPlayerInControl())
        //{
        //    if (currentState == State.Hidden && InputManager.controls.Gameplay.ShellSense.WasPerformedThisFrame())
        //    { 
        //        ShowHint(true);
        //    }
        //    if (currentState == State.Visible && InputManager.controls.Gameplay.ShellSense.WasReleasedThisFrame())
        //    {
        //        ShowHint(false);
        //    }
        //}
    }

    public void ShowHint(bool show)
    {
        prompt.SetActive(show);

        //Text text = prompt.GetComponentInChildren<Text>();
        //text.text = show ? message : "";
        //image.enabled = show;

        if (show)
        {
            currentState = State.Visible;
        }
        else
        {
            currentState = State.Hidden;
        }

        //Debug.Log("shell sense: " + show.ToString());
    }

    public void UpdateOnControllerSwitch()
    {
        image.sprite = controlSchemeUIManager.currentSprites.look;
    }


}
