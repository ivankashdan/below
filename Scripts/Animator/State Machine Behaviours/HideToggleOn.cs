using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HideToggleOn : StateMachineBehaviour
{

    PlayerState playerState;
    void Awake()
    {
        playerState = FindAnyObjectByType<PlayerState>();
    }



    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerState.SetState(PlayerState.State.hiding);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

}
