using UnityEngine;

public class SFXCabinetExit : MonoBehaviour
{
    MusicTrack track;
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
            //audioCrossfade.SilenceDefault(false);
            audioCrossfade.CrossfadeToDefault(track);   
        }
            

    }
}
