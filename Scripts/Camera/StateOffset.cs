using UnityEngine;

public class StateOffset : MonoBehaviour
{
    public float composerYFall = -0.5f;
    public float composerYGlide = -0.5f; 

    CameraOffsetBehaviour cameraOffset;

    FallState fallState;
    GroundState groundState;
    GlideState glideState;
    private void Awake()
    {
        cameraOffset = GetComponent<CameraOffsetBehaviour>();

        fallState = FindAnyObjectByType<FallState>();
        groundState = FindAnyObjectByType<GroundState>();
        glideState = FindAnyObjectByType<GlideState>();
    }

    private void OnEnable()
    {
        fallState.FallStarted += OnFall;
        groundState.GroundStarted += OnGround;
        glideState.GlideStarted += OnGlide;
    }

    private void OnDisable()
    {
        fallState.FallStarted -= OnFall;
        groundState.GroundStarted -= OnGround;
        glideState.GlideStarted -= OnGlide;
    }

    void OnFall()
    {
        cameraOffset.StartCameraOffsetTransition(composerYFall);
    }
    void OnGlide()
    {
        cameraOffset.StartCameraOffsetTransition(composerYGlide);
    }

    void OnGround()
    {
        cameraOffset.StartCameraOffsetTransitionToBase();
    }

   
}
