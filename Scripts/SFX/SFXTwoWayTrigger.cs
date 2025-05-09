using UnityEngine;

public class SFXTwoWayTrigger : MonoBehaviour
{
    public MusicTrack trackOne;
    public MusicTrack trackTwo;
    AudioCrossfade audioCrossfade;

    public bool setDefault;
    //public bool triggerOnce = true;
    //bool triggered;

    MusicTrack lastTriggeredTrack;

    private void Awake()
    {
        audioCrossfade = FindAnyObjectByType<AudioCrossfade>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //if (triggerOnce && triggered) return;

            if (lastTriggeredTrack == null)
            {
                TriggerTrack(trackOne);
            }
            else if (lastTriggeredTrack == trackOne)
            {
                TriggerTrack(trackTwo);
            }
            else if (lastTriggeredTrack == trackTwo)
            {
                TriggerTrack(trackOne);
            }
            //triggered = true;
        }
    }


    void TriggerTrack(MusicTrack track)
    {
        if (setDefault)
        {
            audioCrossfade.CrossfadeToDefault(track);
        }
        else
        {
            audioCrossfade.CrossfadeToOnce(track);
        }

        lastTriggeredTrack = track;
    }
}
