using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EelEyes : MonoBehaviour
{
  
    public List<GameObject> eyes;
    public Material patrolEyes;
    public Material investEyes;
    public Material attackEyes;
    public Material stunEyes;
    public Material urchinEyes;

    EelBehaviour eelBehaviour;
    EelBehaviour.State state;
    EelBehaviour.State previousState;


    private void Start()
    {
        eelBehaviour = GetComponent<EelBehaviour>();

        ChangeEyeColour(patrolEyes);
    }

    private void Update()
    {
        state = eelBehaviour.GetState();

        if (state != previousState)
        {
            switch (state)
            {
                case EelBehaviour.State.patrolling:
                    ChangeEyeColour(patrolEyes);
                    break;
                case EelBehaviour.State.investigating:
                    ChangeEyeColour(investEyes);
                    break;
                case EelBehaviour.State.attacking:
                    ChangeEyeColour(attackEyes);
                    break;
                case EelBehaviour.State.stunned:
                    ChangeEyeColour(stunEyes);
                    break;
                case EelBehaviour.State.fleeing:
                    ChangeEyeColour(patrolEyes);
                    break;
                case EelBehaviour.State.migrating:
                    ChangeEyeColour(urchinEyes);
                    break;
            }
        }
        previousState = state;
    }


    void ChangeEyeColour(Material colour)
    {
        foreach (GameObject eye in eyes)
        {
            var renderer = eye.GetComponent<MeshRenderer>();
            renderer.material = colour;
        }
    }
 
}
