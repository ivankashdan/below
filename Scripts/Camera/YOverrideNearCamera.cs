using Unity.Cinemachine;
using UnityEngine;

public class YOverrideNearCamera : CinemachineExtension
{
    public float currentDistance;
    public float currentVerticalAxis;
    public AnimationCurve overrideCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    public float transitionSpeed = 1f;

    float overrideThreshold = 2f;

    public bool overrideActive;


    CinemachineOrbitalFollow orbitalFollow;
    Transform playerTransform;

    protected override void Awake()
    {
        base.Awake();
        PlayerManager playerManager = FindAnyObjectByType<PlayerManager>();
        playerTransform = playerManager.playerObject.transform;

        orbitalFollow = GetComponent<CinemachineOrbitalFollow>();
    }

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (playerTransform == null) return;

        if (stage == CinemachineCore.Stage.Finalize)
        {
            Vector3 cameraPosition = state.GetFinalPosition();

            float distanceToPlayer = Vector3.Distance(playerTransform.position, cameraPosition);



            if (distanceToPlayer < overrideThreshold)
            {
                overrideActive = true;

                float percentageToPlayerPoint = Mathf.InverseLerp(0, overrideThreshold, distanceToPlayer);

                float minVerticalAxis = orbitalFollow.VerticalAxis.Range.x;

                float targetVerticalAxis = Mathf.Lerp(minVerticalAxis, orbitalFollow.VerticalAxis.Center, percentageToPlayerPoint);

                //float curvedTarget = overrideCurve.Evaluate(targetVerticalAxis);

                orbitalFollow.VerticalAxis.Value = Mathf.Lerp(orbitalFollow.VerticalAxis.Value, targetVerticalAxis, transitionSpeed * deltaTime);
            }
            else
            {
                if (overrideActive && orbitalFollow.VerticalAxis.Value < orbitalFollow.VerticalAxis.Center - 5f)
                {
                    orbitalFollow.VerticalAxis.Value = Mathf.Lerp(orbitalFollow.VerticalAxis.Value, orbitalFollow.VerticalAxis.Center, transitionSpeed * deltaTime);
                }
                else
                {
                    overrideActive = false;
                }
            }


            //float clampedDistance = Mathf.Clamp(distanceToPlayer, 0, 2);//

            currentDistance = distanceToPlayer;
            currentVerticalAxis = orbitalFollow.VerticalAxis.Value;
        }
    }
}
