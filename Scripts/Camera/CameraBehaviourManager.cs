using Unity.Cinemachine;
using UnityEngine;

[ExecuteInEditMode]
public class CameraBehaviourManager : MonoBehaviour
{
    public CinemachineVirtualCameraBase followCamera;

    //[Header("Sight check cameras")]
    //public CinemachineVirtualCameraBase topCamera;

    //[Header("Reference cameras")]
    //public CinemachineVirtualCameraBase defaultCam;
    //public CinemachineVirtualCameraBase tunnelCam;

 


    //[Space(10)]
    //public bool copyDefaultOrbits = true;
    //public bool copyDefaultFreelook = true;

    //private void Start()
    //{
    //    if (copyDefaultOrbits) orbitalFollow.Orbits = defaultCam.GetComponent<CinemachineOrbitalFollow>().Orbits;
    //    if (copyDefaultFreelook) freeLookModifier.Modifiers = defaultCam.GetComponent<CinemachineFreeLookModifier>().Modifiers;
    //}


    //float CalculateNewRadiusBasedOnScale(Orbit orbit)
    //{
    //    float baseRadius = GetBaseRadiusForOrbit(orbit);
    //    return baseRadius * (scaleControl.GetStageGFXScale() + 0.2f);
    //}




    //void XRecenter(bool recenter) => orbitalFollow.HorizontalAxis.Recentering.Enabled = recenter;
    //void SetRecenteringTime(float time) => orbitalFollow.HorizontalAxis.Recentering.Time = time;
    //void SetHeadingForward() => orbitalFollow.RecenteringTarget = CinemachineOrbitalFollow.ReferenceFrames.TrackingTarget;
    //void SetHeadingDelta() => orbitalFollow.RecenteringTarget = CinemachineOrbitalFollow.ReferenceFrames.AxisCenter;

    //public void TempXRecenter()
    //{
    //    StartCoroutine(TempXRecenterCoroutine());
    //}


    //IEnumerator TempXRecenterCoroutine() //suspend input and camera control necessary too
    //{
    //    tempXRecenter = true;
    //    SetHeadingDelta();
    //    SetRecenteringTime(0.2f);
    //    XRecenter(true);

    //    yield return new WaitForSeconds(tempXRecenterDuration);

    //    XRecenter(false);
    //    SetHeadingForward();
    //    SetRecenteringTime(0.5f);
    //    tempXRecenter = false;
    //    glide = false;
    //}

    //float GetBaseRadiusForOrbit(Orbit orbit)
    //{
    //    switch (orbit)
    //    {
    //        case Orbit.Top:
    //            return baseTopRadius;
    //        case Orbit.Center:
    //            return baseCenterRadius;
    //        case Orbit.Bottom:
    //            return baseBottomRadius;
    //        default:
    //            throw new ArgumentOutOfRangeException(nameof(orbit), orbit, null);
    //    }
    //}

    //float GetCurrentRadiusForOrbit(Orbit orbit)
    //{
    //    switch (orbit)
    //    {
    //        case Orbit.Top:
    //            return orbitalFollow.Orbits.Top.Radius;
    //        case Orbit.Center:
    //            return orbitalFollow.Orbits.Center.Radius;
    //        case Orbit.Bottom:
    //            return orbitalFollow.Orbits.Bottom.Radius;
    //        default:
    //            throw new ArgumentOutOfRangeException(nameof(orbit), orbit, null);
    //    }
    //}

    //void UpdateOrbitRadius(Orbit orbit, float newRadius)
    //{
    //    switch (orbit)
    //    {
    //        case Orbit.Top:
    //            orbitalFollow.Orbits.Top.Radius = newRadius;
    //            break;
    //        case Orbit.Center:
    //            orbitalFollow.Orbits.Center.Radius = newRadius;
    //            break;
    //        case Orbit.Bottom:
    //            orbitalFollow.Orbits.Bottom.Radius = newRadius;
    //            break;
    //    }
    //}

}


