using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlopeRotation : MonoBehaviour
{
    //slope rotation values

    public Transform GFX;
    public float slopeRotationSpeed = 10f;

    float rotationTimeCount;
    Transform playerObject;
    Quaternion targetRotation;

    PlayerState playerState;
    GroundCheck groundCheck;
    HandleInput handleInput;


    private void Start()
    {
        playerState = FindAnyObjectByType<PlayerState>();
        groundCheck = FindAnyObjectByType<GroundCheck>();
        handleInput = FindAnyObjectByType<HandleInput>();  
        playerObject = GameObject.FindWithTag("Player").transform;
    }

    private void Update()//
    {
        Vector3 input = handleInput.GetHandledInput();
        if (input != Vector3.zero) HandleSlopeRotation(input);
    }

    public void HandleSlopeRotation(Vector3 movement) //effects GFX, not gameObject
    {
        if (Physics.Raycast(playerObject.position, Vector3.down, out RaycastHit hit, Mathf.Infinity, groundCheck.groundLayerMask))
        {
            Vector3 surfaceNormal = hit.normal;
            Vector3 gfxUp = GFX.transform.up;

            if (movement != Vector3.zero)
            {
                targetRotation = Quaternion.FromToRotation(gfxUp, surfaceNormal) * Quaternion.LookRotation(movement);
            }
            else
            {
                targetRotation = Quaternion.FromToRotation(gfxUp, surfaceNormal);//
            }
            rotationTimeCount = 0f; // Reset the rotation timer whenever a new target rotation is set


        }

        rotationTimeCount += Time.deltaTime * slopeRotationSpeed;

        if (!playerState.IsInState(PlayerState.State.ground))
        {
            GFX.transform.localRotation = Quaternion.Slerp(GFX.transform.localRotation, Quaternion.Euler(Vector3.zero), rotationTimeCount);
        }
        else if (movement != Vector3.zero)
        {
            GFX.transform.rotation = Quaternion.Slerp(GFX.transform.rotation, targetRotation, rotationTimeCount);
        }

    }

    //bool IsSlopeDownward() => targetRotation.eulerAngles.x > 7 && targetRotation.eulerAngles.x < 300;

    //float GetSlopeRotation() => targetRotation.eulerAngles.x;

    //public Vector3 GetSlopeDirection()
    //{
    //    float rayLength = 4f; // Adjust the length to match the height you want to check
    //    Vector3 rayOrigin = transform.position + new Vector3(0, 2f, 0); // Adjust the offset to match the height of your character

    //    if (Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hit, rayLength, playerState.groundLayerMask))
    //    {
    //        Vector3 normal = hit.normal;

    //        // Project the normal onto the character's forward direction
    //        Vector3 forward = gfx.transform.forward;
    //        Vector3 slopeDirection = Vector3.ProjectOnPlane(forward, normal).normalized;

    //        //Debug.Log("Slope direction: " + slopeDirection);
    //        return slopeDirection;
    //    }

    //    // If not standing on any slope, return forward direction
    //    return gfx.transform.forward;
    //}

}
