using UnityEngine;

public class MusicTrack : MonoBehaviour
{
    public AudioClip audioClip;

    public float fadeIn = 1f;
    //public float fadeOut = 1f;
    public float volume = 0.05f;
    public bool loop = true;
    //[HideInInspector] 
    public float playPosition;

    protected AudioCrossfade audioCrossfade;
    protected virtual void Awake()
    {
        audioCrossfade = FindAnyObjectByType<AudioCrossfade>();
    }

    public virtual void PlayTrackAsDefault()
    {
        audioCrossfade.CrossfadeToDefault(this);
    }

    public virtual void PlayTrack()
    {
        audioCrossfade.CrossfadeToOnce(this);
    }

    protected virtual void StopTrack()
    {
        //playPosition = audioCrossfade.GetPlayPosition();
        audioCrossfade.CrossfadeToDefault();

    }


}
