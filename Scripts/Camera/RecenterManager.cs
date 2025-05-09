using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class RecenterManager : MonoBehaviour
{
    Axis currentAxis;
    CinemachineOrbitalFollow.ReferenceFrames currentTarget;
    public Axis GetCurrentAxis() => currentAxis;
    public enum Axis
    {
        none,
        x,
        y,
        xy
    }

    bool active;
    public bool IsActive => active;

    Vector2 targetValues;
    Transform currentTargetTransform;
    Transform emptyRecenterTarget;
    //Transform previousTargetTransform;

    CinemachineOrbitalFollow.ReferenceFrames defaultTarget;
    float defaultWaitForX;
    float defaultTimeForX;
    float defaultCenterForX;
    float defaultWaitForY;
    float defaultTimeForY;
    float defaultCenterForY;

    CameraBehaviourManager cameraManager;
    CinemachineOrbitalFollow orbitalFollow;
    CinemachineInputAxisController inputAxisController;

    private void Awake()
    {
        cameraManager = GetComponentInParent<CameraBehaviourManager>();
        orbitalFollow = cameraManager.followCamera.GetComponent<CinemachineOrbitalFollow>();
        inputAxisController = cameraManager.followCamera.GetComponent<CinemachineInputAxisController>();

        defaultTarget = orbitalFollow.RecenteringTarget;

        defaultTimeForX = orbitalFollow.HorizontalAxis.Recentering.Time;
        defaultWaitForX = orbitalFollow.HorizontalAxis.Recentering.Wait;
        defaultCenterForX = orbitalFollow.HorizontalAxis.Center;

        defaultTimeForY = orbitalFollow.VerticalAxis.Recentering.Time;
        defaultWaitForY = orbitalFollow.VerticalAxis.Recentering.Wait;
        defaultCenterForY = orbitalFollow.VerticalAxis.Center;

        emptyRecenterTarget = new GameObject("EmptyRecenterTarget").transform;
    }


    //public Current current; //TESTS
    //private void Update() //TESTS
    //{
    //    if (Input.GetKeyDown(KeyCode.C)) 
    //    {
    //        Vector3 waterDirection = current.GetWaterDirection();
    //        Vector3 xWaterDirection = new Vector3(waterDirection.x, 0, waterDirection.z); 
    //        StartRecenter(Axis.xy, xWaterDirection, 0, 0.5f);


    //    }
    //}

    private void LateUpdate()
    {
        if (currentAxis == Axis.xy 
            && currentTarget == CinemachineOrbitalFollow.ReferenceFrames.AxisCenter)
        {
            targetValues = GetYawAndPitch(cameraManager.followCamera.transform.position, currentTargetTransform.position);
            orbitalFollow.HorizontalAxis.Center = targetValues.x;
            orbitalFollow.VerticalAxis.Center = targetValues.y;
        }
    }


    public void StartRecenter(Axis axis, Vector3 direction, float wait, float time)
    {
        Vector3 targetPosition = cameraManager.followCamera.transform.position + (direction * 100f);
        emptyRecenterTarget.position = targetPosition;

        StartRecenter(axis, emptyRecenterTarget, wait, time);
    }
    public void StartRecenter(Axis axis, Transform target, float wait, float time)
    {
        SetTarget(target);
        StartRecenter(
            true,
            axis,
            CinemachineOrbitalFollow.ReferenceFrames.AxisCenter,
            wait,
            time
            );
    }

    
   

    public void SetInputAxisController(bool value)
    {
        inputAxisController.enabled = value;
    }

    public void StartRecenter(bool suspendControl, Axis axis, CinemachineOrbitalFollow.ReferenceFrames target, float wait, float time)
    {
        active = true;

        if (suspendControl) inputAxisController.enabled = false;
        orbitalFollow.RecenteringTarget = target;
        currentTarget = target;
        currentAxis = axis;

        if (axis == Axis.x || axis == Axis.xy)
        {
            StartXRecenter(wait, time);
        }
        if (axis == Axis.y || axis == Axis.xy)
        {
            StartYRecenter(wait, time);
        }
    }

    public void StopRecenter()
    {
        active = false;

        currentAxis = Axis.none;
        currentTarget = defaultTarget;
        orbitalFollow.RecenteringTarget = defaultTarget;

        StopXRecenter();
        StopYRecenter();

        inputAxisController.enabled = true;
    }

    void SetTarget(Transform target)
    {
        //previousTargetTransform = currentTargetTransform;
        currentTargetTransform = target;
        //TargetChanged?.Invoke();
    }

    //public void RevertTarget()
    //{
    //    Transform revertingFromTarget = currentTargetTransform;
    //    currentTargetTransform = previousTargetTransform;
    //    previousTargetTransform = revertingFromTarget;
    //    //TargetChanged?.Invoke();
    //}


    void StartXRecenter(float wait, float time)
    {
        orbitalFollow.HorizontalAxis.Recentering.Enabled = true;
        orbitalFollow.HorizontalAxis.Recentering.Wait = wait;
        orbitalFollow.HorizontalAxis.Recentering.Time = time;
    }

    void StartYRecenter(float wait, float time)
    {
        orbitalFollow.VerticalAxis.Recentering.Enabled = true;
        orbitalFollow.VerticalAxis.Recentering.Wait = wait;
        orbitalFollow.VerticalAxis.Recentering.Time = time;
    }


    void StopXRecenter()
    {
        orbitalFollow.HorizontalAxis.Recentering.Enabled = false;
        orbitalFollow.HorizontalAxis.Center = defaultCenterForX;
        orbitalFollow.HorizontalAxis.Recentering.Wait = defaultWaitForX;
        orbitalFollow.HorizontalAxis.Recentering.Time = defaultTimeForX;
        orbitalFollow.HorizontalAxis.CancelRecentering();
    }

    void StopYRecenter()
    {
        orbitalFollow.VerticalAxis.CancelRecentering();
        orbitalFollow.VerticalAxis.TriggerRecentering();
        orbitalFollow.VerticalAxis.Center = defaultCenterForY;
        orbitalFollow.VerticalAxis.Recentering.Wait = defaultWaitForY;
        orbitalFollow.VerticalAxis.Recentering.Time = defaultTimeForY;
    }

    Vector2 GetYawAndPitch(Vector3 cameraPosition, Vector3 targetPosition)
    {
        Vector3 directionToTarget = targetPosition - cameraPosition;
        float distance = directionToTarget.magnitude;

        float yaw = Mathf.Atan2(directionToTarget.x, directionToTarget.z) * Mathf.Rad2Deg;
        
        float pitch = Mathf.Asin(directionToTarget.y / distance) * Mathf.Rad2Deg;
        float invertedPitch = -pitch;

        return new Vector2(yaw, invertedPitch);
    }


    //private void OnApplicationQuit()
    //{   
    //    if (active) StopOverride();
    //}


    //void OnDrawGizmos()
    //{
    //    if (Application.isPlaying)
    //    {
    //        Transform currentTransform = cameraManager.followCamera.transform;
    //        Transform targetTransform = testTarget;

    //        Vector3 currentDirection = currentTransform.forward;
    //        Vector3 targetDirection = (targetTransform.position - currentTransform.position).normalized;

    //        Gizmos.color = Color.yellow;
    //        Gizmos.DrawLine(currentTransform.position, currentTransform.position + currentDirection * 10f);

    //        Gizmos.color = Color.green;
    //        Gizmos.DrawLine(currentTransform.position, currentTransform.position + targetDirection * 10f);
    //    }
    //}


}
