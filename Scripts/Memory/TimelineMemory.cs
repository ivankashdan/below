using UnityEngine;
using UnityEngine.Playables;

public class TimelineMemory : MemoryTextModel
{


    public PlayableAsset playable;

    MemoryDirector memoryDirector;

    protected override void Awake()
    {
        base.Awake();
        memoryDirector = FindAnyObjectByType<MemoryDirector>();
    }

    public override void Interact()
    {
        base.Interact();
        memoryDirector.OpenTimeline(playable);
    }

    public override void CloseMemory()
    {
        base.CloseMemory();
        memoryDirector.CloseTimeline();
    }



}
