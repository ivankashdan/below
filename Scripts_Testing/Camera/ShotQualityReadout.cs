using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class ShotQualityReadout : MonoBehaviour
{
    CinemachineShotQualityEvaluator evaluator;
    CinemachineDeoccluder deoccluder;
    //CinemachineDecollider decollider;

    CinemachineClearShot clearShot;


    public bool occluded;
    public List<float> cameraShotQualities; 

    private void Awake()
    {
        evaluator = GetComponent<CinemachineShotQualityEvaluator>();
        clearShot = GetComponent<CinemachineClearShot>();
        //decollider = GetComponent<CinemachineDecollider>();
    }


    private void Start()
    {
        //cameraShotQualities.clear();

        foreach (var camera in clearShot.ChildCameras)
        {
            cameraShotQualities.Add(camera.State.ShotQuality);
        }
    }


    private void Update()
    {
        for (int i = 0; i < clearShot.ChildCameras.Count; i++) 
        {
            CinemachineVirtualCameraBase camera = clearShot.ChildCameras[i];

            OcclusionChecker occlusionChecker = camera.GetComponent<OcclusionChecker>();

            occluded = occlusionChecker.IsOccluded;

            //CinemachineDecollider decollider = camera.GetComponent<CinemachineDecollider>();

            //if (decollider.IsTarget

            cameraShotQualities[i] = camera.State.ShotQuality;
        }
    }


        




}
