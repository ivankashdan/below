using UnityEngine;
using static TutorialMessages;
using UnityEngine.InputSystem;

public class TutorialHide : MonoBehaviour, IListenCheck
{
    public Prompt GetPrompt() => Prompt.hide;
    public InputAction GetAction() => InputManager.controls.Gameplay.Hide;
    public bool GetCheck() =>
        playerState.IsInState(PlayerState.State.hiding)
        //&& shellCheck.IsAShellEquipped()
        //&& animationBehaviour.IsAnimationPlaying(AnimationBehaviour.Animation.Hide)
        ;

    public State GetState() => state;
    public State SetState(State state) => this.state = state;
    State state;

    PlayerState playerState;
    ShellCheck shellCheck;
    AnimationBehaviour animationBehaviour;
    private void Awake()
    {
        playerState = FindAnyObjectByType<PlayerState>();
        shellCheck = FindAnyObjectByType<ShellCheck>();
        animationBehaviour = FindAnyObjectByType<AnimationBehaviour>();
    }
}
