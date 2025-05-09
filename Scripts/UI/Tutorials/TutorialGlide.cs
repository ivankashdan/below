using UnityEngine;
using static TutorialMessages;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class TutorialGlide : MonoBehaviour, IListenCheck
{
    public Prompt GetPrompt() => Prompt.glide;
    public InputAction GetAction() => InputManager.controls.Gameplay.Glide;
    public State GetState() => state;
    public State SetState(State state) => this.state = state;
    State state;
    public bool GetCheck() =>
        playerState.IsInState(PlayerState.State.gliding); 
        //&& shellCheck.DoesShellHaveGlideAbility();

    PlayerState playerState;
    ShellCheck shellCheck;
    private void Awake()
    {
        playerState = FindAnyObjectByType<PlayerState>();
        shellCheck = FindAnyObjectByType<ShellCheck>();
    }
}
