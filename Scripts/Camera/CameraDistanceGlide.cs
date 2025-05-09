using UnityEngine;

public class CameraDistanceGlide : MonoBehaviour
{
    public float transitionDuration = 1f;
    public OrbitsReference glideOrbit;


    GlideState glideState;
    OrbitsBehaviour orbitsBehaviour;

    private void Awake()
    {
        glideState = FindAnyObjectByType<GlideState>();   
        orbitsBehaviour = GetComponent<OrbitsBehaviour>();
    }

    private void OnEnable()
    {
        glideState.GlideStarted += OnGlideStarted;
        glideState.GlideFinished += OnGlideFinished;
    }

    private void OnDisable()
    {
        glideState.GlideStarted -= OnGlideStarted;
        glideState.GlideFinished -= OnGlideFinished;
    }

    void OnGlideStarted()
    {
        orbitsBehaviour.StartOrbitTransition(glideOrbit.storedOrbits, transitionDuration);
    }

    void OnGlideFinished()
    {
        orbitsBehaviour.StartOrbitTransitionToDefault(transitionDuration);
    }
}
