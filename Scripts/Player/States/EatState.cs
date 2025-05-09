using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatState: AbstractState //not used currently
{
	AnimationBehaviour animationBehaviour;
	PlayerState playerState;

	private void Start()
	{
		animationBehaviour = FindAnyObjectByType<AnimationBehaviour>();
		playerState = FindAnyObjectByType<PlayerState>();
	}

	public override void OnEnter()
	{
		//animationBehaviour.TriggerAnimation(AnimationBehaviour.Animation.Eat);
    }

	public override void OnExit() { }

	public override void OnUpdate()
	{
		//if (animationBehaviour.IsAnimationFinished(AnimationBehaviour.Animation.Eat))
		//{
		//	animationBehaviour.ResetAnimationTrigger(AnimationBehaviour.Animation.Eat);
		//	playerState.SetState(PlayerState.State.ground);
		//}
	}
}
