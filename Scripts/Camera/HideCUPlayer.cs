using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class HideCUPlayer : MonoBehaviour
{

    public LayerMask visibleMask;
    public LayerMask hidePlayerMask;

    public float distanceThreshold = 10f;
    public float distanceReappear = 7f;
    public float delay = 2f;
    Transform playerObject;

    bool overrideHide;


    public void OverrideHide()
    {
        overrideHide = true;
        HidePlayer(true);
    }


    private void Start()
    {
        playerObject = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (!overrideHide)
        {
            Vector3 cameraPosition = Camera.main.transform.position;
            Vector3 targetPosition = playerObject.position;

            float distance = Vector3.Distance(cameraPosition, targetPosition);

            if (distance <= distanceThreshold)
            {
                HidePlayer(true);
                //Debug.Log("Camera is within the specified distance of the object.");
            }
            else if (distance >= distanceReappear)
            {
                HidePlayer(false);
                //Debug.Log("Camera is too far from the object.");
            }
        }
      
    }


    void HidePlayer(bool hide)
    {
        if (hide)
        {
            Camera.main.cullingMask = hidePlayerMask;

            if (delayCoroutine != null) StopCoroutine(delayCoroutine);
            delayCoroutine = StartCoroutine(DelayCoroutine());
        }
        else if (delayCoroutine == null)
        {

            Camera.main.cullingMask = visibleMask;
        }


    }

    Coroutine delayCoroutine;

    IEnumerator DelayCoroutine()
    {
        yield return new WaitForSeconds(delay);
        StopCoroutine(delayCoroutine);
        delayCoroutine = null;
    }

    


}
