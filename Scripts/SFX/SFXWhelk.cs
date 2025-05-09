using UnityEngine;

public class SFXWhelk : MonoBehaviour
{
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        audioSource.Play();
    }


}
