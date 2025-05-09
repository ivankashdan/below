using System;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class ShellSense : MonoBehaviour
{
    public event Action SenseActivated;
    public event Action SenseDeactivated;
    public event Action<Transform> ShellSenseTargetSet;

    public Camera senseCameraOverlay;

    PointOfInterest[] pointOfInterests;



    //ShellReference[] looseShells;
    //List<ShellReference> looseShells;

    public float transitionDuration = 0.5f;

    float senseDeactiveLifetimeModifier = 0.1f;
    static float particleDiscoverLifetimeModifier = 0.4f;


    //bool shellSense;

    //public bool IsShellSenseActive() => shellSense;

    //ShellCheck shellCheck;
    //ShellEquip shellEquip;
    //FocalZoom focalZoom;

    private void Awake()
    {
        pointOfInterests = FindObjectsByType<PointOfInterest>(FindObjectsSortMode.None);

        //senseCameraOverlay.gameObject.SetActive(false);

        SetParticleSystems(false);
        ClearParticleSystems();
    }

    void ClearParticleSystems()
    {
        foreach (PointOfInterest pointOfInterest in pointOfInterests)
        {
            if (pointOfInterest == null) continue;


            foreach (ParticleSystem particleSystem in pointOfInterest.particleSystems)
            {
                particleSystem.Clear();
            }
        }
    }


    public void Sense(bool sense)
    {
        //shellSense = sense;

        SelectGrade.Instance.SelectVolume(sense ? SelectGrade.Instance.senseVolume : SelectGrade.Instance.defaultVolume, transitionDuration);

        //senseCameraOverlay.gameObject.SetActive(sense);

        SetParticleSystems(sense);

        if (sense)
        {
            SenseActivated?.Invoke();
        }
        else
        {
            SenseDeactivated?.Invoke();
        }
    }


    void SetParticleSystems(bool value)
    {
        foreach (PointOfInterest pointOfInterest in pointOfInterests)
        {
            if (pointOfInterest == null) continue;
            
            //ParticleSystem[] particleSystems = pointOfInterest.gameObject.GetComponentsInChildren<ParticleSystem>();

            foreach (ParticleSystem particleSystem in pointOfInterest.particleSystems)
            {
                if (value && !pointOfInterest.IsDiscovered())
                {
                    particleSystem.Play();

                }
                else
                {
                    particleSystem.Stop();
                    DecreaseParticlesLiftime(particleSystem, senseDeactiveLifetimeModifier);
                }
            }
            
        }
    }


    public static void ParticleStopOnDiscover(ParticleSystem particleSystem)
    {
        particleSystem.Stop();
        DecreaseParticlesLiftime(particleSystem, particleDiscoverLifetimeModifier);
    }

  
    static void DecreaseParticlesLiftime(ParticleSystem particleSystem, float lifetimeModifier)
    {
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[particleSystem.particleCount];
        int count = particleSystem.GetParticles(particles);

        for (int i = 0; i < count; i++)
        {
            particles[i].remainingLifetime *= lifetimeModifier; // Make them die 90% faster
        }

        particleSystem.SetParticles(particles, count);
    }

    //void SelectCurrentShell() //currently can only handle 1 of each size
    //{
    //    looseShells = FindObjectsByType<ShellReference>(FindObjectsSortMode.None);

    //    foreach (ShellReference shell in looseShells)
    //    {
    //        GameObject shellObject = shell.gameObject;

    //        if (shellCheck.CanShellBeEquipped(shellObject) && shellCheck.IsNextBiggest(shellObject))
    //        {
    //            ShellSenseTargetSet?.Invoke(shell.transform);
    //            return;
    //        }
    //    }
    //}


    //public enum Direction
    //{
    //    Left,
    //    Right
    //}

    //public void SelectShell(Direction direction)
    //{
    //    if (direction == Direction.Left)
    //    {
    //        SelectPreviousShell();
    //    }
    //    else
    //    {
    //        SelectNextShell();
    //    }
    //}

    //void UpdateShells()
    //{
    //    looseShells = FindObjectsByType<ShellReference>(FindObjectsSortMode.None);

    //}

    //void SelectNextShell()
    //{

    //}
}
