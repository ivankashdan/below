using UnityEngine;

public class SFXOpening : MusicTrack
{
    public GameObject cardObject;

    CardManager cardManager;


    protected override void Awake()
    {
        base.Awake();

        cardManager = FindAnyObjectByType<CardManager>();
    }

    private void OnEnable()
    {
        cardManager.CardShown += OnCardShown;
        cardManager.CardHidden += OnCardHidden;
    }

    private void OnDisable()
    {
        cardManager.CardShown -= OnCardShown;
        cardManager.CardHidden -= OnCardHidden;
    }

    void OnCardShown(GameObject card)
    {
        if (card == cardObject)
        {
            PlayTrack();
        }
    }

    void OnCardHidden()
    {
        StopTrack();
    }

}
