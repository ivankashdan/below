using UnityEngine;
using static TutorialMessages;
using UnityEngine.InputSystem;

public class TutorialSprint : MonoBehaviour, IListenCheck
{
    public Prompt GetPrompt() => Prompt.sprint;
    public InputAction GetAction() => InputManager.controls.Gameplay.Sprint;
    public bool GetCheck() => playerState.IsInState(PlayerState.State.ground);
    public State GetState() => state;
    public State SetState(State state) => this.state = state;
    State state;

    KrillCloseState krillCloseState;
    TutorialMessages tutorialMessages;
    PlayerState playerState;

    private void Awake()
    {
        krillCloseState = FindAnyObjectByType<KrillCloseState>();
        tutorialMessages = GetComponent<TutorialMessages>();
        playerState = FindAnyObjectByType<PlayerState>();

    }

    private void OnEnable()
    {

        krillCloseState.KrillFleeing += OnKrillFleeing;
    }
    private void OnDisable()
    {
        krillCloseState.KrillFleeing -= OnKrillFleeing;
    }

    void OnKrillFleeing()
    {
        tutorialMessages.SetPromptActiveIfInactive(TutorialMessages.Prompt.sprint);
    }

   

}
