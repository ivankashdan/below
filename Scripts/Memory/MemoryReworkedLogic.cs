using System;
using System.Buffers;
using TMPro;
using UnityEngine;

public class MemoryReworkedLogic : MonoBehaviour
{
    public event Action<MemoryTextModel> MemoryOpened;
    public event Action<MemoryTextModel> MemoryClosed;

    public GameObject textBackground;
    public MemoryTextModel GetCurrentMemory() => currentMemory;
    MemoryTextModel currentMemory;

    MemoryTextComponentManager textComponentManager;
    MemoryTextSelection textSelection;
    MemoryReworkedUI ui;

    private void Awake()
    {
        textComponentManager = GetComponent<MemoryTextComponentManager>();
        textSelection = GetComponent<MemoryTextSelection>();
        ui = GetComponent<MemoryReworkedUI>();
    }

    private void OnEnable()
    {
        MenuManager.MenuClosed += OnMenuClosed;
    }

    private void OnDisable()
    {
        MenuManager.MenuClosed -= OnMenuClosed;
    }

    void OnMenuClosed(MenuManager.Menu menu)
    {
        if (menu == MenuManager.Menu.Memory)
        {
            CloseMemory();
        }
    }

    public void OpenMemory(MemoryTextModel model)
    {
        MenuManager.Instance.OpenMenu(MenuManager.Menu.Memory);
        textBackground.SetActive(true);
        textComponentManager.GenerateBakedText(model.reference.text);
        textSelection.InitiateSelection();
        textSelection.SetManualEffectsOffset(model.reference.effectRange);
        ui.UpdateUI();
        currentMemory = model;
        MemoryOpened?.Invoke(model);
    }

    public void CloseMemory()
    {
        if (currentMemory != null)
        {
            textBackground.SetActive(false);
            currentMemory.CloseMemory();
            MemoryClosed?.Invoke(currentMemory);
            currentMemory = null;
        }
    }

}
