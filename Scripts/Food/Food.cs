using NewFeatures;
using System;
using UnityEngine;

abstract public class Food : MonoBehaviour
{
    static public event Action<GameObject> FoodEaten;

    //public int nutrition = 1;
    //public int stageRequirement = 0;

    //protected ScaleControl scaleControl;

    protected FoodSystem foodSystem;
    protected FoodVFX foodVFX;
    AnimationBehaviour animationBehaviour;
    protected InteractionBehaviour interactionBehaviour;

    bool animationActive;
     
    public virtual void Awake()
    {
        foodSystem = FindAnyObjectByType<FoodSystem>();
        foodVFX = FindAnyObjectByType<FoodVFX>();
        animationBehaviour = FindAnyObjectByType<AnimationBehaviour>();
        interactionBehaviour = FindAnyObjectByType<InteractionBehaviour>();
    }



    public virtual bool EatThisFood()
    {
        EatResolve();

        interactionBehaviour.RemoveInteractable(gameObject);
        Destroy(gameObject);

        return true;
    }

    protected void EatResolve()
    {
        foodSystem.AddToFoodCount();

        FoodEaten?.Invoke(gameObject);

        //foodVFX.PlayEatingVFX(transform);
        PointOfInterest.Discover(true, gameObject);

        AddBestiaryEntryIfAttached();

    }

    protected void FoodEatenEvent()
    {
        FoodEaten?.Invoke(gameObject);
    }

    protected virtual void AddBestiaryEntryIfAttached()
    {
        if (TryGetComponent<BestiaryEntry>(out var entry))
        {
            BestiaryManager.AddEntry(entry.reference);
        }
    }

    //public virtual void TryToEat()
    //{
    //    foodSystem.TryToEat(nutrition, gameObject);
    //}



}