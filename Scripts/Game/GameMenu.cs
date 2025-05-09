using System;
using System.Collections;
using UnityEngine;

public class GameMenu : MonoBehaviour
{
    //public event Action MenuActivated;

    public static GameMenu Instance { get; private set; }

    public GameObject menu;

    public GameObject mainImage;
    public GameObject controlsImage;

    bool menuIsVisible;
    bool controlVisible;
    bool menuJustClosed;
 
    //public bool IsMenuActive() => menuIsVisible || menuJustClosed;
    //public bool IsMenuVisible() => menuIsVisible;
    //bool IsMenuJustClosed() => menuJustClosed;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

    }
    //void Update()
    //{
    //    if (menuIsVisible)
    //    {
    //        if (controlVisible)
    //        {
    //            if (GamepadInput.controls.Menu.Back.WasPressedThisFrame())
    //            {
    //                //ControlsExit();
    //            }
    //        }
    //        else
    //        {
    //            if (GamepadInput.controls.Menu.Back.WasPressedThisFrame())
    //            {
    //                //MainMenu(false);
    //            }
    //        }

    //    }


    //}

    
    //public void Continue()
    //{
    //    if (menuIsVisible)
    //    {
    //        MainMenu(false);
    //    }
    //}

    //public void Controls()
    //{
    //    Debug.Log("Controls activated");
    //    mainImage.SetActive(false);
    //    controlsImage.SetActive(true);
    //    controlVisible = true;
    //}

    //public void ControlsExit()
    //{
    //    Debug.Log("Controls deactivated");
    //    mainImage.SetActive(true);
    //    controlsImage.SetActive(false);
    //    controlVisible = false;
    //}

    IEnumerator InputDelayCoroutine()
    {
        menuJustClosed = true;
        yield return new WaitForSeconds(0.1f);
        menuJustClosed = false;
    }


}
