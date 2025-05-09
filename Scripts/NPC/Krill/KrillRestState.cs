using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class KrillRestState : NpcBase, IState, IRest
{
    [SerializeField] bool wandering;

    Coroutine pauseCoroutine;

    public void OnEnter()
    {
        speed.ResetSpeed();

        //wandering = GetRandomBool();
    }

    public void OnExit()
    {
        StopPauseCoroutine();
        wandering = true;
    }

    public void OnUpdate()
    {
        SetSeenStateIfPlayerVisible();

        if (wandering)
        {
            Wander();
        }
    }

    void SetSeenStateIfPlayerVisible()
    {
        if (sight.IsPlayerVisible())
        {
            stateMachine.SetState(stateMachine.seenState);
        }
    }

    void Wander()
    {
        if (agent.destination == null || agent.remainingDistance <= sight.sightCollider.radius)
        {
            pauseCoroutine ??= StartCoroutine(PauseAndChooseDestination(Random.Range(0, 3)));
        }
    }

    IEnumerator PauseAndChooseDestination(float pauseDuration)
    {
        yield return new WaitForSeconds(pauseDuration);
        ChooseRandomDestination();
        pauseCoroutine = null;
    }


    void StopPauseCoroutine()
    {
        if (pauseCoroutine != null)
        {
            StopCoroutine(pauseCoroutine);
            pauseCoroutine = null;
        }
    }

    void ChooseRandomDestination()
    {
        Vector3 randomPos = Random.insideUnitSphere * sight.sightRadius;
        Vector3 newDestination = transform.position + randomPos;
        agent.destination = newDestination;
    }

    bool GetRandomBool()
    {
        return Random.Range(0, 2) == 0;
    }
}