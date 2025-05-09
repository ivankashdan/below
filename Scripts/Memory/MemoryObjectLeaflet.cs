using UnityEngine;

public class MemoryObjectLeaflet : MemoryObject
{
    //InteractionBehaviour interactionBehaviour;
    //protected override void Awake()
    //{
    //    base.Awake();
    //    //interactionBehaviour = FindAnyObjectByType<InteractionBehaviour>();
    //}

    protected override void Start() 
    {
        gameObject.tag = "Untagged";
    }

    public void ActivateMemoryObject()
    {
        gameObject.tag = "Memory";
        //interactionBehaviour.CheckIfColliderInsideRange();
    }
}
