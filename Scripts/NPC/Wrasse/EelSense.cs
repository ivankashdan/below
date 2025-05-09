using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class EelSense : MonoBehaviour
{
    EelEyes eyes;
    public float sightDistance = 10f;
    public bool showRays = false;
    public LayerMask seenObjectsLayerMask;
    public LayerMask playerObjectLayerMask;

    Transform playerObject;
    ShellCheck shellCheck;
    EelBehaviour eelBehaviour;

    bool playerVisible;
    public bool IsPlayerVisible() => playerVisible;

    private void Start()
    {
        shellCheck = FindAnyObjectByType<ShellCheck>();
        playerObject = GameObject.FindWithTag("Player").transform;
        eelBehaviour = GetComponent<EelBehaviour>();
        
        eyes = GetComponent<EelEyes>();
    }

    private void Update()
    {
        if (playerObject != null)
        {
            foreach (var eye in eyes.eyes)
            {
                if (LookForPlayerWithEye(eye))//
                {
                   //if (IsPlayerUrchin())
                   //{
                   //     eelBehaviour.SwimThroughWindow();
                   //}
                   //else
                   //{
                        playerVisible = true;
                        return;
                   //}
                }
            }
            playerVisible = false;
        }
    }


    bool IsPlayerUrchin()
    {
        GameObject shell = shellCheck.GetShell();
        if (shell != null)
        {
            if (shell.transform.name.Contains("Urchin")) return true;
        }
        return false;
    }
        
        
        


    private bool LookForPlayerWithEye(GameObject eye)
    {
        Vector3 playerDirection = (playerObject.position - eye.transform.position).normalized;
        Ray ray = new Ray(eye.transform.position, playerDirection);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, sightDistance, seenObjectsLayerMask))
        {
            
            if (showRays) DebugRays(ray, hit, eye);

            if (hit.collider.gameObject.tag == "Player") return true;
        }
        return false;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sightDistance);
    }

    void DebugRays(Ray ray, RaycastHit hit, GameObject eye)
    {
        Color rayColorSeen = Color.red;
        Color rayColorUnseen = Color.green;

        float raycastDistance = Vector3.Distance(ray.origin, hit.point); 
        
        if (hit.collider.gameObject.tag == "Player")
        {
            Debug.DrawRay(ray.origin, ray.direction * raycastDistance, rayColorSeen);
        }
        else if (hit.collider.gameObject.tag != "Player")
        {
            Debug.DrawRay(ray.origin, ray.direction * raycastDistance, rayColorUnseen);
        }
    }

}
