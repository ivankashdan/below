using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TutorialTriggerHide : MonoBehaviour
{
    TutorialMessages tutorialMessages;

    //bool activated;

    void Awake()
    {
        tutorialMessages = FindAnyObjectByType<TutorialMessages>();
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    if (!activated && other.gameObject.tag == "Player")
    //    {
    //        StartTutorial();
    //    }
    //}

    //void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.tag == "Player")
    //    {
    //        ClearTutorial();
    //    }
    //}

    public void StartTutorial()
    {
        tutorialMessages.SetPromptActiveIfInactive(TutorialMessages.Prompt.hide);
        //activated = true;
    }

    public void ClearTutorial()
    {
       
        tutorialMessages.SetPromptCleared(TutorialMessages.Prompt.hide);
        
    }

}
