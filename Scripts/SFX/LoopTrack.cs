using UnityEngine;

public class LoopTrack : MonoBehaviour
{
    public AudioClip audioClip;

    public float volume = 0.05f;

    OneShotManager oneShotManager;

    protected virtual void Awake()
    {
        oneShotManager = FindAnyObjectByType<OneShotManager>();
    }

    protected void PlayTrack()
    {
        oneShotManager.audioSource.PlayOneShot(audioClip, volume);
    }

    protected void StopTrack()
    {
        oneShotManager.audioSource.Stop();
    }
}
