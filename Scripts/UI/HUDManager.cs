using UnityEngine;
using UnityEngine.Timeline;

public class HUDManager : MonoBehaviour
{
    public GameObject hudObject;

    public bool startVisible = true;

    bool visible;
    public bool IsVisible => visible;

    CutsceneManager cutsceneManager;
    CardManager cardManager;
    ShellSense shellSense;

    private void Awake()
    {
        cutsceneManager = FindAnyObjectByType<CutsceneManager>();
        cardManager = FindAnyObjectByType<CardManager>();
        shellSense = FindAnyObjectByType<ShellSense>();

        if (startVisible)
        {
            ShowHUD();
        }
        else
        {
            HideHUD();
        }
    }

    private void OnEnable()
    {
        MenuManager.MenuOpened += OnMenuOpened;
        MenuManager.MenuBaseDeactivated += OnMenuClosed;

        cutsceneManager.CutsceneStarted += OnCutsceneStarted;
        cutsceneManager.CutsceneEnded += OnCutsceneEnded;

        cardManager.CardShown += OnCardShown;
        cardManager.CardHidden += ShowHUD;

        shellSense.SenseActivated += HideHUD;
        shellSense.SenseDeactivated += ShowHUD;
    }
    private void OnDisable()
    {
        MenuManager.MenuOpened -= OnMenuOpened;
        MenuManager.MenuBaseDeactivated -= OnMenuClosed;

        cutsceneManager.CutsceneStarted -= OnCutsceneStarted;
        cutsceneManager.CutsceneEnded -= OnCutsceneEnded;

        cardManager.CardShown -= OnCardShown;
        cardManager.CardHidden -= ShowHUD;

        shellSense.SenseActivated -= HideHUD;
        shellSense.SenseDeactivated -= ShowHUD;
    }



 

    void OnCutsceneStarted(TimelineAsset playable)
    {
        HideHUD();
    }
    void OnCutsceneEnded(TimelineAsset playable)
    {
        ShowHUD();
    }

    void OnMenuOpened(MenuManager.Menu menu)
    {
        HideHUD();
    }

    void OnMenuClosed()
    {
        if (cutsceneManager.IsCutscenePlaying() == false && cardManager.isCardShown == false)
        {
            ShowHUD();
        }
    }

    void OnCardShown(GameObject card)
    {
        HideHUD();
    }

    void HideHUD() => SetHUD(false);
    void ShowHUD() => SetHUD(true);

    void SetHUD(bool value)
    {
        hudObject.SetActive(value);
        visible = value;

    }


}
