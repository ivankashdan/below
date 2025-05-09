using UnityEngine;
using Unity.Cinemachine;
using System.Linq;
using System.Collections;

[RequireComponent(typeof(RecenterManager))]
public class SenseRecenter : MonoBehaviour
{
    public float turnSpeed = 0.5f;
    public float cutoffTime = 2f;
    float delayedStart = 0f;

    //CameraBehaviourManager cameraManager;
    RecenterManager recenterManager;
    ShellSense shellSense;

    PlayerManager playerManager;

    public Transform currentTarget;
    //Transform previousTarget;


    private void Awake()
    {
        //cameraManager = GetComponentInParent<CameraBehaviourManager>();

        recenterManager = GetComponent<RecenterManager>();

        shellSense = FindAnyObjectByType<ShellSense>();

        playerManager = FindAnyObjectByType<PlayerManager>();
    }

    private void OnEnable()
    {
        shellSense.SenseActivated += OnSenseActivated;
        //shellSense.SenseDeactivated += OnSenseDeactivated;
        //shellSense.ShellSenseTargetSet += OnShellSenseTargetSet;
    }
    private void OnDisable()
    {
        shellSense.SenseActivated -= OnSenseActivated;
        //shellSense.SenseDeactivated -= OnSenseDeactivated;
        //shellSense.ShellSenseTargetSet -= OnShellSenseTargetSet;
    }

    Transform GetNearestPOI()
    {
        PointOfInterest[] pointOfInterests = FindObjectsByType<PointOfInterest>(FindObjectsSortMode.None);
        Transform nearestPOI = null;
        float minDistance = float.MaxValue;

        foreach (var poi in pointOfInterests)
        {
            float distance = Vector3.Distance(poi.transform.position, playerManager.playerObject.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestPOI = poi.transform;
            }
        }

        return nearestPOI;
    }

    void OnSenseActivated()
    {
        currentTarget = GetNearestPOI();
        recenterManager.StartRecenter(RecenterManager.Axis.xy, currentTarget, delayedStart, turnSpeed);
        //recenterManager.StartRecenter(
        //true,
        //RecenterManager.Axis.xy,
        //CinemachineOrbitalFollow.ReferenceFrames.AxisCenter,
        //wait,
        //time
        //);

        StopAllCoroutines();
        StartCoroutine(TurnReleaseRoutine());
    }

    IEnumerator TurnReleaseRoutine()
    {
        yield return new WaitForSeconds(cutoffTime);
        recenterManager.StopRecenter();
    }

    void OnSenseDeactivated()
    {

        //recenterManager.StopRecenter();
    }

    void OnShellSenseTargetSet(Transform target)
    {
        currentTarget = target;
        //SetTarget(target);
    }


    //public void SetTarget(Transform target)
    //{
    //    //previousTarget = currentTarget;
    //    currentTarget = target;
    //}

  

}
