using UnityEngine;

public class NpcGizmo : NpcBase
{
    private void OnDrawGizmosSelected()
    {
        if (Application.isPlaying)
        {
            if (stateMachine.GetState() == stateMachine.restState)
            {
                Gizmos.color = Color.grey;
            }
            else if (stateMachine.GetState() == stateMachine.seenState)
            {
                Gizmos.color = Color.green;
            }
            else if (stateMachine.GetState() == stateMachine.closeState)
            {
                Gizmos.color = Color.red;
            }
            Gizmos.DrawWireSphere(transform.position, sight.sightRadius);
        }
    }
}