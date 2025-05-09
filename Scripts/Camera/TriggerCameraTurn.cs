using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class TriggerCameraTurn : MonoBehaviour
{
    public Transform target;

    //
    public float turnTime = 0.5f;
    public bool oneShot = true;
    public bool turnOnTriggerEnter = true;
    //public bool stopTurnOnTriggerExit = true;
    public bool timedRelease = true;
    public float releaseTime = 2f;

    bool triggered = false;



    float wait = 0f;

    RecenterManager recenterManager;

    enum State
    {
        inactive,
        active,
        resolved
    }

    State state;

    private void Awake()
    {
        recenterManager = FindAnyObjectByType<RecenterManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (oneShot && triggered) return;
     
        if (turnOnTriggerEnter)
        {
            if (other.CompareTag("Player") && state == State.inactive)
            {

                StartTurn();
            }
        }
    }

    //void OnTriggerExit(Collider other)
    //{
    //    if (stopTurnOnTriggerExit)
    //    {
    //        if (other.CompareTag("Player") && state == State.active)
    //        {
    //            EndTurn();
    //        }
    //    }

    //}


    public void StartTurn()
    {
        recenterManager.StartRecenter(RecenterManager.Axis.xy, target, wait, turnTime);
        
        if (timedRelease)
        {
            StopAllCoroutines();
            StartCoroutine(ReleaseOverride());
        }

        state = State.active;
        Debug.Log("Enter");
        triggered = true;
    }

    public void EndTurn()
    {
        if (state == State.active)
        {
            recenterManager.StopRecenter();
            //recenterManager.RevertTarget();
            state = State.resolved;
        }
  

        //StopAllCoroutines();
        //EndTurn();
        //Debug.Log("Exit");
    }

    IEnumerator ReleaseOverride()
    {
        yield return new WaitForSeconds(releaseTime);
        EndTurn();
    }

    //void EndTurn()
    //{

    //}



}
