using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class StateComposition : MonoBehaviour
{
    public float composerYFall = 0.25f;

    ScreenYBehaviour screenY;

    FallState fallState;
    GroundState groundState;
    private void Awake()
    {
        screenY = GetComponent<ScreenYBehaviour>();

        fallState = FindAnyObjectByType<FallState>();
        groundState = FindAnyObjectByType<GroundState>();
    }

    private void OnEnable()
    {
        fallState.FallStarted += OnFall;
        groundState.GroundStarted += OnGround;
    }

    private void OnDisable()
    {
        fallState.FallStarted -= OnFall;
        groundState.GroundStarted -= OnGround;
    }

    void OnFall()
    {

        screenY.StartScreenYTransition(composerYFall);
    }

    void OnGround()
    {
        screenY.StartScreenYTransitionToBase();
    }


}
