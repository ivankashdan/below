using UnityEngine;

public class HintLighterShellTrigger : MonoBehaviour
{
    bool activated;

    ShellCheck shellCheck;
    HintLighterShell hintLighterShell;
    private void Awake()
    {
        shellCheck = FindAnyObjectByType<ShellCheck>();
        hintLighterShell = FindAnyObjectByType<HintLighterShell>();
    }

    private void OnTriggerEnter(Collider other)//
    {
        if (!shellCheck.DoesShellHaveGlideAbility() && !activated && other.gameObject.tag == "Player")
        {
            hintLighterShell.Activate();
            activated = true;
        }
    }

    
   



}
