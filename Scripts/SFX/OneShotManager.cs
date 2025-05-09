using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class OneShotManager : MonoBehaviour
{
    [HideInInspector] public AudioSource audioSource;


    AudioSource[] audioSources;

    private void Awake()
    {
        audioSources = GetComponents<AudioSource>();
    }

    public void PlayTrack(OneShotTrack track)
    {
        foreach (AudioSource source in audioSources)
        {
            if (source.isPlaying) continue;

            source.resource = track.audioClip;
            source.volume = track.volume;
            source.Play();
            return;
        }
    }
}
