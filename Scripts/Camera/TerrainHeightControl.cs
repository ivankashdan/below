using NUnit.Framework.Interfaces;
using Unity.Cinemachine;
using UnityEngine;

public class TerrainHeightControl : MonoBehaviour
{

    //public bool controlActive;
    public State currentState;
    public float currentHeight;
    public float requestedHeight;
    //public float currentOffset = 0;

    float rayYOffset = 2f;
    float baseHeight;

    public enum State
    {
        Inactive,
        Active,
        Resolving
    }

    CameraBehaviourManager cameraManager;
    OrbitsBehaviour orbitsBehaviour;
    CinemachineDeoccluder deoccluder;

    private void Awake()
    {
        cameraManager = GetComponentInParent<CameraBehaviourManager>();
        deoccluder = cameraManager.followCamera.GetComponent<CinemachineDeoccluder>();
        orbitsBehaviour = GetComponent<OrbitsBehaviour>();

    }

    private void Start()
    {
        baseHeight = orbitsBehaviour.GetBaseHeight();
        requestedHeight = baseHeight;
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying == false) return;

        Vector3 cameraPosition = CalculateCameraPosition();

        float offsetDistance = baseHeight + rayYOffset;

        Gizmos.color = Color.green;
        Gizmos.DrawRay(cameraPosition, Vector3.down * offsetDistance);
    }




    private void Update()
    {
        currentHeight = orbitsBehaviour.GetCurrentOrbit(Orbit.Center).Height;

        switch (currentState)
        {
            case State.Inactive:
                Vector3 cameraPosition = CalculateCameraPosition();

                float offsetDistance = baseHeight + rayYOffset;

                Physics.Raycast(
                    cameraPosition,
                    Vector3.down,
                    out RaycastHit hit,
                    offsetDistance,
                    deoccluder.CollideAgainst,
                    QueryTriggerInteraction.Ignore);

                if (hit.collider != null && currentState == State.Inactive)
                {
                    requestedHeight = hit.point.y - offsetDistance; //11 -  10
                                                                    //baseHeight + hit.distance - rayOffset.y;

                    if (requestedHeight > baseHeight)
                    {
                        orbitsBehaviour.StartHeightTransition(requestedHeight);
                        //controlActive = true;

                        currentState = State.Active;
                    }

                    //currentOffset = requestedHeight - baseHeight;
                }
                else if (currentHeight > baseHeight)
                {
                    currentState = State.Resolving;
                    requestedHeight = baseHeight;
                    orbitsBehaviour.StartHeightTransition(baseHeight);
                }

                break;
            case State.Active:
                if (currentHeight == requestedHeight)
                {
                    currentState = State.Inactive;
                }
                break;
            case State.Resolving:
                if (currentHeight == baseHeight)
                {
                    currentState = State.Inactive;
                    requestedHeight = baseHeight;
                }
                break;
        }

    }


    Vector3 CalculateCameraPosition()
    {
        float baseHeight = orbitsBehaviour.GetBaseHeight();

        Vector3 playerPosition = cameraManager.followCamera.Follow.position;
        Vector3 cameraPosition = cameraManager.followCamera.transform.position;
        Vector3 defaultCameraPosition = new Vector3(cameraPosition.x, playerPosition.y, cameraPosition.z);

        Vector3 rayOffset = new Vector3(0, rayYOffset + baseHeight, 0);
        Vector3 offsetCameraPosition = defaultCameraPosition + rayOffset;
        //- new Vector3(0, currentOffset, 0);

        return offsetCameraPosition;
    }

}
