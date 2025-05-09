using UnityEngine;
using UnityEngine.Playables;

public class FoodInteractable : Food, IInteractable
{

    PlayerState playerState;

    protected bool isInteractable = true;

    public override void Awake()
    {
        base.Awake();
        playerState = FindAnyObjectByType<PlayerState>();
    }

    //private void Start()
    //{
    //    gameObject.tag = "Food Interactable";
    //}

    public virtual void Interact()
    {
        EatThisFood();
    }

    public virtual bool IsInteractable() => isInteractable; // playerState.IsInState(PlayerState.State.ground);
    public int GetInteractPriority() => 3;

}
