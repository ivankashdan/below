using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using static CustomCameraCollider;

public class CustomCameraCollider : CinemachineExtension
{
    public LayerMask occlusionLayers;

    public float groundOffset;
    [Space(10)]
    public bool colliding;
    public CollisionDirection collisionDirection;
    public string hitString;

    [Header("Smoothing")]
    public float baseSmoothing = 0;   // Faster response when no collision
    public float collisionSmoothing = 15;    // Smoother movement when colliding 
    public float smoothingIncreaseRate = 2f; // Rate of smoothing increase
    //public float collisionResetTime = 1.5f; // Time before resetting to fast response
    public float currentSmoothing;



    Vector3 currentCameraPosition;
    Vector3 targetCameraPosition;

    Color debugColor = Color.green;
    Vector3 debugStartPosition = Vector3.zero;
    Vector3 debugEndPosition = Vector3.zero;

    Transform playerTransform;


    public enum CollisionDirection
    {
        None,
        Floor,
        Wall,
        Ceiling
    }


    protected override void Awake()
    {
        base.Awake();
        PlayerManager playerManager = FindAnyObjectByType<PlayerManager>();
        playerTransform = playerManager.playerObject.transform;

        currentCameraPosition = playerTransform.position; // Initialize camera position
        currentSmoothing = collisionSmoothing; // Start with fast response

    }


    CollisionDirection CheckDirection(RaycastHit hit)
    {
        Vector3 normal = hit.normal;

        if (Vector3.Dot(Vector3.up, normal) > 0.5f)
        {
            return CollisionDirection.Floor;
        }
        else if (Vector3.Dot(Vector3.down, normal) > 0.5f)
        {
            return CollisionDirection.Ceiling;
        }
        else
        {
            return CollisionDirection.Wall;
        }
    }

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage != CinemachineCore.Stage.Body) return; // Prevent multiple updates per frame

        if (playerTransform == null) return;

        RaycastHit hit;
        Vector3 cameraPosition = state.GetFinalPosition();
        Vector3 direction = (cameraPosition - playerTransform.position).normalized;
        float maxDistance = Vector3.Distance(playerTransform.position, cameraPosition);

        if (Physics.Raycast(playerTransform.position, direction, out hit, maxDistance, occlusionLayers, QueryTriggerInteraction.Ignore))
        {
            

            collisionDirection = CheckDirection(hit);

            hitString = collisionDirection.ToString() + ": " + hit.collider.name;
            debugColor = Color.red;
            colliding = true;

            switch (collisionDirection)
            {
                case CollisionDirection.Floor:

                    //Physics.Raycast(hit.point, Vector3.up, out RaycastHit verticalHit, groundOffset, occlusionLayers, QueryTriggerInteraction.Ignore);

                    //if (verticalHit.collider == null)
                    //{
                    //    targetCameraPosition = hit.point + hit.normal * 0.2f + Vector3.up * groundOffset;
                    //}
                    //else
                    //{
                    //    targetCameraPosition = hit.point + hit.normal * 0.2f; // Offset to avoid clipping
                    //}
                    //break;
                    targetCameraPosition = cameraPosition;
                    break;
                case CollisionDirection.Ceiling:
                    targetCameraPosition = hit.point + hit.normal * 0.2f; // Offset to avoid clipping
                    break;
                case CollisionDirection.Wall:
                    targetCameraPosition = hit.point + hit.normal * 0.2f; // Offset to avoid clipping
                    break;
            }

            
            currentSmoothing = Mathf.Lerp(currentSmoothing, collisionSmoothing, smoothingIncreaseRate * deltaTime); // Gradually increase smoothing
        }
        else
        {
            targetCameraPosition = cameraPosition; // No collision, use default position
            debugColor = Color.green;
            hitString = "";
            colliding = false;
            collisionDirection = CollisionDirection.None;

            currentSmoothing = Mathf.Lerp(currentSmoothing, baseSmoothing, smoothingIncreaseRate * deltaTime); // Gradually increase smoothing
        }

        // Smooth transition with dynamically changing smoothing
        currentCameraPosition = Vector3.Lerp(currentCameraPosition, targetCameraPosition, currentSmoothing * deltaTime);

        debugStartPosition = playerTransform.position;
        debugEndPosition = currentCameraPosition;

        //currentDistance = Vector3.Distance(playerTransform.position, currentCameraPosition);

        // Override the final camera position
        state.PositionCorrection += currentCameraPosition - state.GetFinalPosition();

        

    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        Gizmos.color = debugColor;
        Debug.DrawLine(debugStartPosition, debugEndPosition, debugColor);
        if (debugColor == Color.red)
        {
            Gizmos.DrawSphere(debugEndPosition, 0.1f);
        }
    }
}
