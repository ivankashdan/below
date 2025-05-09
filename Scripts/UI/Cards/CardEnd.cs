using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Timeline;

public class CardEnd : MonoBehaviour
{
    public GameObject card;
    public CinemachineVirtualCameraBase cardCamera;
    public TimelineAsset triggerAsset;

    CardManager cardManager;
    CutsceneManager cutsceneManager;

    bool cardVisible;

    private void Awake()
    {
        cardManager = GetComponent<CardManager>();

        cutsceneManager = FindAnyObjectByType<CutsceneManager>();

        cardCamera.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        cutsceneManager.CutsceneEnded += OnCutsceneEnded;

    }

    private void OnDisable()
    {

        cutsceneManager.CutsceneEnded -= OnCutsceneEnded;
    }

    void OnCutsceneEnded(TimelineAsset playable)
    {
        if (playable == triggerAsset)
        {
            ShowCard();
        }
    }

    public void ShowCard()
    {
        cardManager.ShowCard(card, cardCamera);
        cardVisible = true;
    }

    //private void Update()
    //{
    //    if (cardVisible)
    //    {
    //        if (CardStart.IsAnyKeyDown())
    //        {
    //            GameExecutable.Instance.Restart();
    //        }
    //    }
    //}


}
