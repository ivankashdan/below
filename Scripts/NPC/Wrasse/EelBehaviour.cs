using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;

public class EelBehaviour : MonoBehaviour
{

    float investigateAttackDelay = 0.1f;
    float investigatePatience = 0.1f;
    float attackLoseSight = 0.1f;
    float stunRecoveryTime = 10;
    float chaseMultiplier = 2;
    float fleeRecoveryTime = 4;

    public bool eelDisappears;

    public enum State
    {
        patrolling,
        investigating,
        attacking,
        stunned,
        fleeing,
        migrating
    }

    public List<Transform> patrolWaypoints = new List<Transform>();

    State state = State.patrolling;
    State previousState;

    Transform playerObject;
    //AccessTrees accessTrees;

    NavMeshAgent agent;
    EelSense eelSense;
    StunPhysics stunPhysics;

    float investigateSeePlayerTimer;
    float investigateLostPatienceTimer;
    float attackLostSightTimer;
    float stunnedTimer;
    float fleeTimer;

    float startSpeed;
    float startAngularSpeed;
    float startAcceleration;

    int cantFindKelp;
    Vector3 lastSafeDestination;

    Vector3 hidePosition;

    public State GetState() => state;


    public bool kelpRequired = true;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        eelSense = GetComponent<EelSense>();
        stunPhysics = GetComponent<StunPhysics>();
        
        //accessTrees = FindAnyObjectByType<AccessTrees>();
        playerObject = GameObject.FindWithTag("Player").transform;

        startSpeed = agent.speed;
        startAngularSpeed = agent.angularSpeed;
        startAcceleration = agent.acceleration;

        lastSafeDestination = transform.position;//
        hidePosition = transform.position;

        SetChaseSpeed(false);

        //if (kelpRequired) ChooseRandomDestinationInKelp();
        //else 
        if (territory!= null) ChooseRandomDestinationInTerritory();
    }

    void SetChaseSpeed(bool chasing)
    {
        if (chasing)
        {
            agent.speed = startSpeed * chaseMultiplier;
            agent.angularSpeed = startAngularSpeed * chaseMultiplier;
            agent.acceleration = startAcceleration * chaseMultiplier;
        }
        else
        {
            agent.speed = startSpeed;
            agent.angularSpeed = startAngularSpeed;
            agent.acceleration = startAcceleration;
        }
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.M))
        //{
        //    SwimThroughWindow();//
        //}

        if (previousState != state) //once on stage change
        {
            //Debug.Log("Eel state: " + state.ToString());//
            if (state == State.investigating)
            {
                //if (kelpRequired) ChooseRandomDestinationInKelp();
                //else 
                if (territory != null) ChooseRandomDestinationInTerritory();//
            }
            else if (state == State.investigating)
            {
                agent.ResetPath(); //removes the current path to player//
            }

            if (state == State.attacking || state == State.fleeing)
            {
                SetChaseSpeed(true);
            }
            else
            {

                SetChaseSpeed(false);
            }
        }
        previousState = state;

        switch (state) //continuously updated
        {
            case State.patrolling:
                Patrol();
                break;
            case State.investigating:
                InvestigatePlayer();
                break;
            case State.attacking:
                AttackPlayer();
                break;
            case State.stunned:
                StunRecover();
                break;
            case State.fleeing:
                Flee();
                break;
        }
    }

   

    public void Stunned()
    {
        state = State.stunned;
        stunPhysics.StunEel(true);
    }

    public bool IsEelStunned() => state == State.stunned;

    void StunRecover()
    {
        stunnedTimer += Time.deltaTime;

        if (stunnedTimer >= stunRecoveryTime) 
        {
            stunnedTimer = 0;
            stunPhysics.StunEel(false);
            state = State.patrolling;
        }
    }

    void Flee()
    {
        agent.SetDestination(hidePosition);
        fleeTimer += Time.deltaTime;

        if (fleeTimer >= fleeRecoveryTime)
        {
            fleeTimer = 0;
            state = State.patrolling;
        }
    }

    void InvestigatePlayer()
    {
        if (eelSense.IsPlayerVisible())
        {
            investigateSeePlayerTimer += Time.deltaTime;
            investigateLostPatienceTimer = 0;
        }
        else
        {
            investigateSeePlayerTimer = 0;
            investigateLostPatienceTimer += Time.deltaTime;
        }

        if (investigateSeePlayerTimer >= investigateAttackDelay)
        {
            investigateSeePlayerTimer = 0;
            state = State.attacking;
        }

        if (investigateLostPatienceTimer >= investigatePatience)
        {
            investigateLostPatienceTimer = 0;
            state = State.patrolling;
        }
    }

    public void AttackPlayer()
    {
        if (eelSense.IsPlayerVisible())
        {
            attackLostSightTimer = 0;
            if (playerObject)
            {
                agent.SetDestination(playerObject.position);
            }
        }
        else
        {
            attackLostSightTimer += Time.deltaTime;
        }
        if (attackLostSightTimer >= attackLoseSight)
        {
            attackLostSightTimer = 0;
            state = State.investigating;
        }
    }

    void Patrol()
    {
        if (!agent.pathPending && agent.remainingDistance < 1f * transform.localScale.x)
        {
            //if (patrolWaypoints.Count > 0)
            //{
            //    //if (kelpRequired) ChooseNextWaypointInKelp();
            //}
            //else
            //{
                //if (kelpRequired) ChooseRandomDestinationInKelp();
                //else 
                if (territory != null) ChooseRandomDestinationInTerritory();
            //}
        }

        //only investigate if player is visible, stage 5 or smaller, and in kelp?
        if (eelSense.IsPlayerVisible() 
            //&& FindAnyObjectByType<ScaleControl>().GetStage() <= 5 
            //&& SearchLocationForKelp(player.position)
            ) 
        {
            state = State.investigating;//
        }
    }


    //void Migrate()
    //{
    //    territory = null;
    //    state = State.migrating;
    //    agent.ResetPath();
    //    agent.enabled = false;
    //    Animator rootAnimator = GetComponent<Animator>();
    //    rootAnimator.enabled = true;
    //    rootAnimator.SetTrigger("migrate");
    //}

    //public void MigrationComplete()
    //{
    //    territory = null;
    //    Animator rootAnimator = GetComponent<Animator>();
    //    rootAnimator.enabled = false;
    //    agent.enabled = true;
    //    state = State.patrolling;
    //}


    public Collider territory;
    int maxSearchAttempts = 10;
    int searchAttempts;

    int currentWaypointIndex = 0; // Track the current waypoint index
    //void ChooseNextWaypointInKelp()
    //{
    //    currentWaypointIndex = (currentWaypointIndex + 1) % patrolWaypoints.Count;
    //    Transform currentWaypoint = patrolWaypoints[currentWaypointIndex];

    //    if (SearchLocationForKelp(currentWaypoint.position))//
    //    {
    //        agent.destination = currentWaypoint.position;
    //    }
    //    else
    //    {
    //        if (kelpRequired) ChooseRandomDestinationInKelp();
    //        else if (territory != null) ChooseRandomDestinationInTerritory();
    //    }

    //}

    //public Transform windowDestination;
    //public void SwimThroughWindow()
    //{
    //    territory = null;
    //    state = State.migrating;
    //    agent.destination = windowDestination.position;
    //}


    void ChooseRandomDestinationInTerritory()
    {
        Vector3 randomPos = Random.insideUnitSphere * eelSense.sightDistance;
        Vector3 newDestination = transform.position + randomPos;

        bool validDestination = true;

        if (territory != null)
        {
            if (!SearchLocationForTerritory(newDestination)) validDestination = false;
        }

        if (validDestination)
        {
            searchAttempts = 0;
            cantFindKelp = 0;
            agent.destination = newDestination;
            lastSafeDestination = agent.destination;
        }
        else
        {
            searchAttempts++;
            if (searchAttempts <= maxSearchAttempts)
            {
                ChooseRandomDestinationInTerritory();
                //ChooseRandomDestinationInKelp();
            }
            else if (lastSafeDestination != null)
            {
                agent.destination = lastSafeDestination;
            }
        }

    }

    //void ChooseRandomDestinationInKelp()
    //{
    //    if (cantFindKelp >= 100)
    //    {
    //        if (eelDisappears)
    //        {
    //            SwimThroughWindow();
    //            //Migrate();
    //            //Debug.Log("Eel killed");
    //            //Destroy(gameObject);
    //        }
    //        else
    //        {
    //            cantFindKelp = 0;
    //            agent.destination = lastSafeDestination;
    //        }
    //        //Debug.Log("Eel couldn't find path in kelp");

    //        return;
    //    }

    //    Vector3 randomPos = Random.insideUnitSphere * eelSense.sightDistance;
    //    Vector3 newDestination = transform.position + randomPos;

    //    bool validDestination = true;

    //    if (territory!= null)
    //    {
    //        if (!SearchLocationForTerritory(newDestination)) validDestination = false;
    //    }

    //    //if (!SearchLocationForKelp(newDestination))
    //    //{
    //    //    cantFindKelp++;
    //    //    validDestination = false;
    //    //}

    //    if (validDestination)
    //    {
    //        searchAttempts = 0;
    //        cantFindKelp = 0;
    //        agent.destination = newDestination;
    //        lastSafeDestination = agent.destination;
    //    }
    //    else
    //    {
    //        searchAttempts++;
    //        if (searchAttempts <= maxSearchAttempts)
    //        {
    //            ChooseRandomDestinationInKelp();
    //        }
    //    }
    //}

    bool SearchLocationForTerritory(Vector3 location)
    {
        return territory.bounds.Contains(location);
    }
        

    //bool SearchLocationForKelp(Vector3 location)
    //{
    //    if (accessTrees == null) return false;


    //    TreeInstance[] treeInstancesInArea = accessTrees.GetTreeInstancesInArea(location, eelSense.sightDistance / 2);
    //    //Debug.Log("Found trees: " + treeInstancesInArea.Length);

    //    return treeInstancesInArea != null;
    //}



    void AttachPlayer()
    {
        if (playerObject != null)
        {

        }
    }

}
