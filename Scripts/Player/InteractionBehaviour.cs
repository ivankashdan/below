using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class InteractionBehaviour : MonoBehaviour
{
    public event Action InteractableSelected;
    public event Action<GameObject> InteractableSubmitted;

    public List<GameObject> interactables = new List<GameObject>();
    public GameObject selectedInteractable;

    [HideInInspector] public bool active;

    public bool HasSelected() => selectedInteractable != null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IInteractable interactable))
        {
            if (interactable.IsInteractable())
            {
                AddInteractable(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IInteractable interactable))
        {
            RemoveInteractable(other.gameObject);
        }
    }

    public void InteractAction()
    {
        if (selectedInteractable != null)
        {
            InteractableSubmitted?.Invoke(selectedInteractable);
            selectedInteractable.GetComponent<IInteractable>().Interact();
        }
    }

    void SelectInteractable()
    {
        if (interactables.Count > 0)
        {
            List<GameObject> sortedList = interactables
               .Where(obj => obj.GetComponent<IInteractable>() != null)
               .OrderBy(obj => obj.GetComponent<IInteractable>().GetInteractPriority())
               .ToList();

            if (sortedList.Count > 0)
            {
                selectedInteractable = sortedList[0];
                Debug.Log("Selected Target: " + selectedInteractable.name);
            }
        }
        else
        {
            selectedInteractable = null;
        }

        InteractableSelected?.Invoke();
    }

    void AddInteractable(GameObject gameObject)
    {
        interactables.Add(gameObject);
        SelectInteractable();
    }

    public void RemoveInteractable(GameObject gameObject)
    {
        if (interactables.Contains(gameObject))
        {
            interactables.Remove(gameObject);
            SelectInteractable();
        }
    }
}
