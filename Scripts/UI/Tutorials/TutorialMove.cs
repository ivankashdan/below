using UnityEngine;
using static TutorialMessages;
using UnityEngine.InputSystem;

public class TutorialMove : MonoBehaviour, IListenCheck
{
    public Prompt GetPrompt() => Prompt.move;
    public InputAction GetAction() => InputManager.controls.Gameplay.Move;
    public bool GetCheck() => true;
    public State GetState() => state;
    public State SetState(State state) => this.state = state;
    State state;

    TutorialMessages tutorialMessages;
    private void Awake()
    {
        tutorialMessages = GetComponent<TutorialMessages>();
    }

    private void OnEnable()
    {
        tutorialMessages.TutorialMessageCleared += OnTutorialMessageCleared;
    }
    private void OnDisable()
    {
        tutorialMessages.TutorialMessageCleared -= OnTutorialMessageCleared;
    }

    void OnTutorialMessageCleared(TutorialMessages.Prompt prompt)
    {
        if (prompt == TutorialMessages.Prompt.look)
        {
            tutorialMessages.SetPromptActiveIfInactive(TutorialMessages.Prompt.move);
        }
    }

 


}
