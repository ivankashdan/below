using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class BestiaryMenu : MonoBehaviour
{
    public static BestiaryMenu Instance { get; private set; }

    public GameObject grid;
    public GameObject selector;

    [Space(10)]
    public TMP_Text title;
    public TMP_Text description;
    public TMP_Text count;

    public Image image;

    public Image downArrow;
    public Image upArrow;

    [Space(10)]
    public int visibleStart;


    BestiarySlot[] bestiarySlots;
    //BestiaryScriptableObject currentReference;
    //int selectedSlot;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        bestiarySlots = grid.GetComponentsInChildren<BestiarySlot>();

        ClearMenu();

        title.text = "No species currently found";
        description.text = "";
        image.enabled = false;

        UpdateCount();
    }
    private void OnEnable()
    {
        BestiaryManager.BestiaryEntryAdded += OnEntryAdded;
    }
    private void OnDisable()
    {
        BestiaryManager.BestiaryEntryAdded -= OnEntryAdded;
    }

    private void Start()
    {
        IncrementVisibleStart(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            IncrementVisibleStart(3);
        }
        else if (Input.GetKeyDown(KeyCode.Minus))
        {
            IncrementVisibleStart(-3);
        }
    }

    public void PageIncrease() => PageChange(3);
    public void PageDecrease() => PageChange(-3);

    void PageChange(int value)
    {
        BestiaryScriptableObject currentReference = BestiaryManager.GetReferenceFromTitle(title.text);
        
        IncrementVisibleStart(value);

        if (IsEntryReferenceVisible(currentReference))
        {
            SelectEntry(currentReference);
        }
        else
        {
            selector.gameObject.SetActive(false);
        }

    }

    public void StepUp() => StepChange(-3);
    public void StepDown() => StepChange(3);

    void StepChange(int value)
    {
        Debug.Log("Step change");

        IncrementVisibleStart(value);

        int selectedSlot = GetSelectedSlot();
        ShowEntry(GetEntry(selectedSlot));
        SelectEntry(selectedSlot);
    }
    
    //void SelectCurrentReference()
    //{
    //    if (currentReference != null)
    //    {
    //        SelectEntry(currentReference);
    //    }
    //}

    void IncrementVisibleStart(int value)
    {
        //get current reference
        int maxIncrease = Mathf.Max(BestiaryManager.GetEntries.Count - bestiarySlots.Length, 0);
        visibleStart = Mathf.Clamp(visibleStart + value, 0, maxIncrease);
        visibleStart = Mathf.CeilToInt(visibleStart / 3.0f) * 3; // Ensure visibleStart is rounded up to the nearest multiple of 3

        UpdateUI();

        Debug.Log($"visibleStart incremented: {visibleStart}");
    }

    void UpdateArrows()
    {
        int maxIncrease = Mathf.Max(BestiaryManager.GetEntries.Count - bestiarySlots.Length, 0);

        upArrow.gameObject.SetActive(false);
        downArrow.gameObject.SetActive(false);

        if (visibleStart > 0)
        {
            upArrow.gameObject.SetActive(true);
        }
        if (BestiaryManager.GetEntries.Count > bestiarySlots.Length
            && visibleStart < maxIncrease)
        {
            downArrow.gameObject.SetActive(true);
        }
    }

    public void ShowEntry(BestiaryScriptableObject reference)
    {
        title.text = reference.title;
        image.enabled = true;
        image.sprite = reference.image;
        description.text = reference.description;
    }

    IEnumerator DelayedSelectRoutine(GameObject gameObject)
    {
        yield return null;
        EventSystem.current.SetSelectedGameObject(gameObject);
        EventSystem.current.firstSelectedGameObject = gameObject;

        Debug.Log("slot selected");
    }

    public void SelectEntry(int slotNumber)
    {
        if (slotNumber >= 0 && slotNumber < bestiarySlots.Length)
        {
            SelectEntry(bestiarySlots[slotNumber].storedReference);
        }
    }

    BestiaryScriptableObject GetEntry(int slotNumber)
    {
        return bestiarySlots[slotNumber].storedReference;
    }


    bool IsEntryReferenceVisible(BestiaryScriptableObject reference)
    {
        int listNumber = BestiaryManager.GetEntryNumber(reference);

        if (listNumber >= 0)
        {
            float visibleMax = visibleStart + bestiarySlots.Length;

            if (listNumber < visibleStart)
            {
                return false;
            }
            else if (listNumber >= visibleMax)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        return false;
    }
    void IncrementVisibleStartToEntry(BestiaryScriptableObject reference)
    {
        int listNumber = BestiaryManager.GetEntryNumber(reference);

        if (listNumber >= 0)
        {
            float visibleMax = visibleStart + bestiarySlots.Length;

            if (listNumber < visibleStart)
            {
                while (listNumber < visibleStart)
                {
                    IncrementVisibleStart(-3);
                }
            }
            else if (listNumber >= visibleMax)
            {
                while (listNumber >= visibleMax)
                {
                    IncrementVisibleStart(3);
                    visibleMax = visibleStart + bestiarySlots.Length; // Update visibleMax after increment
                }
            }
            else
            {
                Debug.LogWarning("List number is out of range.");
            }
        }
    }

    public void SelectEntry(BestiaryScriptableObject reference)
    {
        IncrementVisibleStartToEntry(reference);

        BestiarySlot slot = GetSlot(reference);


        //int slotNumber = GetSlotNumber(reference);


        if (slot != null)
        {
            RectTransform selectorRectTransform = selector.GetComponent<RectTransform>();
            RectTransform slotIconRectTransform = slot.icon.GetComponent<RectTransform>();
            selectorRectTransform.position = slotIconRectTransform.position;

            //selectedSlot = slotNumber;

            StopAllCoroutines();
            StartCoroutine(DelayedSelectRoutine(slot.gameObject));

            selector.gameObject.SetActive(true);

            //if (EventSystem.current.currentSelectedGameObject == null ||
            //    EventSystem.current.currentSelectedGameObject.GetComponent<BestiarySlot>() == false)
            //{
            //    EventSystem.current.SetSelectedGameObject(slot.gameObject);
            //}
            //EventSystem.current.firstSelectedGameObject = slot.gameObject;
        }
        else
        {
            selector.gameObject.SetActive(false);
            //Debug.LogWarning("Slot not found for the given reference.");
        }
    }

   

    void OnEntryAdded(BestiaryScriptableObject reference)
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        UpdateList();
        UpdateCount();
        UpdateArrows();
    }

    void UpdateList()
    {
        ClearMenu();

        List<BestiaryScriptableObject> entries = BestiaryManager.GetEntries;

        if (entries.Count == 0) return;

        int visibleOffsetCount = entries.Count - visibleStart;

        for (int i = 0; i < bestiarySlots.Length; i++)
        {
            Button button = bestiarySlots[i].GetComponent<Button>();

            if (i < visibleOffsetCount)
            {
                int visibleOffset = i + visibleStart;

                if (visibleOffset < entries.Count)
                {
                    button.interactable = true;

                    bestiarySlots[i].storedReference = entries[visibleOffset];
                    bestiarySlots[i].icon.enabled = true;

                    if (entries[visibleOffset].icon != null)
                    {
                        bestiarySlots[i].icon.sprite = entries[visibleOffset].icon;
                    }
                }
                else
                {
                    button.interactable = false;
                    bestiarySlots[i].storedReference = null;
                    bestiarySlots[i].icon.enabled = false;
                }
            }
            else
            {
                button.interactable = false;
                bestiarySlots[i].storedReference = null;
                bestiarySlots[i].icon.enabled = false;
            }
        }
    }

    void ClearMenu()
    {
        foreach (var bestiarySlot in bestiarySlots)
        {
            bestiarySlot.storedReference = null;
            bestiarySlot.icon.enabled = false;

            Button button = bestiarySlot.GetComponent<Button>();
            button.interactable = false;
        }
    }

    void UpdateCount()
    {
        count.text = $"{BestiaryManager.GetTotalFound}/{BestiaryManager.GetTotalToFind} found!";
    }

    BestiarySlot GetSlot(BestiaryScriptableObject reference)
    {
        foreach (var slot in bestiarySlots)
        {
            if (slot.storedReference == reference)
            {
                return slot;
            }
        }
        return null;
    }

    int GetSlotNumber(BestiaryScriptableObject reference)
    {
        for (int i = 0; i < bestiarySlots.Length; i++)
        {
            BestiarySlot slot = bestiarySlots[i];
            if (slot.storedReference == reference)
            {
                return i;
            }
        }
        return -1;
    }

    int GetSelectedSlot()
    {
        BestiaryScriptableObject currentReference = BestiaryManager.GetReferenceFromTitle(title.text);

        return GetSlotNumber(currentReference);
    }

}
