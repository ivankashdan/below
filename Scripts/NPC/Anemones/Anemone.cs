using UnityEngine;

public class Anemone : MonoBehaviour, IInteractable
{
    AnemoneBehaviour anemoneBehaviour;
    InteractionBehaviour interactionBehaviour;

    public bool interactable = false;

    public ParticleSystem ambientParticles;

    private void Awake()
    {
        anemoneBehaviour = FindAnyObjectByType<AnemoneBehaviour>();
        interactionBehaviour = FindAnyObjectByType<InteractionBehaviour>();
    }

    public void Interact()
    {
        anemoneBehaviour.EquipAnemone(gameObject);
        interactionBehaviour.RemoveInteractable(gameObject);

        if (ambientParticles != null)
        {
            ambientParticles.Stop();
        }
    }

    public bool IsInteractable() =>
            interactable 
            && !anemoneBehaviour.IsAnemoneEquippedToPlayerShell(gameObject)
            && anemoneBehaviour.IsSpaceForAnemoneOnPlayerShell();

    public int GetInteractPriority() => 1;

}
