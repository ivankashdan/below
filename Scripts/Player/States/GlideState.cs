using System;
using UnityEngine;

public class GlideState : AbstractState
{
    public event Action GlideStarted;
    public event Action GlideFinished;

    //particles
    public Transform glideTrail;

    public float acceleration = 25f;
    public float maxGravity = -5f;
    public float speedMultiplier = 0.75f;

    InputManager input;

    PlayerState playerState;
    AnimationBehaviour animationBehaviour;
    SpeedChange speedChange;
    GroundCheck groundCheck;
    Locomotion locomotion;
    ScaleControl scaleControl;

    Transform playerObject;

    private void Start()
    {
        playerState = FindAnyObjectByType<PlayerState>();
        animationBehaviour = FindAnyObjectByType<AnimationBehaviour>();
        speedChange = FindAnyObjectByType<SpeedChange>();
        groundCheck = FindAnyObjectByType<GroundCheck>();
        locomotion = FindAnyObjectByType<Locomotion>();
        scaleControl = FindAnyObjectByType<ScaleControl>();

        playerObject = GameObject.FindWithTag("Player").transform;

        input = FindAnyObjectByType<InputManager>();

        GlideTrail(false);
    }


    public override void OnEnter()
    {
        animationBehaviour.TriggerAnimation(AnimationBehaviour.Animation.Glide);
        locomotion.BypassInputMovement(true);
        GlideTrail(true);
        //locomotion.AddMomentumToRotation(true);
        GlideStarted?.Invoke();

    }
    public override void OnExit()
    {
        animationBehaviour.ResetAnimationTrigger(AnimationBehaviour.Animation.Glide);
        locomotion.BypassInputMovement(false);
        GlideTrail(false);
        //locomotion.AddMomentumToRotation(false);
        GlideFinished?.Invoke();
    }

    public override void OnUpdate()
    {
        GroundIfOnGround();
        //FallIfButtonReleased();
      
        locomotion.SetMomentum(GlideClamp());

        //Vector3 rotation = locomotion.GetRotation();
        //Vector3 momentum = locomotion.GetMomentum();
        //locomotion.SetRotation(rotation + momentum);
    }


    void GroundIfOnGround()
    {
        if (groundCheck.IsOnGround)
        {
            playerState.SetState(PlayerState.State.ground);
        }

        //if (groundCheck.IsWithinDistanceToGround(groundCheck.groundDistance)) playerState.SetState(PlayerState.State.ground);
    }
 
    //void FallIfButtonReleased()
    //{
    //    if (GameState.Instance.IsPlayerInControl() 
    //        && !GameInput.controls.Gameplay.Glide.IsPressed())
    //    {
    //        playerState.SetState(PlayerState.State.falling);
    //    }
    //}

    public void GlideTrail(bool glide)
    {
        foreach (Transform t in glideTrail.transform)
        {
            t.GetComponent<TrailRenderer>().enabled = glide;
        }
    }

    public Vector3 GlideClamp() 
    {
        Vector3 forwardGlide = ApplyForwardGlide();

        Vector3 momentum = locomotion.GetMomentum();

        float gfxScaledMaxGravity = maxGravity * scaleControl.GetStageGFXScale();

        float x = Mathf.MoveTowards(momentum.x, forwardGlide.x * speedMultiplier, acceleration * speedMultiplier * Time.deltaTime);
        float y = Mathf.MoveTowards(momentum.y, gfxScaledMaxGravity * speedMultiplier, acceleration * speedMultiplier * Time.deltaTime);
        float z = Mathf.MoveTowards(momentum.z, forwardGlide.z * speedMultiplier, acceleration * speedMultiplier * Time.deltaTime);

        return new Vector3(x, y, z);
    }

    Vector3 ApplyForwardGlide()
    {
        Vector3 right = playerObject.right;
        Vector3 forward = Vector3.Cross(right, Vector3.up);

        float speed = speedChange.GetCurrentSpeed();

        return forward * speed;
    }


}
