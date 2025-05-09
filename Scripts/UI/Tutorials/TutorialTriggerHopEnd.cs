using Unity.VisualScripting;
using UnityEngine;
using static TutorialMessages;

public class TutorialTriggerHopEnd : TutorialTrigger
{
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            tutorialMessages.SetPromptActive(prompt);
        }
    }

}
