using UnityEngine;
using UnityEngine.Timeline;

public class FoodWorm : FoodInteractable
{
    public TimelineAsset playable;

    CutsceneManager cutsceneManager;

    public override void Awake()
    {
        base.Awake();

        cutsceneManager = FindAnyObjectByType<CutsceneManager>();
        //nearCameraFades = FindObjectsByType<HermieNearCameraFade>(FindObjectsSortMode.None);
    }

    public override void Interact()
    {
        cutsceneManager.PlayCutscene(playable);
    }

    public void EatWorm()
    {
        foodSystem.AddToFoodCount();

        FoodEatenEvent();

        PointOfInterest.Discover(true, gameObject);

        //AddBestiaryEntryIfAttached();

        interactionBehaviour.RemoveInteractable(gameObject);
        Destroy(gameObject);
    }

   

}
