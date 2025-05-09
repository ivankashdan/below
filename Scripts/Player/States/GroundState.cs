using UnityEngine;
using System;

public class GroundState : AbstractState
{
    public event Action GroundStarted;

    public float maxGravity = -10f;
    public float maxGravityDefault = -10f;

    PlayerState playerState;
    GroundCheck groundCheck;
    ShellCheck shellCheck;
    Locomotion locomotion;
    AnimationBehaviour animationBehaviour;
    InputManager input;
    ScaleControl scaleControl;
    FoodSystem foodSystem;

    bool active;

    bool fallBufferActive;
    public float fallBuffer = 0.5f;
    float fallBufferTimer;

    private void Awake()
    {
        playerState = FindAnyObjectByType<PlayerState>();
        locomotion = FindAnyObjectByType<Locomotion>();
        groundCheck = FindAnyObjectByType<GroundCheck>();
        shellCheck = FindAnyObjectByType<ShellCheck>();
        animationBehaviour = FindAnyObjectByType<AnimationBehaviour>();
        input = FindAnyObjectByType<InputManager>();
        scaleControl = FindAnyObjectByType<ScaleControl>();
        foodSystem = FindAnyObjectByType<FoodSystem>();
    }

    private void OnEnable()
    {
        foodSystem.FoodEaten += EatAnimationIfPossible;
        //animationBehaviour.AnimationCompleted += OnAnimationCompleted;
    }

    private void OnDisable()
    {
        foodSystem.FoodEaten -= EatAnimationIfPossible;
        //animationBehaviour.AnimationCompleted -= OnAnimationCompleted;
    }

    public override void OnEnter() 
    {
        active = true;

        animationBehaviour.TriggerAnimation(AnimationBehaviour.Animation.Walk);
        GroundStarted?.Invoke();

        animationBehaviour.ArmLayerOverride(true);
    }
    public override void OnExit() 
    {
        active = false;

        animationBehaviour.ResetAnimationTrigger(AnimationBehaviour.Animation.Walk);

        animationBehaviour.ArmLayerOverride(false);

        animationBehaviour.DeSyncAnimatorMoveSpeed();

        fallBufferTimer = 0;
    }
    public override void OnUpdate()
    {
    

        FallIfNotOnGround();

        animationBehaviour.SyncAnimatorMoveSpeed();


        
        
        //HopIfButtonPressed();
        //GestureIfButtonPressed();
        //HideIfButtonPressed();
        ResetEatAnimationIfNotPlaying();

        locomotion.SetMomentum(GroundStick());
    }
    public Vector3 GroundStick()
    {
        float gfxScaledGravity = maxGravity * scaleControl.GetStageGFXScale();

        return new Vector3(0, gfxScaledGravity, 0);
    }


    void FallIfNotOnGround()
    {
        if (groundCheck.IsOnGround == false)
        {
            fallBufferActive = true;
        }
        else
        {
            fallBufferActive = false;
            fallBufferTimer = 0;
        }

        if (fallBufferActive)
        {
            fallBufferTimer += Time.deltaTime;

            if (fallBufferTimer > fallBuffer)
            {
                playerState.SetState(PlayerState.State.falling);
                fallBufferActive = false;
                fallBufferTimer = 0;
            }
        }


        //if (!groundCheck.IsWithinDistanceToGround(groundCheck.groundDistance)) playerState.SetState(PlayerState.State.falling);
    }

    void EatAnimationIfPossible()
    { 
        if(playerState.IsInState(PlayerState.State.ground))
        {
            if (animationBehaviour.IsAnimationPlaying(AnimationBehaviour.Animation.Eat) == false)
            {
                animationBehaviour.TriggerAnimation(AnimationBehaviour.Animation.Eat);
            }
        }
    }

    void ResetEatAnimationIfNotPlaying()
    {
        if (animationBehaviour.IsAnimationFinished(AnimationBehaviour.Animation.Eat))
        {
            animationBehaviour.ResetAnimationTrigger(AnimationBehaviour.Animation.Eat);
        }
    }


  
 



    //void HopIfButtonPressed()
    //{

    //    if (GameState.Instance.IsPlayerInControl() 
    //        && GameInput.controls.Gameplay.Hop.WasPerformedThisFrame()
    //        && !animationBehaviour.IsAnimationPlaying(AnimationBehaviour.Animation.Jump))
    //    {

    //        playerState.SetState(PlayerState.State.jumping);

    //    }
    //}

    //void GestureIfButtonPressed()
    //{
    //    if (GameState.Instance.IsPlayerInControl()
    //        && GameInput.controls.Gameplay.Gesture.WasPerformedThisFrame()
    //        && !animationBehaviour.IsAnimationPlaying(AnimationBehaviour.Animation.Gesture))
    //    {
    //        playerState.SetState(PlayerState.State.gesturing);

    //    }
    //}

    //void HideIfButtonPressed()
    //{
    //    if (GameState.Instance.IsPlayerInControl()
    //        && GameInput.controls.Gameplay.Hide.WasPerformedThisFrame()
    //        && !animationBehaviour.IsAnimationPlaying(AnimationBehaviour.Animation.Hide)
    //        && shellCheck.IsAShellEquipped()
    //        )
    //    {
    //        playerState.SetState(PlayerState.State.hiding);

    //    }
    //}



    //void SetStateToEating()
    //{
    //    playerState.SetState(PlayerState.State.eating);
    //}

    //bool AreConditionsMet(InputAction inputAction, string animationName)
    //{
    //    return GameState.Instance.IsPlayerInControl()
    //    && inputAction.WasPerformedThisFrame()
    //    && !animationBehaviour.IsAnimationPlaying(animationName);
    //}


}
