using System;
using UnityEngine;
using UnityEngine.AI;

public class KrillCloseState : NpcBase, IState, IClose
{
    public event Action KrillFleeing;

    [SerializeField] public AudioSource sfxSurprise;

    public float fleeDistance = 15f;

    public float fleeSpeed = 3f;

    public void OnEnter() 
    {
        speed.SetSpeed(fleeSpeed);

        sfxSurprise.Play();

        KrillFleeing?.Invoke();
    }
    public void OnExit() { }
    public void OnUpdate()
    {
        SetSeenStateIfPlayerIsBeyondFleeDistance();

        FleePlayer();
    }

    void SetSeenStateIfPlayerIsBeyondFleeDistance() 
    {
        if (!sight.IsPlayerWithinDistance(fleeDistance))
        {
            stateMachine.SetState(stateMachine.seenState);
        }
    }

    public void FleePlayer()
    {
        AssignDistance(fleeDistance);
    }

    void AssignDistance(float distance)
    {
        Vector3 followPosition = CalculateDistancePosition(transform.position, sight.playerObject.position, distance);


        if (IsPointReachable(followPosition))
        {
            agent.SetDestination(followPosition);
        }
        else if (agent.destination == null || agent.remainingDistance <= sight.sightCollider.radius)
        {
            int maxAttempts = 10;
            float sampleRange = sight.sightRadius;

            Vector3 randomPosition = FindValidNavMeshPosition(followPosition, sampleRange, maxAttempts);
            if (randomPosition != Vector3.zero)
            {
                agent.SetDestination(randomPosition);
            }
            else
            {
                //Debug.LogWarning("Failed to find a valid destination near the follow position.");
            }
        }

    }

    Vector3 CalculateDistancePosition(Vector3 urchinPosition, Vector3 playerPosition, float followDistance)
    {
        Vector3 direction = (urchinPosition - playerPosition);
        Vector3 alteredDirection = new Vector3(direction.x, 0, direction.z).normalized;
        Vector3 newPosition = playerPosition + alteredDirection * followDistance;
        return newPosition;
    }


    bool IsPointReachable(Vector3 destination)
    {
        NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(destination, path);
        return path.status == NavMeshPathStatus.PathComplete;
    }


    Vector3 FindValidNavMeshPosition(Vector3 origin, float range, int attempts)
    {
        for (int i = 0; i < attempts; i++)
        {
            Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * range;
            randomDirection += origin;
            randomDirection.y = origin.y;  // Keep the y position level

            if (IsPointReachable(randomDirection))
            {
                return randomDirection;
            }

        }

        return Vector3.zero;
    }


}