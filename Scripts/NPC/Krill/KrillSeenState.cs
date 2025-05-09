using Unity.Cinemachine;
using UnityEngine;

public class KrillSeenState : NpcBase, IState, ISeen
{
    [SerializeField] protected float closeDistance = 5f;

    [SerializeField] float watchRotationSpeed = 5f;


    public void OnEnter()
    {
        speed.ResetSpeed();

    }

    public void OnExit()
    {
    }

    public void OnUpdate()
    {
        SetRestStateIfPlayerNotVisible();
        SetCloseStateIfPlayerClose();

        WatchPlayer();
    }

    void SetRestStateIfPlayerNotVisible()
    {
        if (!sight.IsPlayerVisible())
        {
            stateMachine.SetState(stateMachine.restState);
        }
    }

    void SetCloseStateIfPlayerClose()
    {
        if (sight.IsPlayerVisible() && sight.IsPlayerWithinDistance(closeDistance))
        {
            stateMachine.SetState(stateMachine.closeState);
        }
    }

    public void WatchPlayer()
    {
        Vector3 direction = sight.playerObject.position - transform.position;
        direction.y = 0f;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, watchRotationSpeed * Time.deltaTime);
    }


}