using UnityEngine;

public class SFXMemory : MusicTrack
{
    [Space(10)]
    public MusicTrack trackSpecial;
    public MemoryTextModel memorySpecial;

    MemoryReworkedLogic memoryLogic;
    protected override void Awake()
    {
        base.Awake();
        memoryLogic = FindAnyObjectByType<MemoryReworkedLogic>();
    }


    private void OnEnable()
    {
        memoryLogic.MemoryOpened += OnMemoryOpened;
        memoryLogic.MemoryClosed += OnMemoryClosed;

    }

    private void OnDisable()
    {
        memoryLogic.MemoryOpened -= OnMemoryOpened;
        memoryLogic.MemoryClosed -= OnMemoryClosed;
    }

    void OnMemoryOpened(MemoryTextModel textModel)
    {
        if (textModel == memorySpecial)
        {
            trackSpecial.PlayTrack();
        }
        else
        {
            PlayTrack();
        }
    }

    void OnMemoryClosed(MemoryTextModel textModel)
    {
        StopTrack();
    }



}
