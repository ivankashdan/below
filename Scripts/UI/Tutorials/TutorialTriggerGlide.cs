using UnityEngine;

public class TutorialTriggerGlide : MonoBehaviour
{
    ShellCheck shellCheck;
    TutorialMessages tutorialMessages;

    //bool activated;

    void Awake()
    {
        shellCheck = FindAnyObjectByType<ShellCheck>();
        tutorialMessages = FindAnyObjectByType<TutorialMessages>();
    }

    void OnTriggerEnter(Collider other)
    {
        //if (activated) return;

        if (shellCheck.DoesShellHaveGlideAbility())
        {
            if (other.gameObject.tag == "Player")
            {
                //activated = true;
                tutorialMessages.SetPromptActiveIfInactive(TutorialMessages.Prompt.glide);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (shellCheck.DoesShellHaveGlideAbility())
        {
            if (other.gameObject.tag == "Player")
            {
                tutorialMessages.SetPromptInactiveIfActive(TutorialMessages.Prompt.glide);
            }
        }
    }




}
