using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTestManager : MonoBehaviour
{

    public Text text;

    private void Start()
    {
        ClearText();
    }

    public void TriggerMessage(string message, float delay)
    {
        text.text = message;

        StopAllCoroutines();
        StartCoroutine(TimeoutCoroutine(delay));
    }

    IEnumerator TimeoutCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);

        ClearText();
    }

    void ClearText()
    {
        text.text = "";
    } 

}
