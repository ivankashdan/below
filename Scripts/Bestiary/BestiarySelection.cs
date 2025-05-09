using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;


enum Direction { Up, Down, Left, Right }

public class BestiarySelection : MonoBehaviour
{

    public static BestiarySelection Instance { get; private set; }

    //public bool readyToSelect;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void OnEnable()
    {
        MenuManager.MenuOpened += OnMenuOpen;
        //MenuManager.MenusClosed += OnMenusClosed;
        //BestiaryMenu.Instance.MenuReadyForSelection += OnMenuReadyForSelection;
    }

    private void OnDisable()
    {
        MenuManager.MenuOpened -= OnMenuOpen;
        //MenuManager.MenusClosed -= OnMenusClosed;
        //BestiaryMenu.Instance.MenuReadyForSelection -= OnMenuReadyForSelection;
    }

    //void OnMenuOpen(MenuManager.Menu menu)
    //{
    //    readyToSelect = false;
    //}

    //void OnMenuReadyForSelection()
    //{
    //    readyToSelect = true;
    //    Debug.Log("Menu ready for selection");
    //    ShowMostRecentEntry();
    //}

    void OnMenuOpen(MenuManager.Menu menu)
    {
        if (menu == MenuManager.Menu.Bestiary)
        {
            ShowMostRecentEntry();
        }
    }

    void ShowMostRecentEntry()
    {
        var mostRecentEntry = BestiaryManager.GetMostRecentEntry();

        if (mostRecentEntry != null)
        {
            ShowAndSelectEntry(mostRecentEntry);
        }
    }

    public void ShowAndSelectEntry(BestiaryScriptableObject selectedEntry)
    {
        BestiaryMenu.Instance.ShowEntry(selectedEntry);
        BestiaryMenu.Instance.SelectEntry(selectedEntry);
    }

    //public void SelectEntry(Direction direction)
    //{
    //    BestiaryMenu.Instance.SelectEntry(direction);
    //}

    //void OnMenusClosed()
    //{

    //}













    //private void Update()
    //{
    //    Vector2 navigate = GameInput.controls.Menu.Navigate.ReadValue<Vector2>();

    //    if (navigate.y > 0)
    //    {
    //        SelectPreviousEntry();

    //    }
    //    else if (navigate.y < 0)
    //    {
    //        SelectNextEntry();
    //    }
    //}


    //GameObject buttonGameObject = BestiaryMenu.Instance.GetButtonGameObject(selectedEntry);
    //if (buttonGameObject != null)
    //{
    //    BestiaryMenu.Instance.ShowEntry(

    //        ShowEntry(buttonGameObject);
    //}
    //else
    //{
    //    throw new Exception("No entry to be selected");
    //}


    //void ShowEntry(GameObject buttonGameObject)
    //{



    //    //if (buttonGameObject.GetComponent<BestiaryEntry>() && buttonGameObject.GetComponent<Button>())
    //    //{
    //    //    EventSystem.current.SetSelectedGameObject(buttonGameObject);
    //    //    BestiaryEntry buttonBestiaryEntry = buttonGameObject.GetComponent<BestiaryEntry>();

    //    //    //EntryShown?.Invoke(buttonBestiaryEntry.reference);
    //    //    //Debug.Log(buttonBestiaryEntry.reference + " selected in Bestiary");
    //    //}
    //    //throw new Exception("Selected entry not viable, most likely not updated yet");
    //}





    //int GetSelectedEntryNumber()
    //{
    //    for (int i = 0; i < listContent.transform.childCount; i++)
    //    {
    //        Transform buttonTransform = listContent.transform.GetChild(i);

    //        if (EventSystem.current.currentSelectedGameObject == buttonTransform)
    //        {
    //            return i;
    //        }
    //    }
    //    Debug.LogError("No entry selected");
    //    return 0;
    //}


    //



    //GameObject GetSelectedEntry()
    //{
    //    if (EventSystem.current.currentSelectedGameObject != null)
    //    {
    //        foreach (Transform buttonTransform in listContent.transform)
    //        {
    //            if (EventSystem.current.currentSelectedGameObject == buttonTransform.gameObject)
    //            {
    //                return buttonTransform.gameObject;
    //            }
    //        }
    //    }
    //    Debug.LogError("No entry selected");
    //    return null;
    //}

    //void SelectNextEntry() => IncrementSelectedEntry(1);
    //void SelectPreviousEntry() => IncrementSelectedEntry(-1);
    //void IncrementSelectedEntry(int value)
    //{
    //    int selectedEntryNumber = GetSelectedEntryNumber();
    //    int incrementedEntryNumber = selectedEntryNumber + value;

    //    if (incrementedEntryNumber >= 0 && incrementedEntryNumber <= listContent.transform.childCount)
    //    {
    //        GameObject incrementedEntry = listContent.transform.GetChild(incrementedEntryNumber).gameObject;
    //        SelectEntry(incrementedEntry);
    //    }
    //}



    //void ScrollToSelectedEntry()
    //{

    //}



}
