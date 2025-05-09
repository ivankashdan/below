using UnityEngine;

public class CinematicState : AbstractState
{
    AnimationBehaviour animationBehaviour;

    private void Awake()
    {
        //animationBehaviour = FindAnyObjectByType<AnimationBehaviour>();
    }

    public override void OnEnter()
    {
        animationBehaviour.animator.StopPlayback();
    }

    public override void OnExit()
    {
        //animationBehaviour.animator.StartPlayback();
        animationBehaviour.animator.StartPlayback();
    }

    public override void OnUpdate()
    {
    }
}
