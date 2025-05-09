using System.Collections;
using UnityEngine;
using static TutorialMessages;
using UnityEngine.InputSystem;

public class TutorialHop : MonoBehaviour, IListenCheck
{
    public Prompt GetPrompt() => Prompt.hop;
    public InputAction GetAction() => InputManager.controls.Gameplay.Hop;
    public bool GetCheck() => playerState.IsInState(PlayerState.State.jumping);
    public State GetState() => state;
    public State SetState(State state) => this.state = state;
    State state;

    //ShellSense sense;
    PlayerState playerState;

    private void Awake()
    {
        //base.Awake();
        //sense = FindAnyObjectByType<ShellSense>();
        playerState = FindAnyObjectByType<PlayerState>();
    }

    //private void OnEnable()
    //{
    //    sense.SenseDeactivated += OnSenseDeactivated;
    //}
    //private void OnDisable()
    //{
    //    sense.SenseDeactivated -= OnSenseDeactivated;
    //}

    //void OnSenseDeactivated()
    //{
    //    if (active)
    //    {
    //        StartCoroutine(DelayedPromptRoutine(delay));
    //    }
    //}
}
