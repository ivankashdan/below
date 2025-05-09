using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityTrigger : MonoBehaviour, IControllerSwitch, ITutorialPrompt
{
    //public bool IsUIEmpty() => image.sprite == controlSchemeUIManager.empty || image.sprite == null;


    public string abilityAcquiredText = "glide <color=#D1F1FF>ability acquired";

    public TMP_Text holdButtonText;
    public Image button;

    bool active;
    public bool IsActive() => active;


    public void UpdateOnControllerSwitch()
    {
        //if (!IsUIEmpty())
        //{
        if (active)
        {
            if (ControllerSwitchBehaviour.Instance.IsSchemeActive(ControlScheme.Keyboard))
            {
                holdButtonText.enabled = false;
            }
            else if (ControllerSwitchBehaviour.Instance.IsSchemeActive(ControlScheme.Xbox360))
            {
                holdButtonText.enabled = true;
            }
        }
      
        button.sprite = controlSchemeUIManager.currentSprites.glide;
        //}
    }

    //public Image image;

    //public Sprite spriteOn;
    //public Sprite spriteOff;
    //public Sprite empty;

    Coroutine timerCoroutine;

    ShellCheck shellCheck;
    ShellEquip shellEquip;
    ControlSchemeUIManager controlSchemeUIManager;
    CentralUIManager centralUIManager;

    public float delay = 4f;

    //GameObject lastShell;
    bool skippedFirstEquip;

    enum ImageType
    {
        On,
        Off
    }

    private void Awake()
    {
        shellCheck = FindAnyObjectByType<ShellCheck>();
        shellEquip = FindAnyObjectByType<ShellEquip>();
        controlSchemeUIManager = FindAnyObjectByType<ControlSchemeUIManager>();
        centralUIManager = GetComponent<CentralUIManager>();

    }

    private void OnEnable()
    {
        shellEquip.ShellChanged += OnShellChange;
    }

    private void OnDisable()
    {
        shellEquip.ShellChanged -= OnShellChange;
    }
    private void Start()
    {
        UpdateOnControllerSwitch();
        ClearImage();
        
        //MessageType(ImageType.None);//
        //StartCoroutine(InitiateDelayCoroutine());
    }

    void OnShellChange()
    {
        if (!skippedFirstEquip)
        {
            skippedFirstEquip = true;
            return;
        }

        if (shellCheck.DoesShellHaveGlideAbility()) //will need to expand with more modifiers
        {
            MessageType(ImageType.On);
        }
        else
        {
            MessageType(ImageType.Off);
        }
    }

    

    void MessageType(ImageType type)
    {
        switch (type)
        {
            case ImageType.On:
                centralUIManager.SetActive(true);
                centralUIManager.SetCentralText(abilityAcquiredText);
                centralUIManager.FormatCentralText(FontStyles.Normal);

                active = true;
                //abilityObject.SetActive(true);

                //abilityAcquiredText.enabled = true;
                //abilityAcquiredText.fontStyle = FontStyles.Normal;

                holdButtonText.enabled = ControllerSwitchBehaviour.Instance.IsSchemeActive(ControlScheme.Xbox360);
                button.enabled = true;

                StartClearTimerCoroutine();
                break;
            case ImageType.Off:
                centralUIManager.SetActive(true);
                centralUIManager.SetCentralText(abilityAcquiredText);
                centralUIManager.FormatCentralText(FontStyles.Strikethrough);

                active = true;
                //abilityObject.SetActive(true);

                //abilityAcquiredText.enabled = true;
                //abilityAcquiredText.fontStyle = FontStyles.Strikethrough;

                holdButtonText.enabled = false;
                button.enabled = false;

                StartClearTimerCoroutine();
                break;
        }
    }

    void ClearImage()
    {
        centralUIManager.SetActive(false);

        holdButtonText.enabled = false;
        button.enabled = false;

        active = false;
        //abilityObject.SetActive(false);
    }

    void StartClearTimerCoroutine()
    {
        if (timerCoroutine != null) StopCoroutine(timerCoroutine);
        timerCoroutine = StartCoroutine(TimerCoroutine());
    }

    IEnumerator TimerCoroutine()
    {
        yield return new WaitForSeconds(delay);
        ClearImage();
    }

   

    

    //IEnumerator InitiateDelayCoroutine()
    //{
    //    yield return new WaitForSeconds(2);
    //}



    //private void Update()
    //{
    //    bool shellChanged = CheckShellChange();

    //    if (initiated) 
    //    {
    //        if (shellChanged)
    //        {
    //            if (shellCheck.DoesShellHaveGlideAbility())
    //            {
    //                MessageType(ImageType.On);
    //            }
    //            else if (!shellCheck.IsAShellEquipped())
    //            {
    //                MessageType(ImageType.None);
    //            }
    //            else
    //            {
    //                MessageType(ImageType.Off);
    //            }
    //        }
    //    }
    //}

    //bool CheckShellChange()
    //{
    //    bool shellChanged = false;

    //    GameObject shell = shellCheck.GetShell();
    //    if (shell != lastShell)
    //    {
    //        shellChanged = true;
    //    }
    //    lastShell = shell;

    //    return shellChanged;
    //}

}
