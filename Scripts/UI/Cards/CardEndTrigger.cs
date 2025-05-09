using UnityEngine;

public class CardTrigger : MonoBehaviour
{
    CardEnd cardEnd;

    private void Awake()
    {
        cardEnd = FindAnyObjectByType<CardEnd>();
    }


    private void OnTriggerEnter(Collider other)
    {
        cardEnd.ShowCard();
    }



}
