using Unity.Cinemachine;
using UnityEngine;

public class YOverrideOnGroundCollision : CinemachineExtension
{
    CinemachineOrbitalFollow orbitalFollow;
    CustomCameraCollider cameraCollider;


    public float clearance = 4f;

    RaycastHit hit;

    public float transitionSpeed = 4f;

    public State currentState = State.Inactive;

    public float currentVerticalAxis;

    public enum State
    {
        Inactive, 
        Active,
        Resolving
    }

    protected override void Awake()
    {
        base.Awake();

        orbitalFollow = GetComponent<CinemachineOrbitalFollow>();
        cameraCollider = GetComponent<CustomCameraCollider>();
    }


    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        if (currentState == State.Active)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.down * clearance);

            if (hit.collider != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(hit.point, 0.5f);
            }
        }
       

    }

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (!Application.isPlaying) return;

        if (stage == CinemachineCore.Stage.Finalize)
        {
            if (cameraCollider.collisionDirection == CustomCameraCollider.CollisionDirection.Floor)
            {
                currentState = State.Active;
            }

            if (currentState == State.Active)
            {
                float maxVerticalAxis = orbitalFollow.VerticalAxis.Range.y;

                orbitalFollow.VerticalAxis.Value = Mathf.Lerp(orbitalFollow.VerticalAxis.Value, maxVerticalAxis, transitionSpeed * deltaTime);


                Vector3 cameraPosition = state.GetFinalPosition();
                Physics.Raycast(cameraPosition, Vector3.down, out hit, 100f, cameraCollider.occlusionLayers, QueryTriggerInteraction.Ignore);

                if (hit.collider != null)
                {
                    float camDistanceToHit = Vector3.Distance(cameraPosition, hit.point);
                    if (camDistanceToHit > clearance)
                    {
                        currentState = State.Inactive;
                    }
                }
            }

         

            if (currentState == State.Resolving)
            {
                if (orbitalFollow.VerticalAxis.Value > orbitalFollow.VerticalAxis.Center + 5f)
                {
                    orbitalFollow.VerticalAxis.Value = Mathf.Lerp(orbitalFollow.VerticalAxis.Value, orbitalFollow.VerticalAxis.Center, transitionSpeed * deltaTime);
                }
                else
                {
                    currentState = State.Inactive;
                }

            }

            currentVerticalAxis = orbitalFollow.VerticalAxis.Value;
        }
    }

   

    //bool IsDefaultClear()
    //{

    //}

}
