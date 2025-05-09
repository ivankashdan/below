using Unity.Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPauseCameraMovement : MonoBehaviour
{
  
    //public CinemachineVirtualCameraBase followCamera;
    //GameMenu gameMenu;

    private void Start()
    {
        //gameMenu = FindAnyObjectByType<GameMenu>();

        //gameMenu.MenuActivated += ResetCameraYMovement;
    }

    private void OnDisable()
    {
        //gameMenu.MenuActivated -= ResetCameraYMovement;
    }

    void ResetCameraYMovement()
    {
        //followCamera.m_YAxis.Reset();
        Debug.Log("Y axis reset");
    }
        
 


}
