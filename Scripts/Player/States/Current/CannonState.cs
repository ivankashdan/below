using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonState : CurrentState
{
    //public event Action CurrenEnteredWithCameraTurn;
    //public event Action CurrentExitedWithCameraTurn;

    PlayerState playerState;
    Locomotion locomotion;

    AnimationBehaviour animationBehaviour;

    private void Start()
    {
        playerState = FindAnyObjectByType<PlayerState>();
        locomotion = FindAnyObjectByType<Locomotion>();
        animationBehaviour = FindAnyObjectByType<AnimationBehaviour>();
    }

    public override void OnEnter()
    {
        //if (IsCameraTurn) CurrenEnteredWithCameraTurn?.Invoke();

        animationBehaviour.TriggerAnimation(AnimationBehaviour.Animation.Fall);

        locomotion.BypassInputMovement(true);
        locomotion.BypassInputRotation(true);
        locomotion.AddMomentumToRotation(true);
    }
    public override void OnExit()
    {
        //if (IsCameraTurn) CurrentExitedWithCameraTurn?.Invoke();

        animationBehaviour.ResetAnimationTrigger(AnimationBehaviour.Animation.Fall);

        locomotion.BypassInputMovement(false);
        locomotion.BypassInputRotation(false);
        locomotion.AddMomentumToRotation(false);
    }

    public override void OnUpdate() 
    {
        locomotion.SetMomentum(CurrentMomentum());
    }

    public Vector3 CurrentMomentum() => waterDirection.normalized * waterSpeed;

    public override void AddCurrent(Current current)
    {
        base.AddCurrent(current);
        playerState.SetState(PlayerState.State.cannon);
    }

    public override void RemoveCurrent(Current current)
    {   
        base.RemoveCurrent(current);
        playerState.SetState(PlayerState.State.falling);
    }



}
