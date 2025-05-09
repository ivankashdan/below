using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static TutorialMessages;

public class TutorialGesture : MonoBehaviour, IListenCheck
{
    public Prompt GetPrompt() => Prompt.gesture;
    public InputAction GetAction() => InputManager.controls.Gameplay.Gesture;
    public bool GetCheck() => false; //own internal resolve condition
    //playerState.IsInState(PlayerState.State.ground)
    //animationBehaviour.IsAnimationPlaying(AnimationBehaviour.Animation.Gesture)
    //;

    public State GetState() => state;
    public State SetState(State state) => this.state = state;
    State state;

    PlayerState playerState;
    AnimationBehaviour animationBehaviour;
    TutorialMessages tutorialMessages;
    private void Awake()
    {
        playerState = FindAnyObjectByType<PlayerState>();
        animationBehaviour = FindAnyObjectByType<AnimationBehaviour>();

        tutorialMessages = GetComponent<TutorialMessages>();
    }
    private void OnEnable()
    {
        LivestockBehaviour.UrchinFleeing += OnUrchinFleeing;
        //animationBehaviour.AnimationStarted += OnAnimationStarted;
    }
    private void OnDisable()
    {

        LivestockBehaviour.UrchinFleeing -= OnUrchinFleeing;
        //animationBehaviour.AnimationStarted -= OnAnimationStarted;
    }

    void OnUrchinFleeing()
    {
        if (tutorialMessages != null)
        {
            Prompt prompt = GetPrompt();

            if (tutorialMessages.IsListeningForPrompt(prompt))
            {
                tutorialMessages.SetPromptCleared(prompt);
            }
        }
    }

    void OnAnimationStarted(AnimationBehaviour.Animation animation)
{
        Prompt prompt = GetPrompt(); 

        if (animation == AnimationBehaviour.Animation.Gesture)
        {
            if (tutorialMessages.IsListeningForPrompt(prompt))
            {
                tutorialMessages.SetPromptCleared(prompt);
            }
        }
    }

}
