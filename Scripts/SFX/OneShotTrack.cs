using UnityEngine;
using UnityEngine.Audio;

public class OneShotTrack : MonoBehaviour
{
    public AudioResource audioClip;

    public float volume = 0.05f;

    OneShotManager oneShotManager;

    protected virtual void Awake()
    {
        oneShotManager = GetComponentInParent<OneShotManager>();
    }

    public void PlayTrack()
    {
        oneShotManager.PlayTrack(this);
    }



}
