using UnityEngine;

public class KrillDeath : FoodInteractable
{
    public AudioSource sfxKill;

    public override void Interact()
    {
        base.Interact();

        GameObject audioContainer = Instantiate(sfxKill.gameObject, sfxKill.transform.position, sfxKill.transform.rotation);
        audioContainer.transform.parent = null;
        var audioSource = audioContainer.GetComponent<AudioSource>();
        audioSource.enabled = true;
        audioSource.Play();

    }


}
