using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureState : AbstractState
{
    public event Action GestureStarted;


    public float delay = 1f;
    float timer;

    PlayerState playerState;
    AnimationBehaviour animationBehaviour;
    InputManager input;
    void Start()
    {
        playerState = FindAnyObjectByType<PlayerState>();
        animationBehaviour = FindAnyObjectByType<AnimationBehaviour>();
        input = FindAnyObjectByType<InputManager>();
    }

    public override void OnEnter() 
    {
        animationBehaviour.TriggerAnimation(AnimationBehaviour.Animation.Gesture);
        GestureStarted?.Invoke();

    }

    public override void OnExit() 
    {
        animationBehaviour.ResetAnimationTrigger(AnimationBehaviour.Animation.Gesture);
    }

    public override void OnUpdate()
    {
        timer += Time.deltaTime;

        if (timer > delay && InputManager.controls.Gameplay.Move.IsInProgress())
        {
            timer = 0f;
            playerState.SetState(PlayerState.State.ground);
        }
    }
}
