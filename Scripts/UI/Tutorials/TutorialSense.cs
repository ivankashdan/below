using System.Collections;
using UnityEngine;
using static TutorialMessages;
using UnityEngine.InputSystem;

public class TutorialSense : MonoBehaviour, IListenCheck
{
    public Prompt GetPrompt() => Prompt.sense;
    public InputAction GetAction() => InputManager.controls.Gameplay.ShellSense;
    public State GetState() => state;
    public State SetState(State state) => this.state = state;
    State state;
    public bool GetCheck() => false;//playerState.IsInState(PlayerState.State.sensing);

    PlayerState playerState;

    ShellSense shellSense;

    TunnelManager tunnelManager;

    TutorialMessages tutorialMessages;
    
    private void Awake()
    {
        //base.Awake();
        
        shellSense = FindAnyObjectByType<ShellSense>();

        playerState = FindAnyObjectByType<PlayerState>();

        tunnelManager = FindAnyObjectByType<TunnelManager>();

        tutorialMessages = FindAnyObjectByType<TutorialMessages>();
    }

    private void OnEnable()
    {
        tunnelManager.PlayerExitedTunnel += OnPlayerExitedTunnel;
        shellSense.SenseDeactivated += OnShellSenseDeactivated;
        //tutorialMessages.TutorialMessageCleared += OnTutorialMessageCleared;
    }
    private void OnDisable()
    {
        tunnelManager.PlayerExitedTunnel += OnPlayerExitedTunnel;
        shellSense.SenseDeactivated += OnShellSenseDeactivated;
        //tutorialMessages.TutorialMessageCleared -= OnTutorialMessageCleared;
    }

    void OnPlayerExitedTunnel()
    {
        tutorialMessages.SetPromptActiveIfInactive(GetPrompt());
    }

    void OnShellSenseDeactivated()
    {
        tutorialMessages.SetPromptCleared(GetPrompt());
    }

    //void OnTutorialMessageCleared(Prompt prompt)
    //{
    //    if (prompt == Prompt.move && active)
    //    {
    //        if (delayedPromptCoroutine != null)
    //        {
    //            StopCoroutine(delayedPromptCoroutine);
    //        }
    //        delayedPromptCoroutine = StartCoroutine(DelayedPromptRoutine(delay));
    //    }
    //}

   
}
