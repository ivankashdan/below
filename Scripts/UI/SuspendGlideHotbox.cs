using System.Collections;
using UnityEngine;

public class SuspendGlideHotbox : MonoBehaviour
{
    public GameObject glideHotbox;
    public float releaseDelay = 2f;


    private void Start()
    {
        Suspend();
    }
    public void Suspend()
    {
        glideHotbox.SetActive(false);
    }

    public void Release()
    {
        StopAllCoroutines();
        StartCoroutine(ReleaseCoroutine());
    }

    IEnumerator ReleaseCoroutine()
    {
        yield return new WaitForSeconds(releaseDelay);
        glideHotbox.SetActive(true);
    }

}
