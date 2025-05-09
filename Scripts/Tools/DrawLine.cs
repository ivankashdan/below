using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class DrawLine : MonoBehaviour
{
  
    LineRenderer lineRenderer;

    public Transform wrasse;
    Transform playerObject;


    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        playerObject = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        if (playerObject != null && wrasse != null)
        {

            lineRenderer.SetPosition(0, playerObject.position);
            lineRenderer.SetPosition(1, wrasse.position);
        }
    }




}
