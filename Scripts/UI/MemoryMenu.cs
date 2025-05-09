using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class MemoryMenu : MonoBehaviour
{
    public event Action MemoryViewed;
    public event Action MemoryClosed;
    public event Action<Sprite> MemoryRead;
    public event Action StoppedReading;

    Sprite storedSprite;

    public enum State
    {
        none,
        viewing,
        reading
    }

    State currentState;

    CinemachineVirtualCameraBase readingCamera;
    PlayerState playerState;

    private void Start()
    {
        playerState = FindAnyObjectByType<PlayerState>();
    }

    public bool IsMemoryOpen() => currentState != State.none;

    public void ViewMemory(CinemachineVirtualCameraBase virtualCamera, Sprite sprite)
    {
        SetState(State.viewing);

        readingCamera = virtualCamera;
        readingCamera.Priority = 10;

        storedSprite = sprite;
    }


    void OnEnterState(State state)
    {
        switch (state)
        {
            case State.none:
                MemoryClosed?.Invoke();

                readingCamera.Priority = 0;
                storedSprite = null;
                playerState.SetState(PlayerState.State.ground);

                //InputManager.Instance.SetStateDelayed(InputManager.State.Gameplay);

                break;
            case State.viewing:
                MemoryViewed?.Invoke();

                playerState.SetState(PlayerState.State.cinematic);

                //InputManager.Instance.SetStateDelayed(InputManager.State.Memory);

                break;
            case State.reading:
                MemoryRead?.Invoke(storedSprite);
                break;
        }
        Debug.Log($"Memory state: {state}");
    }

    void OnExitState(State state)
    {
        switch (state)
        {
            case State.none:
                break;
            case State.viewing:
                break;
            case State.reading:
                StoppedReading?.Invoke();
                break;
        }
    }

    public State GetState()
    {
        return currentState;
    }

    public void SetState(State state)
    {
        OnExitState(currentState);

        currentState = state;

        OnEnterState(state);
    }

 

    //Coroutine blockInputCoroutine;

    //bool inputBlocked;
    //float inputDelay = 0.2f;

    //private void Update()
    //{
    //    if (IsMemoryOpen() && !inputBlocked)
    //    {
    //        if (GameInput.controls.Memory.Read.WasPerformedThisFrame())
    //        {
    //            if (currentState == State.viewing)
    //            {
    //                SetState(State.reading);
    //            }
    //            else if (currentState == State.reading)
    //            {
    //                SetState(State.viewing);
    //            }
    //        }
    //        if (GameInput.controls.Memory.Interact.WasPressedThisFrame())
    //        {
    //            SetState(State.none);
    //        }
    //    }
    //}

    //void StartBlockInputCoroutine()
    //{
    //    if (blockInputCoroutine != null) StopCoroutine(blockInputCoroutine);
    //    blockInputCoroutine = StartCoroutine(BlockInputCoroutine());
    //}


    //IEnumerator BlockInputCoroutine()
    //{
    //    inputBlocked = true;
    //    GameInput.Instance.SuspendActions(true);
    //    yield return new WaitForSeconds(inputDelay);
    //    GameInput.Instance.SuspendActions(false);
    //    inputBlocked = false;
    //    blockInputCoroutine = null;
    //}


    //void StopViewingMemory()
    //{

    //    SetState(State.none);

    //    readingCamera.Priority = 0;

    //    storedSprite = null;

    //    playerState.SetState(PlayerState.State.ground);

    //    StartBlockInputCoroutine();
    //}



    //void ReadMemory()
    //{
    //    StartedReading?.Invoke(storedSprite);

    //    SetState(State.reading);

    //    StartBlockInputCoroutine();
    //}

    //void StopReadingMemory()
    //{
    //    StoppedReading?.Invoke();

    //    SetState(State.viewing);

    //    StartBlockInputCoroutine();
    //}


    //void CloseMemory()
    //{
    //    if (currentState == State.reading)
    //    {
    //        StopReadingMemory();
    //    }

    //    if (currentState == State.viewing)
    //    {
    //        StopViewingMemory();
    //    }
    //}


}
