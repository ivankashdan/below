using UnityEngine;

public class LoopManager : MonoBehaviour
{
    [HideInInspector] public AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
}
