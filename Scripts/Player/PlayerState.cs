using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public enum State
    {
        ground,
        falling,
        gliding,
        cannon, 
        jumping,
        sensing,
        hiding,
        gesturing,
        eating, //not used currently
        none, //
        reading, //
        cinematic //
    }

    public State state = State.none;

    AbstractState abstractState;

    //state access
    HopState hopState;
    GlideState glideState;
    CannonState cannonState;
    FallState fallState;
    GroundState groundState;
    GestureState gestureState;
    HideState hideState;
    SenseState senseState;
    EatState eatState;
    CinematicState cinematicState;


    Dictionary<State, AbstractState> stateDictionary;
   
    private void Awake()
    {
        hopState = FindAnyObjectByType<HopState>();
        glideState = FindAnyObjectByType<GlideState>();
        cannonState = FindAnyObjectByType<CannonState>();
        groundState = FindAnyObjectByType<GroundState>();
        fallState = FindAnyObjectByType<FallState>();
        gestureState = FindAnyObjectByType<GestureState>();
        hideState = FindAnyObjectByType<HideState>();
        senseState = FindAnyObjectByType<SenseState>();
        eatState = FindAnyObjectByType<EatState>();
        cinematicState = FindAnyObjectByType<CinematicState>();

        stateDictionary = new Dictionary<State, AbstractState>()
        {
            {State.jumping, hopState},
            {State.gliding, glideState},
            {State.ground, groundState},
            {State.falling, fallState },
            {State.cannon, cannonState},
            {State.gesturing, gestureState },
            {State.hiding, hideState },
            {State.sensing, senseState },
            {State.eating, eatState },
            {State.cinematic, cinematicState}
        };

       
    }

    private void Start()
    {
        SetState(State.ground);
    }


    void Update()
    {
        if (abstractState != null) abstractState.OnUpdate();
    }

    public void SetState(State state)
    {
        if (abstractState != null) abstractState.OnExit();

        if (stateDictionary.TryGetValue(state, out abstractState))

        if (abstractState != null) abstractState.OnEnter();

        this.state = state;
        Debug.Log("new state: " + state.ToString());
    }

    public bool IsInState(State state)
    {
        return this.state == state;
    }

    public bool DoesStateReceiveInput()
    {
        switch (state)
        {
            case State.ground:
            case State.falling:
            case State.gliding:
            case State.cannon:
            case State.jumping:
            //case State.eating:
                return true;
            default: return false;
        }
    }

}
