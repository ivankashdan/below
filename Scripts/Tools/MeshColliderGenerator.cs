using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class MeshColliderGenerator : MonoBehaviour
{
    public bool createCollidersForRenderers;
    public bool recreateColliders;
    public bool removeDuplicateColliders;

    private void Update()
    {
        if (createCollidersForRenderers)
        {
            CreateMeshCollidersForRenderers();
            createCollidersForRenderers = false;
        }

        if (recreateColliders)
        {
            RecreateMeshColliders();
            recreateColliders = false;
        }

        if (removeDuplicateColliders)
        {
            RemoveDuplicateMeshColliders();
            removeDuplicateColliders = false;
        }

    }

    void CreateMeshCollidersForRenderers()
    {
        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer renderer in renderers)
        {
            MeshCollider newCollider = renderer.gameObject.AddComponent<MeshCollider>();
        }

        Debug.Log("Mesh Colliders have been created for children with MeshRenderer.");
    }


    void RecreateMeshColliders()
    {
        // Find all MeshCollider components in the hierarchy of this GameObject
        MeshCollider[] colliders = GetComponentsInChildren<MeshCollider>();

        foreach (MeshCollider collider in colliders)
        {
            bool colliderState = collider.convex;

            GameObject obj = collider.gameObject;

            // Store the mesh before removing the collider
            //Mesh mesh = collider.sharedMesh;

            // Remove the existing MeshCollider
            DestroyImmediate(collider);

            // Recreate the MeshCollider
            MeshCollider newCollider = obj.AddComponent<MeshCollider>();
            //newCollider.sharedMesh = mesh;

            if (colliderState) newCollider.convex = true;
        }

        Debug.Log("Mesh Colliders have been recreated.");
    }


    void RemoveDuplicateMeshColliders()
    {
        // Get all GameObjects with MeshCollider components in the hierarchy
        MeshCollider[] colliders = GetComponentsInChildren<MeshCollider>();

        List<MeshCollider> destroyList = new List<MeshCollider>();
        // Use a loop to find and remove duplicate MeshColliders
        if (colliders.Length != 0) 
        {
            foreach (MeshCollider collider in colliders)
            {
                GameObject obj = collider.gameObject;
                MeshCollider[] meshColliders = obj.GetComponents<MeshCollider>();

                // If more than one MeshCollider exists, remove the extras
                if (meshColliders.Length > 1)
                {
                    for (int i = 1; i < meshColliders.Length; i++)
                    {
                        destroyList.Add(meshColliders[i]);
                    }
                }
            }

            foreach (var markedMesh in destroyList)
            {
                    DestroyImmediate(markedMesh);
            }
            Debug.Log("Extra Mesh Colliders have been removed.");
        }
        
        
    }


  


}
