using System;
using Unity.Cinemachine;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public event Action<GameObject> CardShown;
    public event Action CardHidden;

    CinemachineVirtualCameraBase currentCamera;
    GameObject currentCard;

    PlayerState playerState;

    private void Awake()
    {
        playerState = FindAnyObjectByType<PlayerState>();   
    }

    public bool isCardShown => currentCamera != null;

    public void ShowCard(GameObject card, CinemachineVirtualCameraBase camera)
    {
        playerState.SetState(PlayerState.State.none);

        camera.gameObject.SetActive(true);
        //camera.Priority = 100;
        card.SetActive(true);

        currentCamera = camera;
        currentCard = card;

        CardShown?.Invoke(card);
    }

    public void HideCard()
    {
        if (currentCard != null)
        {
            if (currentCamera != null)
            {
                currentCamera.gameObject.SetActive(false);
                //currentCamera.Priority = 0;
                currentCamera = null;
            }

            currentCard.SetActive(false);
            currentCard = null;

            playerState.SetState(PlayerState.State.ground);
            CardHidden?.Invoke();
        }
    }
}
