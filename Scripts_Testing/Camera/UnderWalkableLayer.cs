using System.Collections.Generic;
using UnityEngine;

public class UnderWalkableLayer : MonoBehaviour
{
    public string walkableLayerString = "Walkable";  // Layer to track
    public string sheerLayerString = "Sheer";    // Layer to switch to
    public Transform player;                           // Assign player reference
    public float heightThreshold = 0f;  // Offset above player to trigger swap

    private int walkableLayer;
    private int sheerLayer;

    GameObject[] layerObjects;
  

    private void Awake()
    {
        walkableLayer = LayerMask.NameToLayer(walkableLayerString);
        sheerLayer = LayerMask.NameToLayer(sheerLayerString);

        if (walkableLayer == -1 || sheerLayer == -1)
        {
            Debug.LogError("One or both layers do not exist! Check layer names.");
            enabled = false;
        }

        layerObjects = BuildLayerObjectsArray();
    }

    void Update()
    {
        SwapObjectLayersBasedOnThrshold();
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(player.position, new Vector3(player.position.x, player.position.y + heightThreshold, player.position.z));

            if (layerObjects != null)
            {
                foreach (GameObject obj in layerObjects)
                {
                    if (obj != null)
                    {
                        Vector3 meshPoint = GetMeshBottomPoint(obj);
                        if (meshPoint == Vector3.zero) continue;

                        if (obj.layer == sheerLayer)
                        {
                            Gizmos.color = Color.red;
                            Gizmos.DrawSphere(meshPoint, 0.5f);
                        }
                        else if (obj.layer == walkableLayer)
                        {
                            Gizmos.color = Color.green;
                            Gizmos.DrawSphere(meshPoint, 0.5f);
                        }
                    }
                }
            }
        }
    }

    void SwapObjectLayersBasedOnThrshold()
    {

        float playerOffsetHeight = player.position.y + heightThreshold;
        Vector3 highOffsetPosition = new Vector3(player.position.x, playerOffsetHeight, player.position.z);

        foreach (GameObject obj in layerObjects)
        {
            if (obj != null)
            {

                Vector3 meshPosition = GetMeshBottomPoint(obj);
                //MeshCollider meshCollider = obj.GetComponent<MeshCollider>();

                if (meshPosition == Vector3.zero) continue;

                //if (IsWithinBounds(obj, player.gameObject)) //if player offset height is within bounds of object, make walkable
                //{
                //    obj.layer = walkableLayer;
                //}
                //else
                //{
                    if (meshPosition.y > playerOffsetHeight) //if object bottom is above player offset height, make ceiling
                    {
                        obj.layer = sheerLayer;

                    }
                    else if (meshPosition.y <= playerOffsetHeight) //if object bottom is below player offset height, make walkable
                    {


                        obj.layer = walkableLayer;
                    }
                //}

             
            }
        }
    }

    bool IsWithinBounds(GameObject target, GameObject checkingObject)
    {
        Collider targetCollider = target.GetComponent<Collider>();
        if (targetCollider == null) return false; // No collider, can't check

        // Get bounds of the target object
        Bounds targetBounds = targetCollider.bounds;

        // Check if the checking object's position is inside the target's bounds
        return targetBounds.Contains(checkingObject.transform.position);
    }

    Vector3 GetMeshCenterPoint(GameObject gameObject)
    {
        MeshCollider meshCollider = gameObject.GetComponent<MeshCollider>();
        if (meshCollider != null)
        {
            Vector3 worldCenter = meshCollider.bounds.center;
            return worldCenter;
        }
        return Vector3.zero;
        //throw new System.Exception("No mesh collider attached to " + gameObject.name);
    }

    Vector3 GetMeshBottomPoint(GameObject gameObject)
    {
        MeshCollider meshCollider = gameObject.GetComponent<MeshCollider>();
        if (meshCollider != null)
        {
            Vector3 bottomCenter = new Vector3(meshCollider.bounds.center.x, meshCollider.bounds.min.y, meshCollider.bounds.center.z);
            return bottomCenter;
        }
        return Vector3.zero;
        //throw new System.Exception("No mesh collider attached to " + gameObject.name);
    }


    bool IsWithinTouchDistance(GameObject target, GameObject checkingObject, float touchDistance)
    {
        Collider targetCollider = target.GetComponent<Collider>();
        Vector3 checkingObjectPosition = checkingObject.transform.position;

        if (targetCollider == null) return false;

        // Get the closest point on the target's bounds to the checking object
        Vector3 closestPoint = targetCollider.ClosestPoint(checkingObjectPosition);

        // Measure the distance from the closest point to the checking object's center
        float distance = Vector3.Distance(closestPoint, checkingObjectPosition);

        return distance <= touchDistance;
    }

    bool IsBelowMeshBounds(MeshCollider meshCollider, Vector3 position)
    {
        if (meshCollider == null)
            return false;

        Bounds bounds = meshCollider.bounds;

        // Check if target is within the X-Z bounds of the collider
        bool withinHorizontalBounds = position.x >= bounds.min.x &&
                                      position.x <= bounds.max.x &&
                                      position.z >= bounds.min.z &&
                                      position.z <= bounds.max.z;

        // Check if the target is below the lowest Y of the collider
        bool isBelow = position.y < bounds.min.y;

        return withinHorizontalBounds && isBelow;
    }



    Vector3 GetMeshClosestPoint(GameObject gameObject)
    {


        MeshCollider meshCollider = gameObject.GetComponent<MeshCollider>();
        if (meshCollider != null)
        {
            Vector3 closestPoint = meshCollider.ClosestPoint(player.transform.position);
            //Vector3 worldCenter = meshCollider.bounds.center;
            return closestPoint;
        }
        return Vector3.zero;
        //throw new System.Exception("No mesh collider attached to " + gameObject.name);
    }

    GameObject[] BuildLayerObjectsArray()
    {
        List<GameObject> objects = new List<GameObject>();
        GameObject[] allObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);

        foreach (GameObject obj in allObjects)
        {
            if ((obj.layer == walkableLayer || obj.layer == sheerLayer) && obj.GetComponent<MeshCollider>())
            {
                objects.Add(obj);
            }
        }
        return objects.ToArray();
    }
}
