using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BestiarySlot : MonoBehaviour
{
    static public event Action<BestiarySlot> SlotSelected;

    [HideInInspector] public BestiaryScriptableObject storedReference;
    public Image icon;

    public void SelectEntry()
    {
        if (storedReference != null)
        {
            BestiarySelection.Instance.ShowAndSelectEntry(storedReference);//

            SlotSelected?.Invoke(this);
        }
    }

    //public void OnDeselect()
    //{
    //    if (EventSystem.current.currentSelectedGameObject == null)
    //    {
    //        SelectEntry();
    //    }
    //}
}
