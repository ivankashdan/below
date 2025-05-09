using System.Collections;
using UnityEngine;

public class SenseState : AbstractState
{
    ShellSense shellSense;
    PlayerState playerState;

    CutsceneManager cutsceneManager;

    public float moveWait = 1f;
    bool moveReady;

    private void Awake()
    {
        shellSense = FindAnyObjectByType<ShellSense>();
        playerState = FindAnyObjectByType<PlayerState>();

        cutsceneManager = FindAnyObjectByType<CutsceneManager>();
    }

    public override void OnEnter()
    {
        //if (cutsceneManager.playableDirector.state == UnityEngine.Playables.PlayState.Playing)
        //{
        //    playerState.SetState(PlayerState.State.ground);
        //}

        shellSense.Sense(true);

        StartCoroutine(MoveSuspensionCoroutine());
    }

    public override void OnExit() 
    {
        shellSense.Sense(false);
    }

    public override void OnUpdate() 
    {
        if (moveReady)
        {
            if (InputManager.controls.Gameplay.Move.IsInProgress())
            {
                playerState.SetState(PlayerState.State.ground);
            }
        }
    }

    IEnumerator MoveSuspensionCoroutine()
    {
        moveReady = false;
        yield return new WaitForSeconds(moveWait);
        moveReady = true;
    }
}
