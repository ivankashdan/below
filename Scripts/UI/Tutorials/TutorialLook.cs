using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Timeline;
using static TutorialMessages;

public class TutorialLook : MonoBehaviour, IListenCheck
{
    public Prompt GetPrompt() => Prompt.look;
    public InputAction GetAction() => InputManager.controls.Gameplay.Look;
    public bool GetCheck() => true;
    public State GetState() => state;
    public State SetState(State state) => this.state = state;
    State state;

    public TimelineAsset cutsceneEndTrigger;

    public bool triggerOnStart = false;

    CutsceneManager cutsceneManager;

    TutorialMessages tutorialMessages;
    private void Awake()
    {
        tutorialMessages = GetComponent<TutorialMessages>();

        cutsceneManager = FindAnyObjectByType<CutsceneManager>();
    }

    private void Start()
    {
        if (triggerOnStart)
        {
            StartCoroutine(DelayedStartRoutine());
        }
    }

    private void OnEnable()
    {
        cutsceneManager.CutsceneEnded += OnCutsceneEnded;
    }
    private void OnDisable()
    {
        cutsceneManager.CutsceneEnded -= OnCutsceneEnded;
    }

    void OnCutsceneEnded(TimelineAsset timelineAsset)
    {
        if (timelineAsset == cutsceneEndTrigger)
        {
            SetPrompt();
        }
    }

    void SetPrompt()
    {
        tutorialMessages.SetPromptActiveIfInactive(TutorialMessages.Prompt.look);
        Debug.Log("Move prompt triggered");
    }

    IEnumerator DelayedStartRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        SetPrompt();
    }


}
