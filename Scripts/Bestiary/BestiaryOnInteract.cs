using UnityEngine;

public class BestiaryOnInteract : MonoBehaviour
{
    bool activated;

    InteractionBehaviour interactionBehaviour;
    private void Awake()
    {
        interactionBehaviour = FindAnyObjectByType<InteractionBehaviour>();
    }

    private void OnEnable()
    {
        interactionBehaviour.InteractableSubmitted += OnInteractableSubmitted;
    }

    private void OnDisable()
    {
        interactionBehaviour.InteractableSubmitted -= OnInteractableSubmitted;
    }


    void OnInteractableSubmitted(GameObject interactableObject)
    {
        if (activated == false && interactableObject == gameObject)
        {
            if (TryGetComponent<BestiaryEntry>(out var entry))
            {
                BestiaryManager.AddEntry(entry.reference);
                activated = true;
                Debug.Log("Bestiary updated");
            }
        }
    }
}

