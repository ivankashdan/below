using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BestiaryUINotification : MonoBehaviour, IControllerSwitch
{
    public static BestiaryUINotification Instance { get; private set; }

    public void UpdateOnControllerSwitch()
    {
        promptIcon.sprite = controlSchemeUIManager.currentSprites.bestiary;
    }

    public bool IsUIEmpty() => isVisible;

    public GameObject notification;
    public TMP_Text entryText;
    public Image promptIcon;

    public float clearDelay = 5f;
    [Space(10)]
    public bool hiddenStart = true;
    public float hiddenStartDelay = 0.5f;

    
    bool hidden;
    bool isVisible;

    public bool IsVisible => isVisible;

    Coroutine delayedClearCoroutine;

    ControlSchemeUIManager controlSchemeUIManager;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        controlSchemeUIManager = FindAnyObjectByType<ControlSchemeUIManager>();

        notification.SetActive(false);
        //entryText.text = "";

        if (hiddenStart) StartCoroutine(HiddenStartCoroutine());
    }

    private void OnEnable()
    {
        BestiaryManager.BestiaryEntryAdded += OnEntryAdded;
    }

    private void OnDisable()
    {
        BestiaryManager.BestiaryEntryAdded -= OnEntryAdded;
    }

    void OnEntryAdded(BestiaryScriptableObject reference)
    {
        if (!hidden)
        {
            if (delayedClearCoroutine != null) StopCoroutine(delayedClearCoroutine);
            delayedClearCoroutine = StartCoroutine(DelayedClearCoroutine(reference.title));
        }
    }

    public void ClearNotification()
    {
        if (delayedClearCoroutine != null)
        {
            StopCoroutine(delayedClearCoroutine);

            notification.SetActive(false);
            //entryText.text = "";

            isVisible = false;
        }
    }

 

    IEnumerator DelayedClearCoroutine(string title)
    {
        isVisible = true;
        entryText.text = title;
        notification.SetActive(true);
        //$"\n" + $"{BestiaryManager.GetTotalFound} / {BestiaryManager.GetTotalToFind} [Esc] ";

        yield return new WaitForSeconds(clearDelay);

        notification.SetActive(false);
       //entryText.text = "";

        isVisible = false;
    }

    IEnumerator HiddenStartCoroutine()
    {
        hidden = true;

        yield return new WaitForSeconds(hiddenStartDelay);

        hidden = false;
    }

 
}
