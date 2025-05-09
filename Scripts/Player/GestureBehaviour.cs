using System;
using System.Collections.Generic;
using UnityEngine;

public class GestureBehaviour : MonoBehaviour
{
    public event Action<bool> GesturablesChange;

    List<GameObject> gesturables = new();

    public bool IsGesturableWithinRange() => gesturables.Count > 0; 
 

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Urchin")
        {
            gesturables.Add(other.gameObject);
            TriggerEvent();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Urchin")
        {
            gesturables.Remove(other.gameObject);
            TriggerEvent();
        }


    }

    void TriggerEvent()
    {
        GesturablesChange?.Invoke(IsGesturableWithinRange());
    }




}
