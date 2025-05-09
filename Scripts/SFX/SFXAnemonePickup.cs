using UnityEngine;

public class SFXAnemonePickup : OneShotTrack
{

    AnemoneBehaviour anemoneBehaviour;


    protected override void Awake()
    {
        base.Awake();
        anemoneBehaviour = FindAnyObjectByType<AnemoneBehaviour>();
    }

    private void OnEnable()
    {
        anemoneBehaviour.AnemoneEquipped += OnAnemoneEquipped;
    }

    private void OnDisable()
    {
        anemoneBehaviour.AnemoneEquipped -= OnAnemoneEquipped;   
    }

    void OnAnemoneEquipped()
    {
        PlayTrack();
    }
}
