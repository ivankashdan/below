using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellDamage : MonoBehaviour
{

    ShellCheck shellCheck;
    ShellEquip shellEquip;
    AnemoneBehaviour anemoneBehaviour;
    private void Start()
    {
        shellCheck = FindAnyObjectByType<ShellCheck>();
        shellEquip = FindAnyObjectByType<ShellEquip>();
        anemoneBehaviour = FindAnyObjectByType<AnemoneBehaviour>();

    }

    public void TryDamagePlayer()
    {
        if (shellCheck.IsAShellEquipped())
        {


            TryDestroyShell();

        }
        else
        {
            KillPlayer();
        }
    }

    public void KillPlayer()
    {
        if (shellCheck.IsAShellEquipped())
        {
            //if (hidePhysics.IsHidden())
            //{
            //    hidePhysics.ForceStopHidingInShell();
            //}
            shellEquip.DropShell();
        }
        //Destroy(gameObject);
        //FindAnyObjectByType<SceneController>().GameOver();
    }


    public void TryDestroyShell()
    {
        GameObject brokenShellPrefab = shellCheck.GetShellRef().brokenShell;

        if (brokenShellPrefab == null) shellEquip.DropShell();

        else SplinterShell(brokenShellPrefab);
    }

    void SplinterShell(GameObject brokenShellPrefab)
    {
        anemoneBehaviour.DropAnemones();

        Destroy(shellCheck.GetShell().gameObject);
        GameObject brokenShell = Instantiate(brokenShellPrefab, transform, false);
        brokenShell.transform.parent = null;
        foreach (Transform piece in brokenShell.transform)
        {
            piece.gameObject.AddComponent<MeshCollider>().convex = true; //replace with something more efficient
            piece.gameObject.AddComponent<Rigidbody>();
        }
        //shellBurst.Play();
    }



}