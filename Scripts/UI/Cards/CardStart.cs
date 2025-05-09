using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CardStart : MonoBehaviour
{
    public GameObject card;
    public CinemachineVirtualCameraBase cardCamera;

    public bool cardAtStart;

    bool cardVisible;
    bool cardCleared;

    CardManager cardManager;
    OpeningCutscene openingCutscene;

    private void Awake()
    {
        cardManager = GetComponent<CardManager>();
        openingCutscene = FindAnyObjectByType<OpeningCutscene>();

        cardCamera.gameObject.SetActive(false);
        //cardCamera.Priority = 0;
    }

    private void Start()
    {
        if (cardAtStart)
        {
            cardManager.ShowCard(card, cardCamera);
            cardVisible = true;
        }
    }

    private void Update()
    {
        if (!cardCleared && cardVisible)
        {
            if (IsAnyKeyDown())
            {
                cardManager.HideCard();
                openingCutscene.StartCutscene();
                cardCleared = true;
            }
        }
    }

    public static bool IsAnyKeyDown()
    {
        foreach (var map in InputManager.controls.asset.actionMaps) // Loop through all Action Maps
        {
            if (map.name == "Menu") continue; 

            foreach (var action in map.actions) // Loop through all Actions in each map
            {
                if (action == InputManager.controls.Gameplay.Look) continue;
                if (action == InputManager.controls.Gameplay.Move) continue;
                if (action == InputManager.controls.Gameplay.Menu) continue;

                if (action.IsPressed()) // Check if the action is currently pressed
                {
                    return true;
                }
            }
        }
        return false;
    }
   


}
