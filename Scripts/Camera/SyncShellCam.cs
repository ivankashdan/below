using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Camera))]
public class SyncShellCam : MonoBehaviour
{

    Camera shellCam;

    private void Start()
    {
        shellCam = GetComponent<Camera>();
    }

    private void Update()
    {
        shellCam.fieldOfView = Camera.main.fieldOfView;
    }


}
