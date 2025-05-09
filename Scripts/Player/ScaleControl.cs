using NewFeatures;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class ScaleControl : MonoBehaviour
{
    public event Action ScaleUpdated;
    
    public SphereCollider interactionCollider;
    public SphereCollider gestureCollider;
    public Transform gfx;

    public float startSize = 0.8f;
    [HideInInspector] public bool startStageOverriden = false;
    
    public float gfxScaleBase = 0.1f;
    public float gfxScaleInterval = 0.1f;
    
    public float speedMultiplier = 0.1f;

    public float characterControllerRadius = 0.5f;
    public float characterControllerCenterYOffset = 0.1f;
    public float interactionRadius = 1.5f;
    public float gestureRadius = 3f;

    CharacterController controlCollider;
    SpeedChange speedChange;
    FoodSystem foodSystem;

    //int stage;

    private void Awake()
    {
        //if (gfx != null)
        //{
            controlCollider = FindAnyObjectByType<CharacterController>();
            speedChange = FindAnyObjectByType<SpeedChange>();
            foodSystem = FindAnyObjectByType<FoodSystem>();

            //if (!startStageOverriden) SetGFXScale(startSize);

        //}

    }

    private void Start()
    {

        SetGFXScale(GetStageGFXScale());
    }

    private void OnEnable()
    {
        

        //foodSystem.ShellFoodChanged += SetGFXScaleWithFoodSystem;
    }

    //private void OnDisable()
    //{
    //    foodSystem.ShellFoodChanged -= SetGFXScaleWithFoodSystem;
    //}


    public float GetStageGFXScale()
    {
        if (gfx.localScale.x == gfx.localScale.y && gfx.localScale.y == gfx.localScale.z)
        {
            return gfx.localScale.x;
        }
        throw new SystemException("scale is not uniform");
    }

    public void SetGFXScale(float scale)
    {
        SetScale(scale);
        SetColliderScale(scale);
        MatchSpeedToScale(scale);
        Debug.Log("Actual scale set to: " + scale);
        ScaleUpdated.Invoke();
    }

    public float GetScale => gfx.localScale.x;
   
    void SetScale(float scale)
    {
        //if (gfx == null) throw new System.Exception("No GFX found for hermit to update scale");
        gfx.localScale = new Vector3(scale, scale, scale);
    }

    void SetColliderScale(float scale)
    {
        controlCollider.radius = scale * characterControllerRadius;
        controlCollider.center = new Vector3(controlCollider.center.x, scale * (characterControllerRadius + characterControllerCenterYOffset), controlCollider.center.z);
        if (controlCollider.radius < 0.5f)
        {
            controlCollider.height = controlCollider.radius * 2f;

            float stepMultiplier = 0.83f;
            controlCollider.stepOffset = controlCollider.height * stepMultiplier;
        }

        interactionCollider.radius = scale * interactionRadius;
        interactionCollider.center = new Vector3(interactionCollider.center.x, scale, scale);

        gestureCollider.radius = scale * gestureRadius;
        gestureCollider.center = new Vector3(gestureCollider.center.x, scale, scale);
    }

    void MatchSpeedToScale(float scale)
    {
        speedChange.SetBaseSpeed(scale * speedMultiplier);
    }


    //public int GetStage()
    //{
    //    return stage;
    //}

    //public void IncreaseStage(int increase) // break up into simpler functions
    //{
    //    stage += increase;
    //    Debug.Log("scale increased to: " + stage);
    //}

    //public void SetStage(int stage)
    //{
    //    this.stage = stage;
    //    Debug.Log("new stage: " + stage);
    //}

    //public void SetGFXScaleBasedOnStage()
    //{
    //    SetGFXScale(GetStageGFXScale(stage));
    //}

    //public void SetGFXScaleBasedOnStage(int stage)
    //{
    //    SetGFXScale(GetStageGFXScale(stage));
    //}

    //public float GetStageGFXScale(int stage)
    //{
    //    return gfxScaleBase + (gfxScaleInterval * stage);
    //}

    //public void SetGFXScaleWithFoodSystem()
    //{

    //    //float foodBracket = foodSystem.GetCurrentBracket();
    //    //float nextFoodBracket = foodSystem.GetNextBracket();
    //    //float currentFood = foodSystem.GetTotalFood;
    //    //float percentageThroughBracket = Mathf.InverseLerp(foodBracket, nextFoodBracket, currentFood);
    //    //Debug.Log($"Percentage through food bracket = {percentageThroughBracket}");

    //    //float gfxBracket = gfxScaleBase + (gfxScaleInterval * foodBracket);
    //    //float newGFXBracket = gfxScaleBase + (gfxScaleInterval * nextFoodBracket);
    //    //float newScale = Mathf.Lerp(gfxBracket, newGFXBracket, percentageThroughBracket);
    //    //Debug.Log($"New GFX scale = {newScale}");

    //    SetGFXScale(CalculateGFXScaleWithFoodSystem());
    //    ScaleUpdated?.Invoke();
    //}

    //float CalculateGFXScaleWithFoodSystem()
    //{
    //    return CalculateGFXScaleWithFoodSystem(foodSystem.GetShellFood, foodSystem);
    //}

    //public float CalculateGFXScaleWithFoodSystem(int food, FoodSystem foodSystem)
    //{

    //    float foodBracketKey = foodSystem.GetBracketKey(food);
    //    float prevBracketKey = foodSystem.GetPrevBracketKey(food);
    //    //float nextFoodBracketKey = foodSystem.GetNextBracketKey(food);
    //    float bracketProgress = foodSystem.CalculateBracketProgress(food);

    //    float gfxBracket = gfxScaleBase + (gfxScaleInterval * foodBracketKey);
    //    float prevGfxBracket = gfxScaleBase + (gfxScaleInterval * prevBracketKey);
    //    //float nextGfxBracket = gfxScaleBase + (gfxScaleInterval * nextFoodBracketKey);

    //    Debug.Log($"GFX bracket = {gfxBracket}, prevGFXBracket = {prevGfxBracket}, bracket progress = {bracketProgress}");
    //    //Debug.Log($"GFX bracket = {gfxBracket}, nextGFXBracket = {nextGfxBracket}, bracket progress = {bracketProgress}");
    //    return Mathf.Lerp(prevGfxBracket, gfxBracket, bracketProgress);
    //}

}
