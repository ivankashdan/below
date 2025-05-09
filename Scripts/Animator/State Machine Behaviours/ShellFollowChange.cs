using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShellFollowChange : StateMachineBehaviour
{
    ShellConnection shellConnection;
    ShellCheck shellCheck;
    ShellReference shellReference;

    private void Awake()
    {
        shellCheck = FindAnyObjectByType<ShellCheck>();
        shellConnection = FindAnyObjectByType<ShellConnection>();
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (shellCheck.IsAShellEquipped())
        {
            shellReference = shellCheck.GetShell().GetComponent<ShellReference>();
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (shellReference != null)
        {
            if (!shellConnection.IsShellTargetLoose)
            {
                shellConnection.SetShellTargetToLoose();
            }
        }
  
            
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (shellReference != null)
        {
            if (shellConnection.IsShellTargetLoose)
            {
                shellConnection.SetShellTargetToDefault();
            }
        }
    }

 
}
