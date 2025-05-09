using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class FoodBarnacle : FoodInteractable
{
    public List<Transform> flesh;

    int fleshCount;

    public override void Awake()
    { 
        base.Awake();

        foreach (Transform item in flesh)
        {
            fleshCount++;
        }
    }

    public override bool EatThisFood()
    {
        if (fleshCount > 0)
        {
            EatResolve();

            fleshCount--;
            Destroy(flesh[fleshCount].gameObject);
           
            if (fleshCount == 0)
            {
                interactionBehaviour.RemoveInteractable(gameObject);
                isInteractable = false;
                gameObject.tag = "Untagged";
                return true;
            }
        }

        return false;

    }

    //public override void Interact()
    // {

    //     if (fleshCount > 0)
    //     {
    //         fleshCount--;

    //         Destroy(flesh[fleshCount].gameObject);

    //         EatThisFood();

    //         if (fleshCount == 0)
    //         {
    //             interactionBehaviour.RemoveInteractable(gameObject);
    //             gameObject.tag = "Untagged";
    //         }

    //     }


    // }
}
