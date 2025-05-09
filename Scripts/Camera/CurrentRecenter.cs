using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CurrentRecenter : MonoBehaviour
{
    float wait = 0f;
    float time = 0.5f;
    float releaseTime = 0.5f;

    //CameraBehaviourManager cameraManager;
    RecenterManager recenterManager;

    CannonState cannonState;
    PlayerState playerState;

    private void Awake()
    {
        recenterManager = GetComponent<RecenterManager>();

        cannonState = FindAnyObjectByType<CannonState>();
        playerState = FindAnyObjectByType<PlayerState>();

        //cameraManager = GetComponentInParent<CameraBehaviourManager>();
    }

    private void OnEnable()
    {
        cannonState.CurrentAdded += OnCurrentAdded;
        cannonState.CurrentRemoved += OnCurrentRemoved;
    }

    private void OnDisable()
    {
        cannonState.CurrentAdded -= OnCurrentAdded;
        cannonState.CurrentRemoved -= OnCurrentRemoved;
    }

    void OnCurrentAdded(Current current, bool cameraTurn)
    {
        if (cameraTurn) 
        {
            Vector3 waterDirection = current.GetWaterDirection();
            Vector3 xWaterDirection = new Vector3(waterDirection.x, 0, waterDirection.z);
            recenterManager.StartRecenter(RecenterManager.Axis.xy, xWaterDirection, wait, time);
            //recenterManager.SetTarget()
            //recenterManager.StartRecenter(
            //    true,
            //    RecenterManager.Axis.xy,
            //    CinemachineOrbitalFollow.ReferenceFrames.AxisCenter,
            //    wait,
            //    time
            //    );



        }
       

        //recenterManager.StartRecenter(
        //    RecenterManager.Axis.x, 
        //    cameraManager.followCamera.Follow,
        //    wait, 
        //    time
        //    );
    }

    void OnCurrentRemoved()
    {
        StopAllCoroutines();
        StartCoroutine(ReleaseRecenterCoroutine());
    }

    IEnumerator ReleaseRecenterCoroutine()
    {
        yield return new WaitForSeconds(releaseTime);
        
        if (!playerState.IsInState(PlayerState.State.gliding))
        {
            recenterManager.StopRecenter();
        }
    }

}
