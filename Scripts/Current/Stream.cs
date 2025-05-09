using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stream : Current
{
  
    public bool extrude = false;


    void InstantiateCurrent()
    {
        CreateContainerIfNecessary();


        Transform streamExtension = Instantiate(gameObject, transform.parent).transform;
        streamExtension.name = "Current" + " " + transform.parent.childCount.ToString();
        if (streamExtension.childCount != 0)
        {
            for (int i = 0; i < streamExtension.childCount; i++)
            {
                DestroyImmediate(streamExtension.GetChild(i).gameObject);
            }
        }

        Vector3 topPoint = GetCapsuleEndPoint(capsuleCollider, true);

        streamExtension.localPosition = topPoint;
    }

    void CreateContainerIfNecessary()
    {
        string containerName = "Multi-Current";
        bool root = gfx != null;
        if (root &&
            (
            transform.parent == null ||
            transform.parent != null && transform.parent.name != containerName
            )
            )
        {
            GameObject container = new GameObject(containerName);
            container.transform.parent = transform.parent;
            transform.parent = container.transform;
        }
    }

    Vector3 GetCapsuleEndPoint(CapsuleCollider capsule, bool isTop)
    {
        Vector3 center = capsule.transform.TransformPoint(capsule.center);
        float height = Mathf.Max(0, capsule.height / 2 - capsule.radius);
        Vector3 direction = Vector3.zero;

        switch (capsule.direction)
        {
            case 0: // X-axis
                direction = capsule.transform.right;
                break;
            case 1: // Y-axis
                direction = capsule.transform.up;
                break;
            case 2: // Z-axis
                direction = capsule.transform.forward;
                break;
        }

        return center + (isTop ? direction : -direction) * height;
    }


    //void ShowCapusleEndPoints()
    //{
    //    Vector3 topPoint = GetCapsuleEndPoint(capsuleCollider, true);
    //    Vector3 bottomPoint = GetCapsuleEndPoint(capsuleCollider, false);
    //    Debug.DrawLine(topPoint, bottomPoint, Color.red, 5f);//
    //}

}
