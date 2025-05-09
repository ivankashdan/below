using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HIdeCUEnvironment : MonoBehaviour
{
    float distanceThreshold = 3f;
    float distanceReappear = 3f;
    float delay = 0.2f;
    Renderer objectRenderer;
    Coroutine delayCoroutine;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        Vector3 cameraPosition = Camera.main.transform.position;

        Collider targetCollider = GetComponent<Collider>();

        if (targetCollider != null)
        {
            Vector3 closestPoint = targetCollider.ClosestPoint(cameraPosition);

            float distance = Vector3.Distance(cameraPosition, closestPoint);

            if (distance <= distanceThreshold)
            {
                HideObject(true);
                //Debug.Log("Camera is within the specified distance of the object.");
            }
            else if (distance >= distanceReappear)
            {
                HideObject(false);
                //Debug.Log("Camera is too far from the object.");
            }
        }
    }


    void HideObject(bool hide)
    {
        if (hide)
        {
            objectRenderer.enabled = false;

            if (delayCoroutine != null) StopCoroutine(delayCoroutine);
            delayCoroutine = StartCoroutine(DelayCoroutine());
        }
        else if (delayCoroutine == null)
        {
            objectRenderer.enabled = true;
        }
    }
    IEnumerator DelayCoroutine()
    {
        yield return new WaitForSeconds(delay);
        StopCoroutine(delayCoroutine);
        delayCoroutine = null;
    }


}
