using TMPro;
using UnityEngine;

public class MemoryReworkedUI : MonoBehaviour
{
    public AnimationCurve typewriterEffectCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    public float fontSizeMultiplier = 1;
    [Header("Color")]
    public Gradient gradient;
    public float textColourOffset = 0f;
    public float colorCutoffAbove = 2;
    public float colorCutoffBelow = 0.5f;

    [Header("Margin")]
    public float aboveMarginMax = 400f;
    public float belowMarginMax = 200f;
    [Header("Height")]
    public float maxHeight = 200;
    public float heightMultiplier = 1.5f;

    float defaultFontSize;
    float defaultHeight;
    float defaultWidth;

    MemoryTextComponentManager textComponentManager;
    MemoryTextSelection textSelection;

    //public float FewerLinesMultiplier = 0.5f;


    private void Awake()
    {
        textComponentManager = GetComponent<MemoryTextComponentManager>();
        textSelection = GetComponent<MemoryTextSelection>();

        var rectTransform = textComponentManager.textPrefab.GetComponent<RectTransform>();
        defaultWidth = rectTransform.sizeDelta.x;
        defaultHeight = rectTransform.sizeDelta.y;

        defaultFontSize = textComponentManager.textPrefab.GetComponent<TMP_Text>().fontSize;
    }
    private void OnEnable()
    {
        textSelection.SelectionChanged += UpdateUI;
    }

    private void OnDisable()
    {
        textSelection.SelectionChanged -= UpdateUI;
    }

    public void UpdateUI()
    {
        UpdateTextPosition();
        UpdateTextColour();
        UpdateTextWidth();
        UpdateTextHeight();
        UpdateTextMargins();
        UpdateTextSize();
    }

    void UpdateTextPosition()
    {
        var rectTransform = textComponentManager.textPanel.GetComponent<RectTransform>();

        if (textComponentManager.BakedTextComponents.Count == 1)
        {
            rectTransform.pivot = new Vector2(rectTransform.pivot.x, 0.5f);
            return;
        }

        
        float yPivot = Mathf.Lerp(1, 0, textSelection.GetPercentageThroughText());

        rectTransform.pivot = new Vector2(rectTransform.pivot.x, yPivot);

    }

    void UpdateTextColour()
    {
        if (textComponentManager.BakedTextComponents.Count == 1)
        {
            textComponentManager.BakedTextComponents[0].color = gradient.Evaluate(0);
            return;
        }

        for (int i = 0; i < textComponentManager.BakedTextComponents.Count; i++)
        {

            float distance = textSelection.GetAdjustedDistanceToSelection(i);

            //if (distance != 0)
            //{
            distance += textColourOffset;

            if (textSelection.IsMidpointAboveSelection(i))
            {
                distance /= colorCutoffAbove;
            }
            else
            {
                distance /= colorCutoffBelow;
            }
            //}

            //float lineCount = (float)textComponentManager.BakedTextComponents.Count;
            //if (lineCount < 10)
            //{
            //    distance *= FewerLinesMultiplier; 
            //}

            textComponentManager.BakedTextComponents[i].color = gradient.Evaluate(distance);
        }
    }

    void UpdateTextSize()
    {
        if (textComponentManager.BakedTextComponents.Count == 1)
        {
            textComponentManager.BakedTextComponents[0].fontSize = defaultFontSize;
            return;
        }

        for (int i = 0; i < textComponentManager.BakedTextComponents.Count; i++)
        {
            float distance = textSelection.GetAdjustedDistanceToSelection(i);
            distance /= fontSizeMultiplier;
            float scaledFontSize = Mathf.Lerp(defaultFontSize, 0, distance);
            textComponentManager.BakedTextComponents[i].fontSize = scaledFontSize;

        }
    }

    void UpdateTextHeight()
    {

        if (textComponentManager.BakedTextComponents.Count == 1)
        {
            var rectTransform = textComponentManager.BakedTextComponents[0].gameObject.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, defaultHeight);
            return;
        }


        for (int i = 0; i < textComponentManager.BakedTextComponents.Count; i++)
        {
            float distance = textSelection.GetAdjustedDistanceToSelection(i);

            distance /= heightMultiplier;

            float scaledHeight = Mathf.Lerp(maxHeight, defaultHeight, distance);

            var rectTransform = textComponentManager.BakedTextComponents[i].gameObject.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, scaledHeight);
        }
    }


    void UpdateTextWidth()
    {
        if (textComponentManager.BakedTextComponents.Count == 1)
        {
            var rectTransform = textComponentManager.BakedTextComponents[0].gameObject.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(defaultWidth, rectTransform.sizeDelta.y);
            return;
        }

        float currentMidpointDistanceToSelection = textSelection.GetAdjustedDistanceToSelection(textSelection.GetSelectedTextComponent);

        for (int i = 0; i < textComponentManager.BakedTextComponents.Count; i++)
        {
          
            
            float distance = textSelection.GetAdjustedDistanceToSelection(i);

            float marginOffset = CalculateMargin(i);

            float width = 0;

            if (textSelection.IsMidpointAboveSelection(i))
            {
                width = defaultWidth;
            }
            else
            {
                width = Mathf.Lerp(defaultWidth, 0, typewriterEffectCurve.Evaluate(distance));
            }

            var rectTransform = textComponentManager.BakedTextComponents[i].gameObject.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(width + marginOffset, rectTransform.sizeDelta.y);
        }
    }

    void UpdateTextMargins()
    {
        if (textComponentManager.BakedTextComponents.Count == 1)
        {
            textComponentManager.BakedTextComponents[0].margin = new Vector4(0, 0, 0, 0);
            return;
        }

        for (int i = 0; i < textComponentManager.BakedTextComponents.Count; i++)
        {
            float margin = CalculateMargin(i);
            textComponentManager.BakedTextComponents[i].margin = new Vector4(margin, 0, 0, 0);
        }
    }

    float CalculateMargin(int textComponentIndex)
    {
        float distance = textSelection.GetAdjustedDistanceToSelection(textComponentIndex);

        float blendFactor = textSelection.IsMidpointAboveSelection(textComponentIndex) ? 1f : 0f;


        //float lineCount = (float)textComponentManager.BakedTextComponents.Count;
        //if (lineCount < 10)
        //{
        //    blendFactor *= FewerLinesMultiplier;
        //}

        return Mathf.Lerp(belowMarginMax, aboveMarginMax, blendFactor) * distance; 


    }

   

  

 
}