using System;
using System.Linq;
using TMPro;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
public enum Horizontal
{
    Left,
    Right
}
public class MemoryTextSelection : MonoBehaviour
{
    public event Action SelectionChanged;

    public float speed;
    public float speedMin = 1f;
    public float speedMax = 3f;
    public float speedFast = 4f;
    public float stickSensitivityThreshold = 0;
    //public AnimationCurve stickSensitivity = AnimationCurve.EaseInOut(0, 0, 1, 1);

    TMP_Text selectedTextComponent;
    public int selectedTextComponentIndex;
    //public int selectedCharacterIndex;
    public float selectedCharacterPoint;

    public int GetSelectedTextComponent => selectedTextComponentIndex;
    //public int GetSelectedCharacter => selectedCharacterIndex;

    public float percentageThroughText;

    float manualEffectsOffset = 1;
    public void SetManualEffectsOffset(float value) => manualEffectsOffset = value;

    MemoryTextComponentManager textComponentManager;

    private void Awake()
    {
        textComponentManager = GetComponent<MemoryTextComponentManager>();
    }

    private void Update()
    {
        if (textComponentManager.IsGenerated) 
        percentageThroughText = GetPercentageThroughText();
    }

    public float GetPercentageThroughText()
    {
        float totalCharacters = textComponentManager.BakedTextCharCount.Sum();
        float charactersBeforeSelected = textComponentManager.BakedTextCharCount.Take(selectedTextComponentIndex).Sum();
        float percentageThroughCharacters = (charactersBeforeSelected + selectedCharacterPoint) / totalCharacters;

        return percentageThroughCharacters;
    }

    public bool IsMidpointAboveSelection(int textComponentIndex) => !IsMidpointBelowSelection(textComponentIndex);

    public bool IsMidpointBelowSelection(int textComponentIndex)
    {
        float totalLineValue = 1f / (float)textComponentManager.BakedTextComponents.Count;
        float percentageThroughLines = (float)textComponentIndex / (float)textComponentManager.BakedTextComponents.Count;
        float percentageThroughLine = 0.5f;
        float componentPercentageThroughText = percentageThroughLines + (percentageThroughLine * totalLineValue);

        float currentPercentageThroughText = GetPercentageThroughText();

        return componentPercentageThroughText - currentPercentageThroughText > 0;
    }


    public float GetDistanceToSelection(int textComponentIndex)
    {
        float totalLineValue = 1f / (float)textComponentManager.BakedTextComponents.Count;

        float percentageThroughLines = (float)textComponentIndex / (float)textComponentManager.BakedTextComponents.Count;
        float percentageThroughLine = 0.5f;
        float componentPercentageThroughText = percentageThroughLines + (percentageThroughLine * totalLineValue);

        float currentPercentageThroughText = GetPercentageThroughText();

        return Mathf.Abs(componentPercentageThroughText - currentPercentageThroughText);
    }

    public float GetAdjustedDistanceToSelection(int textComponentIndex)
    {
        float midPointDistanceToSelection = GetDistanceToSelection(textComponentIndex);   

        float rangeForDistance = Mathf.Lerp(0, manualEffectsOffset, midPointDistanceToSelection);  //0.35 

        return rangeForDistance;
        
        ////  need to compensate for different totalComponents regardless of how many there are...

        //float totalComponents = textComponentManager.BakedTextComponents.Count;

        //float maxTextComponents = 10;
        //float clampedTotal = Mathf.Clamp(totalComponents, 0, maxTextComponents); //7
        //float countPercentage = Mathf.InverseLerp(0, maxTextComponents, clampedTotal); //0.7

        //float rangeForDistance = Mathf.Lerp(0, countPercentage, midPointDistanceToSelection);  //0.35 

        //float adjustMidPointForScale = rangeForDistance;
      
        //return adjustMidPointForScale;
    }

    public void IncrementCharacter(Horizontal direction)
    {
        if (selectedTextComponent == null) return;

        //int charCount = textComponentManager.BakedTextCharCount[selectedTextComponentIndex];

        
        switch (direction)
        {
            case Horizontal.Left:
                if (selectedCharacterPoint > speed)
                {
                    //selectedCharacterIndex -= speed;
                    selectedCharacterPoint -= speed;
                }
                else if (selectedTextComponentIndex > 0)
                {
                    SelectTextComponent(selectedTextComponentIndex - 1);
                    selectedCharacterPoint = textComponentManager.BakedTextCharCount[selectedTextComponentIndex] - speed;
                    //selectedCharacterIndex = textComponentManager.BakedTextCharCount[selectedTextComponentIndex] - speed;
                }
                break;
            case Horizontal.Right:

                if (selectedCharacterPoint < textComponentManager.BakedTextCharCount[selectedTextComponentIndex] - speed)
                {
                    selectedCharacterPoint += speed;
                    //selectedCharacterIndex += roundedSpeed;
                }
                else if (selectedTextComponentIndex + 1 < textComponentManager.BakedTextComponents.Count - 1)
                {
                    SelectTextComponent(selectedTextComponentIndex + 1);

                    selectedCharacterPoint = 0;
                }
                break;
        }

        //if (selectedCharacterPoint >= 0 && selectedCharacterPoint < selectedTextComponent.text.Length)
        //{
        //    Debug.Log(selectedTextComponent.text[selectedCharacterIndex]);
        //}

        SelectionChanged?.Invoke();
      
    }

    public void InitiateSelection()
    {   
        SelectTextComponent(0);
        selectedCharacterPoint = 0;
    }

    void SelectTextComponent(int index)
    {
        if (index < textComponentManager.BakedTextComponents.Count - 1)
        {
            selectedTextComponent = textComponentManager.BakedTextComponents[index];
            selectedTextComponentIndex = index;
        }
        else
        {
            Debug.Log("Unable to selected text component");
        }
    }



}