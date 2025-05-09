using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class ScaleChangeBehaviour : MonoBehaviour
{
    //public float scaleChangeDelay = 0.5f;

    //Cinemachine3OrbitRig.Orbit baseTop = new Cinemachine3OrbitRig.Orbit();
    //Cinemachine3OrbitRig.Orbit baseCenter = new Cinemachine3OrbitRig.Orbit();
    //Cinemachine3OrbitRig.Orbit baseBottom = new Cinemachine3OrbitRig.Orbit();

    //ScaleControl scaleControl;
    //CameraBehaviourManager cameraManager;
    //OrbitsBehaviour orbitsBehaviour;
    //CinemachineOrbitalFollow orbitalFollow;

    //private void Awake()
    //{
    //    scaleControl = FindAnyObjectByType<ScaleControl>();
    //    orbitsBehaviour = FindAnyObjectByType<OrbitsBehaviour>();

    //    cameraManager = GetComponentInParent<CameraBehaviourManager>();
    //    orbitalFollow = cameraManager.followCamera.GetComponent<CinemachineOrbitalFollow>(); 

    //    baseTop = orbitalFollow.Orbits.Top;
    //    baseCenter = orbitalFollow.Orbits.Center;
    //    baseBottom = orbitalFollow.Orbits.Bottom;
    //}

    //private void OnEnable()
    //{
    //    scaleControl.ScaleUpdated += OnScaleUpdated;
    //}

    //private void OnDisable()
    //{

    //    scaleControl.ScaleUpdated -= OnScaleUpdated;
    //}

    ////private void Start()
    ////{
    ////    cameraManager.UpdateOrbitsBasedOnScale();
    ////}

    //void OnScaleUpdated()
    //{
    //    UpdateOrbitsBasedOnScale();
    //}

    //public void UpdateOrbitsBasedOnScale()
    //{
    //    Debug.Log("Update Orbits Based On Scale called");
    //    orbitsBehaviour.StopOrbitsCoroutine();

    //    StartCoroutine(UpdateOrbitsBasedOnScaleCoroutine());
    //}

    //IEnumerator UpdateOrbitsBasedOnScaleCoroutine()
    //{
    //    yield return new WaitForSeconds(scaleChangeDelay);

    //    orbitsBehaviour.StartOrbitTransition(Orbit.Top, CalculateNewOrbitBasedOnScale(Orbit.Top));
    //    orbitsBehaviour.StartOrbitTransition(Orbit.Center, CalculateNewOrbitBasedOnScale(Orbit.Center));
    //    orbitsBehaviour.StartOrbitTransition(Orbit.Bottom, CalculateNewOrbitBasedOnScale(Orbit.Bottom));
    //}
    //Cinemachine3OrbitRig.Orbit CalculateNewOrbitBasedOnScale(Orbit orbit)
    //{
    //    float offset = 0f;

    //    Cinemachine3OrbitRig.Orbit newOrbit = new Cinemachine3OrbitRig.Orbit
    //    {
    //        Radius = GetBaseForOrbit(orbit).Radius * (scaleControl.GetStageGFXScale() + offset),
    //        Height = GetBaseForOrbit(orbit).Height * (scaleControl.GetStageGFXScale() + offset)
    //    };

    //    return newOrbit;
    //}

    //Cinemachine3OrbitRig.Orbit GetBaseForOrbit(Orbit orbit)
    //{
    //    switch (orbit)
    //    {
    //        case Orbit.Top:
    //            return baseTop;
    //        case Orbit.Center:
    //            return baseCenter;
    //        case Orbit.Bottom:
    //            return baseBottom;
    //        default:
    //            throw new ArgumentOutOfRangeException(nameof(orbit), orbit, null);
    //    }
    //}

   



}
