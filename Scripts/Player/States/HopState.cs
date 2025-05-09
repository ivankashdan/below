using NewFeatures;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopState : AbstractState
{
    public float jumpMaxTime = 0.3f;
    public float jumpMultiplier = 0.5f;
    public float jumpUpReduction = 8f;

    public int cost = 0;

    float jumpTime;

    PlayerManager playerManager;
    PlayerState playerState;
    AnimationBehaviour animationBehaviour;
    SpeedChange speedChange;
    GroundCheck groundCheck;
    Locomotion locomotion;
    ScaleControl scaleControl;


    

    private void Start()
    {
        playerManager = FindAnyObjectByType<PlayerManager>();
        playerState = FindAnyObjectByType<PlayerState>();
        speedChange = FindAnyObjectByType<SpeedChange>();
        animationBehaviour = FindAnyObjectByType<AnimationBehaviour>();
        groundCheck = FindAnyObjectByType<GroundCheck>();
        locomotion = FindAnyObjectByType<Locomotion>();
        scaleControl = FindAnyObjectByType<ScaleControl>();
    }


    public override void OnEnter() 
    {
        //foodSystem.AddShellFood(-cost);
        animationBehaviour.TriggerAnimation(AnimationBehaviour.Animation.Jump);
    }
    public override void OnExit() 
    {
        animationBehaviour.ResetAnimationTrigger(AnimationBehaviour.Animation.Jump);
        
    }

    public override void OnUpdate()
    {
        locomotion.SetMomentum(Jump());
    }

    public Vector3 Jump()
    {
        jumpTime += Time.deltaTime;
        if (jumpTime > jumpMaxTime)
        {
            jumpTime = 0;

            if (groundCheck.IsOnGround)
            {
                playerState.SetState(PlayerState.State.ground);
            }
            //if (groundCheck.IsWithinDistanceToGround(groundCheck.groundDistance))
            //{
            //    playerState.SetState(PlayerState.State.ground);
            //}
            else
            {
                playerState.SetState(PlayerState.State.falling);
            }
        }

        float gfxScale = scaleControl.GetStageGFXScale();
        Vector3 forward = playerManager.playerGFX.transform.forward;
        Vector3 up = Vector3.up / (jumpUpReduction * (gfxScale * 2));
        Vector3 direction = forward + up;

        float hopSpeed = speedChange.GetSpeed(SpeedChange.Speed.hop);
        Vector3 jumpMovement = direction * hopSpeed * jumpMultiplier;

        return jumpMovement; /// (gfxScale * 5f); //reconsider this?
    }

    public void SetJumpSettings(float jumpMaxtime, float jumpMultiplier, float jumpUpReduction)
    {
        this.jumpMaxTime = jumpMaxtime;
        this.jumpMultiplier = jumpMultiplier;
        this.jumpUpReduction = jumpUpReduction;
    }


}
