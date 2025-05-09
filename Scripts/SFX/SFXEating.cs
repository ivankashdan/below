using UnityEngine;

public class SFXEating : OneShotTrack
{
    //FoodSystem foodSystem;

    public OneShotTrack wormFood;
    public GameObject wormObject;

    //protected override void Awake()
    //{
    //    base.Awake();
    //    foodSystem = FindAnyObjectByType<FoodSystem>();
    //}

    private void OnEnable()
    {
        Food.FoodEaten += OnFoodEaten;
        //foodSystem.FoodEaten += OnFoodEaten;
    }

    private void OnDisable()
    {
        Food.FoodEaten -= OnFoodEaten;
        //foodSystem.FoodEaten -= OnFoodEaten;
    }

    void OnFoodEaten(GameObject gameObject)
    {
        if (gameObject == wormObject)
        {
            wormFood.PlayTrack();
        }
        else
        {
            PlayTrack();
        }
    }

}
