using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class FallState : AbstractState
{
    public event Action FallStarted;

    public float maxGravity = -10f;
    public float maxGravityDefault = -10f;
    public float gravityAcceleration = 25f;
    public float groundBuffer = 0.5f;
    float groundBufferTimer;


    PlayerState playerState;
    Locomotion locomotion;
    GroundCheck groundCheck;
    AnimationBehaviour animationBehaviour;
    ScaleControl scaleControl;

    private void Start()
    {
        playerState = FindAnyObjectByType<PlayerState>();
        locomotion = FindAnyObjectByType<Locomotion>();
        groundCheck = FindAnyObjectByType<GroundCheck>();
        animationBehaviour = FindAnyObjectByType<AnimationBehaviour>();
        scaleControl = FindAnyObjectByType<ScaleControl>();
    }

    public override void OnEnter() 
    {
        animationBehaviour.TriggerAnimation(AnimationBehaviour.Animation.Fall);
        FallStarted?.Invoke();

    }
    public override void OnExit() 
    {
        animationBehaviour.ResetAnimationTrigger(AnimationBehaviour.Animation.Fall);
        groundBufferTimer = 0;
    }
    public override void OnUpdate()
    {
        if (groundBufferTimer < groundBuffer)
        {
            groundBufferTimer += Time.deltaTime;
        }
        else
        {
            GroundIfOnGround();
        }

        //GlideIfButtonPressed();

        locomotion.SetMomentum(FallGravity());
    }

    //void GlideIfButtonPressed()
    //{
    //    if (
    //        shellCheck.DoesShellHaveGlideAbility() &&
    //        GameState.Instance.IsPlayerInControl()
    //        && GameInput.controls.Gameplay.Glide.IsPressed() 
    //        && !groundCheck.IsWithinDistanceToGround(groundCheck.glideDistance) 
    //        )
    //    {
    //        playerState.SetState(PlayerState.State.gliding);
    //    }
    //}
  
    public Vector3 FallGravity()
    {
        Vector3 momentum = locomotion.GetMomentum();

        float gfxScaledMaxGravity = maxGravity * scaleControl.GetStageGFXScale();

        float x = Mathf.MoveTowards(momentum.x, 0, gravityAcceleration * Time.deltaTime);
        float y = Mathf.MoveTowards(momentum.y, gfxScaledMaxGravity, gravityAcceleration * Time.deltaTime);
        float z = Mathf.MoveTowards(momentum.z, 0, gravityAcceleration * Time.deltaTime);

        return new Vector3(x, y, z);
    }

    void GroundIfOnGround()
    {
        if (groundCheck.IsOnGround)
        {
            playerState.SetState(PlayerState.State.ground);
        }

        //if (groundCheck.IsWithinDistanceToGround(groundCheck.groundDistance)) playerState.SetState(PlayerState.State.ground);
    }


}
