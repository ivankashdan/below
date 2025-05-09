using UnityEngine;

public class TutorialUIPrefixText : MonoBehaviour
{
    public GameObject prefixText;

    TutorialMessages tutorialMessages;
    ControlSchemeUIManager controlSchemeUIManager;

    private void Awake()
    {
        tutorialMessages = FindAnyObjectByType<TutorialMessages>();
        controlSchemeUIManager = FindAnyObjectByType<ControlSchemeUIManager>();
    }
    //private void OnEnable()
    //{
    //    tutorialMessages.TutorialMessageVisible += OnTutorialVisible;
    //    //tutorialMessages.TutorialMessageCleared += OnTutorialCleared;
    //    tutorialMessages.NoTutorialsActive += OnNoTutorialsActive;
    //}

    //private void OnDisable()
    //{
    //    tutorialMessages.TutorialMessageVisible -= OnTutorialVisible;
    //    //tutorialMessages.TutorialMessageCleared -= OnTutorialCleared;
    //    tutorialMessages.NoTutorialsActive -= OnNoTutorialsActive;
    //}


}
