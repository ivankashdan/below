using UnityEngine;

public class SFXTrigger : MonoBehaviour
{
    public MusicTrack track;
    AudioCrossfade audioCrossfade;

    public bool setDefault;
    public bool triggerOnce = true;
    bool triggered;


    private void Awake()
    {
        audioCrossfade = FindAnyObjectByType<AudioCrossfade>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (triggerOnce && triggered) return;

            if (setDefault)
            {
                audioCrossfade.CrossfadeToDefault(track);
            }
            else
            {
                audioCrossfade.CrossfadeToOnce(track);
            }

            triggered = true;
        }
    }
}
