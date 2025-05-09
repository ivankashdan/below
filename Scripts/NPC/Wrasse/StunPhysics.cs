using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;

public class StunPhysics : MonoBehaviour
{
    public Rigidbody gfx;

    NavMeshAgent agent;
    Transform playerObject;

    Vector3 originalPosition;
    Vector3 originalRotation;
    Vector3[] originalPositions;
    Vector3[] originalRotations;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerObject = GameObject.FindWithTag("Player").transform;

        Rigidbody[] rbs = GetComponentsInChildren<Rigidbody>();

        originalPositions = new Vector3[rbs.Length];
        originalRotations = new Vector3[rbs.Length];
    }

    public void StunEel(bool stun)
    {
        Rig rig = GetComponentInChildren<Rig>();
        SphereCollider biteCollider = GetComponent<SphereCollider>();  
        if (stun)
        {
            biteCollider.enabled = false;
            //rig.weight = 0;

            agent.enabled = false;

            ApplyGravity(true);

            ApplyForce(gfx.GetComponent<Rigidbody>());
        }
        else
        {
            ApplyGravity(false);

            //rig.weight = 1;

            agent.enabled = true;

            biteCollider.enabled = true;
        }
    }



    void ApplyGravity(bool gravity)
    {
        
        
        Rigidbody[] rbs = GetComponentsInChildren<Rigidbody>();


        if (gravity)
        {
            originalPosition = transform.localPosition;
            originalRotation = transform.localEulerAngles;
        }
        else
        {
            Vector3 newPosition = new Vector3(gfx.transform.position.x, originalPosition.y, gfx.transform.position.z);
            Vector3 newRotation = new Vector3(originalRotation.x, gfx.transform.eulerAngles.y, originalRotation.z);
            transform.localPosition = newPosition;
            transform.localEulerAngles = newRotation;
        }

        for (int i = 0; i < rbs.Length; i++)
        {
            Rigidbody rb = rbs[i];
            if (GetComponent<Rigidbody>() != rb)
            {
                if (gravity)
                {

                    originalPositions[i] = rb.transform.localPosition;
                    originalRotations[i] = rb.transform.localEulerAngles;
                    rb.isKinematic = false;
                    rb.useGravity = true;
                }
                else
                {
                    

                    rb.transform.localPosition = originalPositions[i];
                    rb.transform.localEulerAngles = originalRotations[i];
                    rb.isKinematic = true;
                    rb.useGravity = false;
                }
            }
            
        }
    }
    //
    void ApplyForce(Rigidbody rb)
    {
        if (playerObject!= null)
        {
            float force = 5f;
            Vector3 playerDirection = playerObject.transform.position - gameObject.transform.position;
            Vector3 forceDirection = -playerDirection + Vector3.up;
            rb.AddForce(forceDirection * force, ForceMode.Impulse);
        }
    }
}
