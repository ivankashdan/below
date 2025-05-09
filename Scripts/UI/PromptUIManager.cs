using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static TutorialMessages;

public class PromptUIManager :  MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeSpeed = 1f;
    public Image image;
    public TMP_Text text;

    LayoutElement imageLayoutElement;

    Coroutine fadeCoroutine;
    bool hidden = true;

    IPromptUI visiblePrompt;

    public bool IsTutorialUIVisible() => visiblePrompt == tutorialUI as IPromptUI;
  

    TargetUI targetUI;
    TutorialUI tutorialUI;

    ControlSchemeUIManager controlSchemeUIManager;
    HUDManager hudManager;

    private void Awake()
    {
        imageLayoutElement = image.GetComponent<LayoutElement>();

        targetUI = FindAnyObjectByType<TargetUI>();
        tutorialUI = FindAnyObjectByType<TutorialUI>();

        controlSchemeUIManager = FindAnyObjectByType<ControlSchemeUIManager>();
        hudManager = FindAnyObjectByType<HUDManager>();
    }


    private void Update()
    {
        if (hudManager.IsVisible)
        {
            UpdateUI();
        }
    }

    //private void OnEnable()
    //{
    //    targetUI.TargetActive += UpdateUI;
    //    targetUI.TargetInactive += UpdateUI;

    //    tutorialUI.TargetActive += UpdateUI;
    //    tutorialUI.TargetInactive += UpdateUI;

    //}

    //private void OnDisable()
    //{
    //    targetUI.TargetActive -= UpdateUI;
    //    targetUI.TargetInactive -= UpdateUI;

    //    tutorialUI.TargetActive -= UpdateUI;
    //    tutorialUI.TargetInactive -= UpdateUI;
    //}

    void UpdateUI()
    {
        if (targetUI.IsActive())
        {
            SetPromptUI(targetUI);
        }
        else if (tutorialUI.IsActive())
        {
            SetPromptUI(tutorialUI);
        }
        else
        {
            ClearPromptUI();
        }
    }


 

    void SetPromptUI(IPromptUI promptUI)
    {
        image.sprite = promptUI.GetSprite();
        text.text = promptUI.GetText();
        visiblePrompt = promptUI;

        ResizeUI(image);


        //if (hidden && fadeCoroutine == null)
        //{
        //    StopAllCoroutines();
        //    fadeCoroutine = StartCoroutine(FadeInRoutine(promptUI.GetSprite(), promptUI.GetText()));
        //}
    }

    void ClearPromptUI()
    {
        image.sprite = controlSchemeUIManager.empty;
        text.text = "";

        visiblePrompt = null;

        //if (!hidden && fadeCoroutine == null)
        //{
        //    StopAllCoroutines();
        //    fadeCoroutine = StartCoroutine(FadeOutRoutine());
        //}

    }

    public void ResizeUI(Image image, float largeHeight = 120f, float smallHeight = 85f)
    {
        if (image.gameObject.activeSelf)
        {
            float originalWidth = image.sprite.rect.width;
            float originalHeight = image.sprite.rect.height;

            LayoutElement layoutElement = image.GetComponent<LayoutElement>();

            if (originalHeight > largeHeight)
            {
                layoutElement.preferredHeight = largeHeight;
            }
            else
            {
                layoutElement.preferredHeight = smallHeight;
            }

            float newWidth = (originalWidth / originalHeight) * layoutElement.preferredHeight;
            layoutElement.preferredWidth = newWidth;

            CanvasGroup imageCanvasGroup = image.GetComponentInParent<CanvasGroup>();
            LayoutRebuilder.ForceRebuildLayoutImmediate(imageCanvasGroup.GetComponent<RectTransform>());
        }
        
    }

    IEnumerator FadeInRoutine(Sprite sprite, string text)
    {
        yield return new WaitForEndOfFrame();

        image.sprite = sprite;
        this.text.text = text;

        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Mathf.Clamp01(canvasGroup.alpha + fadeSpeed * Time.deltaTime);
            yield return null;
        }
        hidden = false;
        fadeCoroutine = null;
    }

    IEnumerator FadeOutRoutine()
    {
        yield return new WaitForEndOfFrame();

        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Mathf.Clamp01(canvasGroup.alpha + fadeSpeed * Time.deltaTime);
            yield return null;
        }
        hidden = true;
        fadeCoroutine = null;
    }


    //additionalActive = memoryUI.IsActive();  //neaten these up
    //imageAdditional.enabled = additionalActive;
    //textAdditional.enabled = additionalActive;

    //if (memoryUI.IsActive())
    //{
    //    image.sprite = memoryUI.spriteFirst;
    //    text.text = memoryUI.textFirst;

    //    imageAdditional.sprite = memoryUI.spriteSecond;
    //    textAdditional.text = memoryUI.textSecond;
    //}
    //else if (urchinUI.IsActive() && showGesture) //not used currently
    //{
    //    image.sprite = urchinUI.currentSprite;
    //    //text.text = urchinUI.currentText;
    //}


}
