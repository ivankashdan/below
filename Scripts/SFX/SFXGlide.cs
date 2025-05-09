using UnityEngine;

public class SFXGlide : LoopTrack
{

    GlideState glideState;

    protected override void Awake()
    {
        base.Awake();

        glideState = FindAnyObjectByType<GlideState>();
    }

    private void OnEnable()
    {
        glideState.GlideStarted += OnGlideStarted;
        glideState.GlideFinished += OnGlideFinished;
    }

    private void OnDisable()
    {
        glideState.GlideStarted -= OnGlideStarted;
        glideState.GlideFinished -= OnGlideFinished;
    }

    void OnGlideStarted()
    {
        PlayTrack();
    }

    void OnGlideFinished()
    {
        StopTrack();
    }

}
