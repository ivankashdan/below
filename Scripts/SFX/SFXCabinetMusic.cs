using UnityEngine;

public class SFXCabinetMusic : MusicTrack
{
    private void OnTriggerEnter(Collider other)
    {
        PlayTrack();
    }


}
