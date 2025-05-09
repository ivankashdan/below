using Unity.Cinemachine;
using UnityEngine;

public class GlideRecenter : MonoBehaviour, IControllerSwitch
{

    float wait = 0.5f;
    float time = 1.5f;

    GlideState glideState;
    RecenterManager recenterManager;

    bool active;

    private void Awake()
    {
        glideState = FindAnyObjectByType<GlideState>();

        recenterManager = GetComponent<RecenterManager>();
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
    public void UpdateOnControllerSwitch()
    {
        if (active)
        {
            //Activate();

            bool isGamepad = ControllerSwitchBehaviour.Instance.IsSchemeActive(ControlScheme.Xbox360);

            if (isGamepad)
            {
                Activate();
            }
            else //if keyboard
            {
                Deactivate();
            }
        }
    }

    void Activate()
    {
        bool isGamepad = ControllerSwitchBehaviour.Instance.IsSchemeActive(ControlScheme.Xbox360);

        if (isGamepad)
        {
            recenterManager.StartRecenter(
             true,
             RecenterManager.Axis.x,
             CinemachineOrbitalFollow.ReferenceFrames.TrackingTarget,
             wait,
             time
            );
            active = true;
        }
        //else
        //{
        //    recenterManager.StartRecenter(
        //    false,
        //    RecenterManager.Axis.x,
        //    CinemachineOrbitalFollow.ReferenceFrames.TrackingTarget,
        //    wait,
        //    time
        //   );
        //}

        //active = true;

    }

    void Deactivate()
    {
        recenterManager.StopRecenter();
        active = false;
    }



    void OnGlideStarted()
    {
        Activate();
    }

    void OnGlideFinished()
    {
        Deactivate();
    }

   
}
