using UnityEngine;

public class FoodSpongeTile : MonoBehaviour
{

    public Transform parts;


    FoodSystem foodSystem;
    private void Awake()
    {
        foodSystem = FindAnyObjectByType<FoodSystem>();
    }

    private void OnEnable()
    {
        foodSystem.FoodEaten += OnFoodEaten;
    }

    private void OnDisable()
    {
        foodSystem.FoodEaten -= OnFoodEaten;
    }

    void OnFoodEaten()
    {
        if (parts.childCount <= 1)
        {
            Destroy(gameObject);
        }
    }

}
