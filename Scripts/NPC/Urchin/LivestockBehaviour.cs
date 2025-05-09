using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class LivestockBehaviour : MonoBehaviour
{
    public static event Action UrchinFleeing;
    public enum State
    {
        resting,
        watching,
        wander,
        following,
        fleeing
    }


    public State playerVisibleState = State.resting;
    public State noPlayerVisibleState = State.resting;

    public bool scaredOfProximity = false;
    public bool scaredOfGesture = true;


    string surprisedTrigger = "surprised";

    State previousState;
    Vector3 targetPosition;

    NavMeshAgent agent;
    SphereCollider sphereCollider;
    AnimationBehaviour animationBehaviour;
    Transform playerObject;
    Animator animator;

    Renderer gfxRenderer;

    float searchCheck = 0.1f;
    float searchTimePassed = 0;

    public float sightRadius = 20f;
    public float followDistance = 7f;
    public float fleeDistance = 15f;
    public float gestureDistance = 12f;

    public float normalSpeed = 1f;
    public float followSpeed = 5f;
    public float fleeSpeed = 8f;

    public float fleeLength = 3f;

    public float watchRotationSpeed = 5f;


    public Collider territory;

    public State state = State.resting;

    public AudioSource sfxSurprise;

    bool signalled;
    bool coinTossed = true; //maintains initial setting



    void SetSpeed(float speed)
    {
        agent.speed = speed;
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        sphereCollider = GetComponentInChildren<SphereCollider>();
        animationBehaviour = FindAnyObjectByType<AnimationBehaviour>();
        playerObject = GameObject.FindWithTag("Player").transform;


        state = noPlayerVisibleState;

        gfxRenderer = GetComponentInChildren<Renderer>();
    }

    private void OnDrawGizmosSelected()
    {
        if (IsPlayerVisible())
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawWireSphere(transform.position, sightRadius);
    }

    private void OnDrawGizmos()
    {
        if (targetPosition != Vector3.zero)
        {
            Gizmos.DrawSphere(targetPosition, 1f);

        }
    }



    void MatchSpeed(State state)
    {
        if (state == State.following)
        {
            SetSpeed(followSpeed);
        }
        if (state == State.fleeing)
        {
            SetSpeed(fleeSpeed);
        }
        else
        {
            SetSpeed(normalSpeed);
        }
    }


    Coroutine signalCoroutine;



    IEnumerator SignalCoroutine()
    {
        UrchinFleeing?.Invoke();
        signalled = true;
        yield return new WaitForSeconds(fleeLength);
        signalled = false;
        signalCoroutine = null;
    }

    private void Update()
    {

        if (animationBehaviour != null && animationBehaviour.IsAnimationPlaying(AnimationBehaviour.Animation.Gesture) && IsPlayerVisible())
        {
            if (signalCoroutine == null)
            {
                signalCoroutine = StartCoroutine(SignalCoroutine());
                Debug.Log("Player gestured");
            }
        }

        //once on stage change
        if (previousState != state)
        {
            MatchSpeed(state);
            agent.ResetPath();
            //coinTossed = false;

            RunAnimation(state);
            ResetAnimation(previousState);

            RunSFX(state);

            //Debug.Log("State is: " + state.ToString());
        }
        previousState = state;


        //every few seconds //includes checks for setting states
        searchTimePassed += Time.deltaTime;
        if (searchTimePassed >= searchCheck)
        {
            searchTimePassed = 0;

            if (IsPlayerVisible())
            {
                if (state == noPlayerVisibleState)
                {
                    state = playerVisibleState;
                }

                if (state == State.fleeing)
                {
                    CheckIfSafe();
                }
                else if (scaredOfGesture) 
                {
                    ScaredOfGesture();
                }
              
                if (scaredOfProximity)
                {
                    ScaredOfProximity();
                }
            }
            else
            {
                state = noPlayerVisibleState;
                //LostSightState();
            }
             
        }

        //constantly update //doesn't include checks, movement module
        if (state == State.watching)
        {
            WatchPlayer();
        }
        else if (state == State.fleeing)
        {
            FleePlayer();
        }
        else if (state == State.following)
        {
            GoFollow();
        }
        else if (state == State.wander)
        {
            Wander();
        }

    }

    void RunSFX(State state)
    {
        if (state == State.fleeing)
        {
            sfxSurprise.Play();
        }
    }
    void RunAnimation(State state)
    {
        if (state == State.fleeing)
        {
            animator.SetTrigger(surprisedTrigger);
        }
    }

    void ResetAnimation(State previousState)
    {
        if (previousState == State.fleeing)
        {
            animator.ResetTrigger(surprisedTrigger);
        }
    }


    void ScaredOfGesture() //resting
    {
        if (IsPlayerSignalling())
        {
            state = State.fleeing;
        }
     
    }

    void ScaredOfProximity() //following, watching, wander
    {
        if (!CheckPlayerIsBeyondDistance(followDistance))
        {
            state = State.fleeing;
        }
    }

    void CheckIfSafe() //fleeing
    {
        if (CheckPlayerIsBeyondDistance(fleeDistance) && !IsPlayerSignalling()) //return to to watching if safe
        {
            state = playerVisibleState;
        }
    }




    bool IsPlayerVisible()
    {
        if (playerObject != null)
        {
            if (Vector3.Distance(playerObject.position, transform.position) <= sightRadius)
            {
                return true;
            }
        }
        return false;
    }

    public bool IsPlayerCloseToUrchin() //following, watching, wander
    {
        if (!CheckPlayerIsBeyondDistance(gestureDistance))
        {
            return true;
        }
        return false;
    }

    public bool CanPlayerSeeUrchin() //following, watching, wander
    {
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);

        // Check if the object is within the camera's view
        if (viewportPosition.x >= 0 && viewportPosition.x <= 1 &&
            viewportPosition.y >= 0 && viewportPosition.y <= 1 &&
            viewportPosition.z > 0) // Z is positive to ensure the object is in front of the camera
            return true;

        return false;

        //if (gfxRenderer != null)
        //{
        //    if (gfxRenderer.isVisible) return true;
        //}
        //return false;
    }


    bool CheckPlayerIsBeyondDistance(float distance)
    {
        float currentDistance = Vector3.Distance(transform.position, playerObject.position);
        return currentDistance >= distance;
    }

    bool IsPlayerSignalling() //needs updating
    {
        if (signalled)
            return true;
        return false;
    }

    void WatchPlayer()
    {
        Vector3 direction = playerObject.position - transform.position;
        direction.y = 0f;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, watchRotationSpeed * Time.deltaTime);
    }

    void GoFollow()
    {
        AssignFollowDistance(followDistance);

    }

    void FleePlayer()
    {
        AssignFollowDistance(fleeDistance);
    }
  
    void AssignFollowDistance(float distance)
    {
        Vector3 followPosition = CalculateFollowPosition(transform.position, playerObject.position, distance);

      
        if (IsPointReachable(followPosition))
        {
            agent.SetDestination(followPosition);
        }
        else if (agent.destination == null || agent.remainingDistance <= sphereCollider.radius)
        {
            int maxAttempts = 10;
            float sampleRange = sightRadius;

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

    Vector3 CalculateFollowPosition(Vector3 urchinPosition, Vector3 playerPosition, float followDistance)
    {
        Vector3 direction = (urchinPosition - playerPosition);
        Vector3 alteredDirection = new Vector3(direction.x, 0, direction.z).normalized;
        Vector3 newPosition = playerPosition + alteredDirection * followDistance;
        return newPosition;
    }


    void Wander()
    {
       
        if (agent.destination == null || agent.remainingDistance <= sphereCollider.radius)
        {
            if (territory != null)
            {
                if (territory.bounds.Contains(transform.position))
                {
                    ChooseRandomDestinationInTerritory();
                }
                else
                {
                    ChooseRandomDestination();
                }
            }
            else
            {
                ChooseRandomDestination();
            }
        }
    }



    int maxSearchAttempts = 10;
    int searchAttempts = 0;
    void ChooseRandomDestinationInTerritory()
    {
        Vector3 randomPos = UnityEngine.Random.insideUnitSphere * sightRadius;
        Vector3 newDestination = transform.position + randomPos;

        if (territory.bounds.Contains(newDestination))
        {
            agent.destination = newDestination;
        }
        else
        {
            searchAttempts++;
            if (searchAttempts <= maxSearchAttempts)
            {
                ChooseRandomDestinationInTerritory();
            }
            else
            {
                searchAttempts = 0;
            }
        }
        

    }

    void ChooseRandomDestination()
    {
        Vector3 randomPos = UnityEngine.Random.insideUnitSphere * sightRadius;
        Vector3 newDestination = transform.position + randomPos;
        agent.destination = newDestination;
    }


    bool IsPlayerInTerritory()
    {
        if (territory != null)
        {
            if (playerObject != null)
            {
                if (territory.bounds.Contains(playerObject.position))
                {
                    return true;
                }
            }
            return false;
        }
        return true;
    }


    void StateCoinToss(State a, State b)
    {
        if (!coinTossed)
        {
            int coinToss = UnityEngine.Random.Range(0, 2);
            if (coinToss == 0)
            {
                state = a;
            }
            else
            {
                state = b;
            }
            coinTossed = true;
        }
    }

}
