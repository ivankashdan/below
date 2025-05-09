using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public enum Orbit
{
    Top,
    Center,
    Bottom
}

public class OrbitsBehaviour : MonoBehaviour
{
    [Header("Orbit Radius Transition")]
    public float defaultTransitionDuration = 0.5f;
    public AnimationCurve zoomTransitionCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    float baseHeight;
    public float GetBaseHeight() => baseHeight;

    CameraBehaviourManager cameraManager;
    CinemachineOrbitalFollow orbitalFollow;

    public OrbitsReference defaultOrbit;

    private void Awake()
    {
        cameraManager = GetComponentInParent<CameraBehaviourManager>();
        orbitalFollow = cameraManager.followCamera.GetComponent<CinemachineOrbitalFollow>();

        baseHeight = orbitalFollow.Orbits.Center.Height;  

    }

    public void StartHeightTransition(float targetHeight)
    {
        float currentRadius = GetCurrentOrbit(Orbit.Center).Radius;

        Cinemachine3OrbitRig.Settings settings = new Cinemachine3OrbitRig.Settings()
        {
            Top = new Cinemachine3OrbitRig.Orbit() { Height = currentRadius, Radius = targetHeight },
            Center = new Cinemachine3OrbitRig.Orbit() { Height = targetHeight, Radius = currentRadius },
            Bottom = new Cinemachine3OrbitRig.Orbit() { Height = targetHeight, Radius = currentRadius }
        };

        StartOrbitTransition(settings);
    }

    public void StartRadiusTransition(float targetRadius)
    {
        float currentHeight = GetCurrentOrbit(Orbit.Center).Height;

        Cinemachine3OrbitRig.Settings settings = new Cinemachine3OrbitRig.Settings()
        {
            Top = new Cinemachine3OrbitRig.Orbit() { Height = targetRadius, Radius = currentHeight },
            Center = new Cinemachine3OrbitRig.Orbit() { Height = currentHeight, Radius = targetRadius },
            Bottom = new Cinemachine3OrbitRig.Orbit() { Height = currentHeight, Radius = targetRadius }
        };

        StartOrbitTransition(settings);
    }

    public void StartOrbitTransitionToDefault(float duration = 1f)
    {
        StartOrbitTransition(defaultOrbit.storedOrbits);
    }

    public void StartOrbitTransition(Cinemachine3OrbitRig.Settings settings, float duration = 1f)
    {
        StopAllCoroutines();
        StartCoroutine(TransitionOrbitsCoroutine(settings, duration));
    }

    IEnumerator TransitionOrbitsCoroutine(Cinemachine3OrbitRig.Settings settings, float duration)
    {
        Coroutine topCoroutine = StartCoroutine(TransitionOrbitCoroutine(Orbit.Top,  settings.Top, duration));
        Coroutine centerCoroutine = StartCoroutine(TransitionOrbitCoroutine(Orbit.Center, settings.Center, duration));
        Coroutine bottomCoroutine = StartCoroutine(TransitionOrbitCoroutine(Orbit.Bottom, settings.Bottom, duration));

        yield return topCoroutine;
        yield return centerCoroutine;
        yield return bottomCoroutine;
    }

    IEnumerator TransitionOrbitCoroutine(Orbit orbit, Cinemachine3OrbitRig.Orbit targetOrbit, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = zoomTransitionCurve.Evaluate(elapsedTime / duration);

            Cinemachine3OrbitRig.Orbit lerpedOrbit = new Cinemachine3OrbitRig.Orbit()
            {
                Radius = Mathf.Lerp(GetCurrentOrbit(orbit).Radius, targetOrbit.Radius, t),
                Height = Mathf.Lerp(GetCurrentOrbit(orbit).Height, targetOrbit.Height, t)
            };

            UpdateActualOrbit(orbit, lerpedOrbit);
            yield return null;
        }

        // Ensure the final position is set
        //UpdateActualOrbit(orbit, targetOrbit);
        Debug.Log($" {orbit} updated: height ({targetOrbit.Height}), radius ({targetOrbit.Radius})");
    }

    public Cinemachine3OrbitRig.Orbit GetCurrentOrbit(Orbit orbit)
    {
        switch (orbit)
        {
            case Orbit.Top:
                return orbitalFollow.Orbits.Top;
            case Orbit.Center:
                return orbitalFollow.Orbits.Center;
            case Orbit.Bottom:
                return orbitalFollow.Orbits.Bottom;
            default:
                throw new ArgumentOutOfRangeException(nameof(orbit), orbit, null);
        }
    }

    void UpdateActualOrbit(Orbit orbit, Cinemachine3OrbitRig.Orbit newOrbit)
    {
        switch (orbit)
        {
            case Orbit.Top:
                orbitalFollow.Orbits.Top = newOrbit;
                break;
            case Orbit.Center:
                orbitalFollow.Orbits.Center = newOrbit;
                break;
            case Orbit.Bottom:
                orbitalFollow.Orbits.Bottom = newOrbit;
                break;
        }
    }
}
