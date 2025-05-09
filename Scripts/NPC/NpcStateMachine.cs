using UnityEngine;

public class NpcStateMachine : MonoBehaviour
{
    public IState restState, seenState, closeState;
 
    protected IState currentState;

    private void Awake()
    {
        restState = (IState)GetComponent<IRest>();
        seenState = (IState)GetComponent<ISeen>();
        closeState = (IState)GetComponent<IClose>();

        if (restState == null || seenState == null || closeState == null)
        {
            Debug.LogError("all states have not been added to NPC");
        } 
    }

    private void Start()
    {
        SetState(restState);
    }

    private void Update()
    {
        currentState.OnUpdate();
    }

    public IState GetState()
    {
        return currentState;
    }

    public void SetState(IState state)
    {
        if (state == null)
        {
            Debug.LogError(state.ToString() + " has not been added to NPC");
        }
        else
        {
            if (currentState != null)
            {
                currentState.OnExit();
            }
            currentState = state;
            currentState.OnEnter();
            Debug.Log("NPC state changed to: " + state.ToString());
        }
    }
}