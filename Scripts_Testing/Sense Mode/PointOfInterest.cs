using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PointOfInterest : MonoBehaviour
{

    bool discovered;

    public List<ParticleSystem> particleSystems = new List<ParticleSystem>();

    public List<ParticleSystem> visibleSystems = new List<ParticleSystem>();

    public bool IsDiscovered() => discovered;

    public void Discover(bool value)
    {
        discovered = value;

        if (discovered)
        {
            //ParticleSystem[] particleSystems = gameObject.GetComponentsInChildren<ParticleSystem>();

            foreach (ParticleSystem particleSystem in particleSystems)
            {
                ShellSense.ParticleStopOnDiscover(particleSystem);
            }
            
            foreach(ParticleSystem particleSystem in visibleSystems)
            {
                ShellSense.ParticleStopOnDiscover(particleSystem);

                AudioSource[] audioSources = particleSystem.gameObject.GetComponentsInChildren<AudioSource>();
                foreach (AudioSource audioSource in audioSources)
                {
                    audioSource.volume = 0;
                }
            }
        }

    }
    
    public static void Discover(bool value, GameObject discoverObject)
    {
        PointOfInterest pointOfInterest = discoverObject.GetComponentInChildren<PointOfInterest>();

        if (pointOfInterest == null)
        {
            pointOfInterest = discoverObject.GetComponentInParent<PointOfInterest>();
        }

        if (pointOfInterest != null)
        {
            pointOfInterest.Discover(value);
        }
    }

}
