using UnityEngine;

public class SenseZoom : MonoBehaviour
{
    public float senseFOV = 40;

    FocalZoom focalZoom;
    ShellSense shellSense;

    private void Awake()
    {
        shellSense = FindAnyObjectByType<ShellSense>();
        focalZoom = GetComponent<FocalZoom>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            OnSenseActivated();
        }
        else if (Input.GetKeyUp(KeyCode.Z))
        {
            OnSenseDeactivated();
        }
    }

    //private void OnEnable()
    //{
    //    shellSense.SenseActivated += OnSenseActivated;
    //    shellSense.SenseDeactivated += OnSenseDeactivated;
    //}

    //private void OnDisable()
    //{
    //    shellSense.SenseActivated -= OnSenseActivated;
    //    shellSense.SenseDeactivated -= OnSenseDeactivated;
    //}

    void OnSenseActivated()
    {
        focalZoom.SetFOV(senseFOV);
    }

    void OnSenseDeactivated()
    {
        focalZoom.SetFOVToDefault();
    }



}
