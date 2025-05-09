using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXBehaviour : MonoBehaviour
{
    public AudioSource source;

    public AudioClip anemonePickupSFX;
    public AudioClip shellPickupSFX;

    public void Play(AudioClip clip)
    {
        source.clip = clip;
        source.Play();
    }


}
