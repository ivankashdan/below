public class HideState : AbstractState
{
    AnimationBehaviour animationBehaviour;
    InputManager input;
    PlayerState playerState;

    //ShellCheck shellCheck;
    ShellConnection shellConnection;
    //ShellReference shellReference;

    enum State
    {
        hiding,
        hidden, 
        unhiding,
        visible
    }

    State currentState;

    private void Start()
    {
        animationBehaviour = FindAnyObjectByType<AnimationBehaviour>();
        input = FindAnyObjectByType<InputManager>();
        playerState = FindAnyObjectByType<PlayerState>();
        //shellCheck = FindAnyObjectByType<ShellCheck>();
        shellConnection = FindAnyObjectByType<ShellConnection>();
    }

    public override void OnEnter()
    {
        animationBehaviour.TriggerAnimation(AnimationBehaviour.Animation.Hide);
        animationBehaviour.TailShellLayerOverride(false);

        //shellReference = shellCheck.GetShellRef();

        shellConnection.SetShellTargetToLoose();

        currentState = State.hiding;

    }

    public override void OnExit()
    {
        animationBehaviour.TailShellLayerOverride(true);
        shellConnection.SetShellTargetToDefault();

        currentState = State.visible;
    }

    public override void OnUpdate()
    {
        if (currentState == State.hiding && animationBehaviour.IsAnimationFinished(AnimationBehaviour.Animation.Hide)) 
        {
            currentState = State.hidden;
        }

        if (currentState == State.hidden && InputManager.controls.Gameplay.Move.IsInProgress())
        {
            animationBehaviour.ResetAnimationTrigger(AnimationBehaviour.Animation.Hide);
            animationBehaviour.TriggerAnimation(AnimationBehaviour.Animation.Unhide);
            currentState = State.unhiding;
        }

        if (currentState == State.unhiding && animationBehaviour.IsAnimationFinished(AnimationBehaviour.Animation.Unhide))
        {
            animationBehaviour.ResetAnimationTrigger(AnimationBehaviour.Animation.Unhide);
            playerState.SetState(PlayerState.State.ground);
        }
    }
}
