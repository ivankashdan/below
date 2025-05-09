using UnityEngine;

public class WIndowExitTrigger : MonoBehaviour
{
    public GameObject wrasseHotbox;
    public BestiaryScriptableObject reference;

    SuspendGlideHotbox suspendGlideHotbox;
    TriggerCameraTurn triggerCameraTurn;
    TutorialTriggerHide triggerHide;
    OrbitTrigger orbitTrigger;

    bool activated;

    private void Awake()
    {
        suspendGlideHotbox = wrasseHotbox.GetComponent<SuspendGlideHotbox>();
        triggerCameraTurn = wrasseHotbox.GetComponent <TriggerCameraTurn>();
        orbitTrigger = wrasseHotbox.GetComponent<OrbitTrigger>();
        triggerHide = wrasseHotbox.GetComponent<TutorialTriggerHide>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!activated)
        {
            if (other.CompareTag("Player"))
            {
                OnWrasseAnimationFinished();
            }
        }

     
    }

    public void OnWrasseAnimationFinished()
    {
        activated = true;

        suspendGlideHotbox.Release();
        triggerCameraTurn.EndTurn();
        orbitTrigger.ReleaseDistance();
        triggerHide.ClearTutorial();
        BestiaryManager.AddEntry(reference);
    }



}
