using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class StartingLocation : MonoBehaviour
{

    public Transform location;

    public bool bypass;

    Transform playerObject;


    private void OnEnable()
    {
        playerObject = GameObject.FindWithTag("Player").transform; 
    }

    private void Update()
    {
        if (!bypass && location!= null)
        {
            if (!Application.isPlaying)
            {
                playerObject.position = location.position;
            }
        }
      
    }
}
