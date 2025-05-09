using UnityEngine;
using UnityEngine.Audio;

public class SFXSimpleSense : MusicTrack
{
   

    ShellSense shellSense;

    protected override void Awake()
    {
        base.Awake();
        shellSense = FindAnyObjectByType<ShellSense>();
    }

    private void OnEnable()
    {
        shellSense.SenseActivated += OnShellSenseActivated;
        shellSense.SenseDeactivated += OnShellSenseDeactivated;
    }

    private void OnDisable()
    {
        shellSense.SenseActivated -= OnShellSenseActivated;
        shellSense.SenseDeactivated -= OnShellSenseDeactivated;
    }

    void OnShellSenseActivated()
    {
        PlayTrack();
    }

    void OnShellSenseDeactivated()
    {
        StopTrack();
    }

}
