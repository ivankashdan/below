using System;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class MemoryTextComponentManager : MonoBehaviour
{
    public event Action TextGenerated; //not used currently
    bool generated;
    public bool IsGenerated => generated;

    public GameObject textPanel;
    public GameObject textPrefab;
    public char lineBreak = '>';
    public char spaceBreak = '/';

    const int MaxTextPrefabs = 100;

    [HideInInspector] public List<TMP_Text> BakedTextComponents = new List<TMP_Text>();
    [HideInInspector] public List<int> BakedTextCharCount = new List<int>();

    public void GenerateBakedText(string text)
    {
        ClearExistingTextComponents();

        string remainingText = text;

        for (int i = 0; i < MaxTextPrefabs && !string.IsNullOrEmpty(remainingText); i++)
        {
            TMP_Text textComponent = InstantiateTextObject();
            textComponent.text = remainingText;

            Canvas.ForceUpdateCanvases();

            int lastVisibleCharIndex = textComponent.textInfo.characterCount - 1;
            if (lastVisibleCharIndex < 0)
            {
                break;
            }

            string actualVisibleText = textComponent.text.Substring(0, textComponent.textInfo.characterInfo[lastVisibleCharIndex].index + 1);
            Debug.Log(actualVisibleText);
            textComponent.textWrappingMode = TextWrappingModes.NoWrap;  //for height effect

            int visibleCutIndex = 0;
            int remainingCutIndex = 0;

            if (actualVisibleText.Contains(spaceBreak))
            {
                visibleCutIndex = FindFirstCharacterIndex(spaceBreak, remainingText);
                remainingCutIndex = visibleCutIndex + 1;
                InstantiateSpacer();
            }
            else if (actualVisibleText.Contains(lineBreak))
            {
                visibleCutIndex = FindFirstCharacterIndex(lineBreak, remainingText);
                remainingCutIndex = visibleCutIndex + 1;
            }
            else if (remainingText.Length <= textComponent.textInfo.characterCount)
            {
                break;
            }
            else
            {
                visibleCutIndex = FindLastSpaceIndex(actualVisibleText);
                remainingCutIndex = visibleCutIndex;
            }

            string visibleText = remainingText.Substring(0, visibleCutIndex).TrimEnd();
            remainingText = remainingText.Substring(remainingCutIndex).TrimStart();

            textComponent.text = visibleText;

            BakedTextCharCount.Add(textComponent.textInfo.characterCount);
        }

        generated = true;

        TextGenerated?.Invoke();
    }

    void ClearExistingTextComponents()
    {
        foreach (Transform child in textPanel.transform)
        {
            Destroy(child.gameObject);
        }
        BakedTextComponents.Clear();
        BakedTextCharCount.Clear();
    }

    TMP_Text InstantiateTextObject()
    {
        GameObject newTextObj = Instantiate(textPrefab, textPanel.transform);
        TMP_Text newTextComponent = newTextObj.GetComponent<TMP_Text>();

        BakedTextComponents.Add(newTextComponent);

        return newTextComponent;
    }

    void InstantiateSpacer()
    {
        TMP_Text spacer = InstantiateTextObject();
        spacer.text = ""; // Ensure the spacer has no text
        BakedTextCharCount.Add(0);
        spacer.textWrappingMode = TextWrappingModes.NoWrap;  //for height effect
        Canvas.ForceUpdateCanvases();
        Debug.Log("spacer added");
    }


    int FindFirstCharacterIndex(char character, string text)
    {
        int lastIndex = text.Length;

        for (int i = 0; i < lastIndex; i++)
        {
            if (text[i] == character)
            {
                return i;
            }
        }
        return -1;
    }

    int FindLastCharacterIndex(char character, string text)
    {
        int lastIndex = text.Length;

        for (int i = lastIndex - 1; i >= 0; i--)
        {
            if (text[i] == character)
            {
                return i;
            }
        }
        return -1;
    }

    int FindLastSpaceIndex(string text)
    {
        int character = FindLastCharacterIndex(' ', text);

        if (character == -1) return text.Length;
        return character;
    }

}