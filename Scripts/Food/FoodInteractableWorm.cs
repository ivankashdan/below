using Unity.Cinemachine;
using UnityEngine;

public class FoodInteractableWorm : FoodInteractable
{
    WormCamera wormCamera;

    public override void Awake()
    {
        base.Awake();
        wormCamera = FindAnyObjectByType<WormCamera>();
    }

    public override bool EatThisFood()
    {
        base.EatThisFood();
        wormCamera.ActivateCam(true);
        return true;
    }





}
