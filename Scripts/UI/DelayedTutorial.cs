using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using static TutorialMessages;

abstract public class DelayedTutorial : MonoBehaviour
{
    public float delay = 2f;
    public bool active = true;
    public abstract Prompt GetPrompt();

    protected Coroutine delayedPromptCoroutine;

    protected TutorialMessages tutorialMessages;

    protected virtual void Awake()
    {
        tutorialMessages = GetComponent<TutorialMessages>();
    }

    protected IEnumerator DelayedPromptRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (!tutorialMessages.IsListening())
        {
            tutorialMessages.SetPromptActiveIfInactive(GetPrompt());
        }
        else
        {
            delayedPromptCoroutine = StartCoroutine(DelayedPromptRoutine(2f));
        }
    }
}
