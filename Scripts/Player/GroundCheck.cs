using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public LayerMask groundLayerMask; //for slope 
    public CharacterController characterController;
    //float checkDelay = 0.5f;

    //float timeCount;


    //[SerializeField] float offsetBase = 2f;
    //[SerializeField] float groundDistanceBase = 4f;
    //[SerializeField] float glideDistanceBase = 8f;

    //[HideInInspector] public float groundDistance;
    //[HideInInspector] public float glideDistance;

    bool onGround;
    public bool IsOnGround => onGround;

    private void Update()
    {
        onGround = characterController.isGrounded;
        //if (timeCount < checkDelay)
        //{
        //    timeCount += Time.deltaTime;
        //}
        //else
        //{
        //    onGround = characterController.isGrounded;
        //    timeCount = 0;
        //}

        //ScaleParams();

        //Transform[] legTips = anatomy.GetLegTips();

        //for (int i = 0; i < legTips.Length; i++)
        //{
        //    raisedLegPositions[i] = legTips[i].position + new Vector3(0, offset, 0);
        //    raisedRootPosition = anatomy.root.position + new Vector3(0, offset, 0);
        //    raisedHeadPosition = anatomy.head.position + new Vector3(0, offset, 0);
        //}

        //onGround = IsWithinDistanceToGround(groundDistance);

    }

    //Vector3[] raisedLegPositions = new Vector3[4];
    //Vector3 raisedRootPosition = new Vector3();
    //Vector3 raisedHeadPosition = new Vector3();
    //float offset;

    //ScaleControl scaleControl;
    //Anatomy anatomy;
    //SlopeRotation slopeRotation;

    //private void Awake()
    //{
    //    scaleControl = FindAnyObjectByType<ScaleControl>();
    //    anatomy = FindAnyObjectByType<Anatomy>();
    //    slopeRotation = FindAnyObjectByType<SlopeRotation>();
    //}

    //private void OnDrawGizmos()
    //{
    //    if (Application.isPlaying)
    //    {
    //        DrawWithinDistance(groundDistance);
    //    }
    //    //DrawWithinDistance(glideDistance);
    //}

    //void DrawWithinDistance(float distance)
    //{
    //    if (IsWithinDistanceToGround(distance))
    //    {
    //        Gizmos.color = Color.green;
    //    }
    //    else
    //    {
    //        Gizmos.color = Color.red;
    //    }

    //    foreach (Vector3 position in raisedLegPositions)
    //    {
    //        Gizmos.DrawRay(position, -slopeRotation.GFX.up * distance);
    //    }
    //    Gizmos.DrawRay(raisedRootPosition, -slopeRotation.GFX.up * distance);
    //    Gizmos.DrawRay(raisedHeadPosition, -slopeRotation.GFX.up * distance);
    //}




    //void ScaleParams()
    //{
    //    float gfxScale = scaleControl.GetStageGFXScale();
    //    offset = offsetBase * gfxScale;
    //    groundDistance = groundDistanceBase * gfxScale;
    //    glideDistance = glideDistanceBase * gfxScale;
    //}

    //public bool IsWithinDistanceToGround(float distance)
    //{
    //    foreach (Vector3 position in raisedLegPositions)
    //    {
    //        if (Physics.Raycast(position, -slopeRotation.GFX.up, distance, groundLayerMask))
    //        {
    //            return true;
    //        }
    //    }

    //    if (Physics.Raycast(raisedRootPosition, -slopeRotation.GFX.up, distance, groundLayerMask))
    //    {
    //        return true;
    //    }


    //    return false;
    //}


}
