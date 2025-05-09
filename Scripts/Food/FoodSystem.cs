using System;
using UnityEngine;

public class FoodSystem : MonoBehaviour
{
    public event Action FoodEaten;

    int foodCount = 0;

    public int GetFoodCount => foodCount;

    PlayerState playerState;

    private void Awake()
    {
        playerState = FindAnyObjectByType<PlayerState>();
    }

    public void AddToFoodCount()
    {
        foodCount++;
        FoodEaten?.Invoke();

        //playerState.SetState(PlayerState.State.eating);
    }



}
