using Unity.Cinemachine;
using UnityEngine;

public class OcclusionChecker : MonoBehaviour
{

    CinemachineVirtualCameraBase virtualCamera;
    Transform targetTransform; // Assign the camera's target
    public LayerMask occlusionLayers; // Define which layers represent obstacles

    bool occluded;
    public bool IsOccluded => occluded;

    private float targetDistance;
    public float TargetDistance => targetDistance;

    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCameraBase>();
        targetTransform = virtualCamera.GetComponent<CinemachineCamera>().Target.TrackingTarget;
    }

    void Update()
    {
        if (virtualCamera != null && targetTransform != null)
        {

            Vector3 cameraPosition = virtualCamera.State.GetFinalPosition();
            Vector3 targetPosition = virtualCamera.LookAt.position;

            targetDistance = Vector3.Distance(cameraPosition, targetPosition);

            if (Physics.Linecast(cameraPosition, targetPosition, out RaycastHit hit, occlusionLayers))
            {
                occluded = true;
                // If the ray hits something, the target is occluded
                Debug.Log("Target is occluded by: " + hit.collider.name);
            }
            else
            {
                occluded = false;
                Debug.Log("Target is visible.");
            }

            



            //Vector3 directionToTarget = targetTransform.position - virtualCamera.transform.position;
            //float distanceToTarget = directionToTarget.magnitude;

            //// Raycast from the camera to the target
            //if (Physics.Raycast(virtualCamera.transform.position, directionToTarget, out RaycastHit hit, distanceToTarget, occlusionLayers))
            //{
            //    occluded = true;
            //    // If the ray hits something, the target is occluded
            //    Debug.Log("Target is occluded by: " + hit.collider.name);
            //}
            //else
            //{
            //    occluded = false;
            //    Debug.Log("Target is visible.");
            //}
        }
    }
}
