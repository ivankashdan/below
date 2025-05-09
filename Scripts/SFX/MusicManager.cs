using Unity.VisualScripting;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip openingCard, exploration, bestiary;

    public GameObject cardObject;

    CardManager cardManager;

    AudioSource audioSource;
    AudioCrossfade audioCrossfade;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioCrossfade = GetComponent<AudioCrossfade>();

        cardManager = FindAnyObjectByType<CardManager>();

    }

    //private void OnEnable()
    //{
    //    cardManager.CardShown += OnCardShown;
    //    cardManager.CardHidden += OnCardHidden;
    //}

    //private void OnDisable()
    //{
    //    cardManager.CardShown -= OnCardShown;
    //    cardManager.CardHidden -= OnCardHidden;
    //}

    //void OnCardShown(GameObject card)
    //{
    //    if (card == cardObject)
    //    {
    //        audioSource.clip = openingCard;
    //        audioSource.Play();
    //    }
    //}

    //void OnCardHidden()
    //{
    //    audioCrossfade.CrossfadeTo(exploration, 2);
    //}



}
