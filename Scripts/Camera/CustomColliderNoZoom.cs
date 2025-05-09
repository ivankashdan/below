using Unity.Cinemachine;
using UnityEngine;

public class CustomColliderNoZoom : CinemachineExtension
{
    [SerializeField] public LayerMask collisionLayerMask = ~0; // Default: Collides with everything
    [SerializeField] private float collisionRadius = 0.3f; // Radius for sphere cast
    [SerializeField] private float cameraPushback = 0.05f; // Small buffer to prevent clipping

    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage,
        ref CameraState state,
        float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Body)
        {
            Vector3 desiredPosition = state.RawPosition;
            Vector3 followTargetPos = state.ReferenceLookAt; // The object being followed

            // Adjust camera position to prevent clipping
            Vector3 adjustedPosition = ResolveCollisions(followTargetPos, desiredPosition);

            // Apply the adjusted position
            state.RawPosition = adjustedPosition;
        }
    }

    private Vector3 ResolveCollisions(Vector3 targetPos, Vector3 desiredPos)
    {
        Vector3 direction = desiredPos - targetPos;
        float distance = direction.magnitude;

        if (distance <= 0f) return desiredPos; // No movement, no need to check

        direction.Normalize();

        // Use SphereCast to check if the camera would be inside an obstacle
        if (Physics.SphereCast(targetPos, collisionRadius, direction, out RaycastHit hit, distance, collisionLayerMask, QueryTriggerInteraction.Ignore))
        {
            // Place the camera at the hit point, with a small buffer to avoid clipping
            return hit.point + hit.normal * cameraPushback;
        }

        return desiredPos; // No collision, use the original position
    }
}
