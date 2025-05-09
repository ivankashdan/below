using UnityEngine;


public class FoodPickup : Food
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interaction"))
            //&& foodSystem.DoesPlayerMeetStageRequirement(stageRequirement)) //reimplement this
        {
            EatThisFood();
            //TryToEat();
            //Debug.Log(other.name + " collided with " + transform.name);
        }


    }

   
}

