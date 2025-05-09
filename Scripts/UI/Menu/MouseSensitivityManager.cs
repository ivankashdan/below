using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Processors;

public class MouseSensitivityManager : MonoBehaviour
{
    CameraBehaviourManager newCameraBehaviour;
    PlayerInput playerInput;

    public float ySensitivityDefault = 1.2f;
    public float ySensitivityKeyboardFar = 0.8f;
    public float ySensitivityKeyboardClose = 0.4f;

    private void Start()
    {
        newCameraBehaviour = FindAnyObjectByType<CameraBehaviourManager>();
        playerInput = FindAnyObjectByType<PlayerInput>();
    }

    private void Update()
    {
        //if (Time.timeScale != 0)
        //{
        //    if (playerInput.currentControlScheme == "Keyboard")
        //    {
        //        if (newCameraBehaviour.IsPlayerZoomed())
        //            SetYSensitivity(ySensitivityKeyboardClose);
        //        else
        //            SetYSensitivity(ySensitivityKeyboardFar);
        //    }
        //    else
        //    {
        //        SetYSensitivity(ySensitivityDefault);
        //    }
        //}
        //else
        //{
        //    SetYSensitivity(0);
        //}
   
    }

    //void SetYSensitivity(float value) => newCameraBehaviour.followCamera.m_YAxis.m_MaxSpeed = value;
}
