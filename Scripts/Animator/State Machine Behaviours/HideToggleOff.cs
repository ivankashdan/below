using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideToggleOff : StateMachineBehaviour
{
    PlayerState playerState;
    void Awake()
    {
        playerState = FindAnyObjectByType<PlayerState>();
    }

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerState.SetState(PlayerState.State.ground);
    }
}
