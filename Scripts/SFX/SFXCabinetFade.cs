using UnityEngine;

public class SFXCabinetFade : MonoBehaviour
{
    public MusicTrack track;
    AudioCrossfade audioCrossfade;

    //public float fadeDuration = 2f;

    private void Awake()
    {
        audioCrossfade = FindAnyObjectByType<AudioCrossfade>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //audioCrossfade.SilenceDefault(true);
            audioCrossfade.CrossfadeToDefault(track);
            //audioCrossfade.StopExisting(fadeDuration);
        }
    }


}
