using System.Collections;
using UnityEngine;

public class HintLighterShell : MonoBehaviour
{

    public string hint = "hint: <color=#FFFFFF>lighter shell required";
    public Color color;

    public float delay = 4f;

    Coroutine timerCoroutine;

    CentralUIManager centralUIManager;

    private void Awake()
    {
        centralUIManager = GetComponent<CentralUIManager>();
    }

    public void Activate()
    {
        centralUIManager.SetActive(true);
        centralUIManager.SetCentralText(hint, color);
        StartTimerCoroutine();
    }

    void StartTimerCoroutine()
    {
        if (timerCoroutine != null) StopCoroutine(timerCoroutine);
        timerCoroutine = StartCoroutine(TimerCoroutine());
    }

    IEnumerator TimerCoroutine()
    {
        yield return new WaitForSeconds(delay);
        centralUIManager.SetActive(false);
    }

    public void UpdateOnControllerSwitch()
    {
        throw new System.NotImplementedException();
    }
}
