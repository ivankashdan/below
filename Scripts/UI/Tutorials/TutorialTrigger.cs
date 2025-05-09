using UnityEngine;

[RequireComponent (typeof(Collider))]
public class TutorialTrigger : MonoBehaviour
{
    [SerializeField] protected TutorialMessages.Prompt prompt;

    protected TutorialMessages tutorialMessages;

    public bool removePromptOnExit = false;

    void Awake()
    {
        tutorialMessages = FindAnyObjectByType<TutorialMessages>();
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            tutorialMessages.SetPromptActiveIfInactive(prompt);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (removePromptOnExit)
        {
            if (other.gameObject.tag == "Player")
            {
                tutorialMessages.SetPromptInactiveIfActive(prompt);
            }
        }
    }

}
