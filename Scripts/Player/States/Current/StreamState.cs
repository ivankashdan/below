using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StreamState : CurrentState //make inherit from baseCurrent //WIP
{
    public float moveToSpineSpeed = 10f;
    public float moveInputSpeedInCurrent = 0.5f;

    PlayerState playerState;
    Locomotion locomotion;
    Transform playerObject;

    bool reachedSpine;

    CapsuleCollider currentCapsule;
    public bool IsReachedSpine() => reachedSpine;



    private void OnDrawGizmos()
    {
        if (currentCapsule != null)
        {
            Vector3 nearestPoint = NearestPointOnCapsuleSpine(currentCapsule, playerObject.position);

            // Draw a debug sphere at the nearest point
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(nearestPoint, 0.8f);
        }
    }

    private void Start()
    {
        playerState = FindAnyObjectByType<PlayerState>();
        locomotion = FindAnyObjectByType<Locomotion>();
        playerObject = GameObject.FindWithTag("Player").transform;
    }


    public override void OnEnter()
    {
        locomotion.BypassInputMovement(true);
        locomotion.BypassInputRotation(true);
        locomotion.AddMomentumToRotation(true);
    }

    public override void OnExit()
    {

        locomotion.BypassInputMovement(false);
        locomotion.BypassInputRotation(false);
        locomotion.AddMomentumToRotation(false);

        ResetSpine();

    }

    public override void OnUpdate()
    {

        CurrentSpineCheck();

        locomotion.SetMomentum(CurrentMomentum());

        //if (!reachedSpine) locomotion.SetMovement(MoveToCurrentSpine());
    }

    public Vector3 CurrentMomentum() => waterDirection.normalized * waterSpeed;



    public override void AddCurrent(Current current)
    {
        base.AddCurrent(current);

        currentCapsule = current.transform.GetComponent<CapsuleCollider>();
       

        //playerState.SetState(PlayerState.State.cannon);
    }

    public override void RemoveCurrent(Current current)
    {
        base.RemoveCurrent(current);

        currentCapsule = null;

        playerState.SetState(PlayerState.State.falling);
   
    }

    public void ResetSpine() => reachedSpine = false;

    public void CurrentSpineCheck()
    {
        if (!reachedSpine)
        {
            if (IsAtCurrentSpine()) reachedSpine = true;
        }
    }
    public Vector3 MoveToCurrentSpine()
    {
        if (currentCapsule == null) return Vector3.zero;

        Vector3 nearestPoint = NearestPointOnCapsuleSpine(currentCapsule, playerObject.position);
        Vector3 direction = nearestPoint - playerObject.position;
        if (direction.magnitude < 0.1f) return Vector3.zero;
        return direction.normalized * moveToSpineSpeed;
    }
    bool IsAtCurrentSpine()//
    {
        float tolerance = 0.1f; // Define your tolerance value here
        Vector3 nearestPoint = NearestPointOnCapsuleSpine(currentCapsule, playerObject.position);
        //Debug.Log("distance is:" + (Vector3.Distance(playerObject.position, nearestPoint) < tolerance));
        return Vector3.Distance(playerObject.position, nearestPoint) < tolerance;

    }
    Vector3 NearestPointOnCapsuleSpine(CapsuleCollider capsule, Vector3 point)
    {
        Transform capsuleTransform = capsule.transform;

        // Get the capsule parameters
        Vector3 center = capsule.center;
        float height = capsule.height;
        float radius = capsule.radius;
        int direction = capsule.direction;

        // Determine the direction vector
        Vector3 directionVector;
        switch (direction)
        {
            case 0: // X-axis
                directionVector = capsuleTransform.right;
                break;
            case 1: // Y-axis
                directionVector = capsuleTransform.up;
                break;
            case 2: // Z-axis
                directionVector = capsuleTransform.forward;
                break;
            default:
                directionVector = Vector3.up;
                break;
        }

        // Calculate top and bottom points of the capsule in world space
        Vector3 capsuleCenterWorld = capsuleTransform.TransformPoint(center);
        float halfHeight = Mathf.Max(0, (height / 2) - radius);

        Vector3 topPoint = capsuleCenterWorld + directionVector * halfHeight;
        Vector3 bottomPoint = capsuleCenterWorld - directionVector * halfHeight;

        // Project the point onto the line segment (topPoint to bottomPoint)
        Vector3 lineSegment = topPoint - bottomPoint;
        Vector3 projectedPoint = ProjectPointOnLineSegment(bottomPoint, topPoint, point);

        return projectedPoint;
    }
    Vector3 ProjectPointOnLineSegment(Vector3 lineStart, Vector3 lineEnd, Vector3 point)
    {
        Vector3 lineDirection = lineEnd - lineStart;
        float lineLength = lineDirection.magnitude;
        lineDirection.Normalize();

        // Calculate the projection of the point onto the line
        float projectedLength = Vector3.Dot(point - lineStart, lineDirection);

        // Clamp the projection to the segment length
        projectedLength = Mathf.Clamp(projectedLength, 0, lineLength);

        // Calculate the nearest point
        Vector3 nearestPoint = lineStart + lineDirection * projectedLength;

        return nearestPoint;
    }


    Vector3 CurrentMovement()
    {

        bool reachedSpine = IsReachedSpine();
        Vector3 movement = locomotion.GetMovement();
        if (!reachedSpine || movement == Vector3.zero)
        {
            return MoveToCurrentSpine();
        }
        else if (reachedSpine || movement != Vector3.zero)
        {
            return movement * moveInputSpeedInCurrent;
        }
        return Vector3.zero;
    }

    //public Vector3 CurrentMomentum()
    //{
    //    //if (waterCurrent != null)
    //    //{
    //    //    Vector3 direction = waterCurrent.GetWaterDirectionAtPoint(playerObject.position);
    //    //    return direction * waterCurrent.waterSpeed;
    //    //}
    //    //return Vector3.zero;

    //    return waterDirection.normalized * waterSpeed;
    //}
}