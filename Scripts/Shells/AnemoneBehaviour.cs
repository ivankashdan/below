using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class AnemoneBehaviour : MonoBehaviour
{
    public event Action AnemoneEquipped;

    public ParticleSystem stingParticles;

    //SFXBehaviour sfx;
    ShellCheck shellCheck;

    void Awake()
    {
        //sfx = FindAnyObjectByType<SFXBehaviour>();
        shellCheck = FindAnyObjectByType<ShellCheck>();
    }

    public bool IsSpaceForAnemoneOnPlayerShell()
    {
        //Debug.Log("Anemone count: " + GetAnemoneCount());
        //Debug.Log("Anemone max: " + GetAnemoneMax());

        return GetAnemoneCount() < GetAnemoneMax();
    }
        
   
    public void EquipAnemone(GameObject anemonePickup)
    {
        if (!shellCheck.IsAShellEquipped()) return;

        GameObject anemones = GetAnemoneContainer();
        if (anemones == null || anemones.transform.childCount == 0) return;

        foreach (Transform anemone in anemones.transform)
        {
            if (anemone.gameObject.activeSelf) continue;

            anemone.gameObject.SetActive(true);
            ConvertToPlayerAnemone(anemone.gameObject);

            AnemoneEquipped?.Invoke();

            //if (anemone.GetComponent<Anemone>())
            //{
            //    anemone.GetComponent<Anemone>().charged = IsAnemoneCharged(anemonePickupGFX);
            //}

            if (IsAnemoneEquippedToAShell(anemonePickup.gameObject))
            {
                anemonePickup.gameObject.SetActive(false);
            }
            else
            {
                Destroy(anemonePickup.gameObject);
                //sfx.Play(sfx.anemonePickupSFX);
            }
            break;
        }
    }

    bool IsAnemoneEquippedToAShell(GameObject anemone) => anemone.GetComponentInParent<AnemoneContainer>();

    public bool IsAnemoneEquippedToPlayerShell(GameObject anemone) => anemone.GetComponentInParent<ShellManager>();

    public void DropAnemones()
    {
        GameObject anemones = GetAnemoneContainer();

        for (int i = 0; i < anemones.transform.childCount; i++)
        {
            Transform anemone = anemones.transform.GetChild(i);

            if (anemone.gameObject.activeSelf)
            {

                anemone.parent = null;

                ConvertToLooseAnemone(anemone.gameObject);
                i--;
            }
        }
    }

    public void ReleaseCharge()
    {
        stingParticles.Play();
        //SetAnemoneCharge(false);
    }

    GameObject GetAnemoneContainer()
    {
        return shellCheck.IsAShellEquipped() ? GetAnemoneContainer(shellCheck.GetShell()) : null;
    }
    GameObject GetAnemoneContainer(GameObject shell)
    {
        AnemoneContainer container = shell.GetComponentInChildren<AnemoneContainer>();
        if (container != null)
        {
            return container.gameObject;
        }
        return null;
    }

    int GetAnemoneCount() => CountAnemoneSlots(true);
    int GetAnemoneMax() => CountAnemoneSlots(false);
    int CountAnemoneSlots(bool onlyActive)
    {
        int count = 0;
        GameObject anemones = GetAnemoneContainer();

        if (anemones != null)
        {
            foreach (Transform anemone in anemones.transform)
            {
                if (!onlyActive || anemone.gameObject.activeSelf)
                {
                    count++;
                }
            }
        }
        return count;
    }

    void ConvertToPlayerAnemone(GameObject anemone)
    {
        GameObject colliderObject = anemone.GetComponentInChildren<Collider>().gameObject;
        colliderObject.layer = LayerMask.NameToLayer("Attire");

        if (anemone.GetComponent<HermieNearCameraFade>() == null)
        {
            var cameraFade = anemone.AddComponent<HermieNearCameraFade>();
            cameraFade.material = anemone.GetComponentInChildren<MeshRenderer>().material;
        }
    }

    void ConvertToLooseAnemone(GameObject anemone)
    {
        GameObject colliderObject = anemone.GetComponentInChildren<Collider>().gameObject;
        colliderObject.layer = LayerMask.NameToLayer("Walkable");

        if (anemone.TryGetComponent<HermieNearCameraFade>(out var cameraFade))
        {
            Destroy(cameraFade);
        }
    }


    ////////////////////not used currently

    public bool IsAnemoneEquippedToShell(GameObject shell)
    {
        GameObject anemones = GetAnemoneContainer(shell);
        if (anemones == null) return false;

        foreach (Transform anemoneSlot in anemones.transform)
        {
            if (anemoneSlot.gameObject.activeSelf) return true;
        }
        return false;
    }

    public GameObject[] GetAllAnemonesOnShell(GameObject shell)
    {
        GameObject anemones = GetAnemoneContainer(shell);

        if (anemones == null || anemones.transform.childCount == 0)
            return null;

        List<GameObject> allAnemones = new List<GameObject>();

        foreach (Transform t in anemones.transform)
        {
            GameObject anemone = t.gameObject;
            if (!anemone.activeSelf) allAnemones.Add(anemone);
        }
        return allAnemones.ToArray();
    }

    public GameObject GetFirstAnemoneOnShell(GameObject shell)
    {
        GameObject anemones = GetAnemoneContainer(shell);

        if (anemones == null || anemones.transform.childCount == 0)
            return null;

        foreach (Transform t in anemones.transform)
        {
            GameObject anemone = t.gameObject;
            if (!anemone.activeSelf) return anemone;
        }
        return null;

    }

    public bool IsAnAnemoneEquipped() => GetAnemoneCount() != 0;
    /////////////////

  

    //private void Update()
    //{
    //    //if (IsAnAnemoneEquipped())
    //    //{
    //    //    bool hasEnergy = energyBehaviour.HasEnergy();
    //    //    if (hasEnergy)
    //    //    {
    //    //        SetAnemoneCharge(true);
    //    //    }
    //    //    else
    //    //    {
    //    //        SetAnemoneCharge(false);
    //    //    }
    //    //}
    //}


    //public bool IsThereAnemoneOfCharge(bool charge) => GetFirstAnemoneOfCharge(charge) != null;
    //public bool IsThereAnemoneOfChargeOnShell(bool charge, GameObject shell) => GetFirstAnemoneOfChargeOnShell(charge, shell) != null;

    //GameObject GetFirstAnemoneOfCharge(bool charge)
    //{
    //    return GetFirstAnemoneOfChargeOnShell(charge, shellBehaviour.GetShell());
    //}



    //GameObject GetFirstAnemoneOfChargeOnShell(bool charge, GameObject shell)
    //{
    //    GameObject anemones = GetAnemoneContainer(shell);

    //    if (anemones == null || anemones.transform.childCount == 0)
    //        return null;

    //    foreach (Transform t in anemones.transform)
    //    {
    //        GameObject anemone = t.gameObject;
    //        if (!anemone.activeSelf) continue;

    //        if (charge && IsAnemoneCharged(anemone))
    //            return anemone;
    //        if (!charge && !IsAnemoneCharged(anemone))
    //            return anemone;
    //    }

    //    return null;

    //}



    //public void SetAnemoneCharge(bool charge)
    //{
    //    GameObject anemone = GetFirstAnemoneOfCharge(!charge);
    //    if (anemone == null) throw new System.Exception("No Anemone found");

    //    Anemone anemoneScript = anemone.GetComponent<Anemone>();
    //    if (anemoneScript == null) throw new System.Exception("No Anemone script found on anemone object");

    //    anemoneScript.charged = charge;
    //}


    //bool IsAnemoneCharged(GameObject anemone)
    //{
    //    if (anemone.GetComponent<Anemone>().charged) return true;
    //    else return false;
    //}




    //public void GiveAnemonesToShell(GameObject newShell)
    //{
    //    if (IsAnAnemoneEquipped())
    //    {
    //        GameObject equippedShell = shellBehaviour.GetShell();
    //        int equippedCount = GetAnemoneCount();
    //        int chargeCount = GetAnemoneChargeCount();

    //        AddOrRemoveAnemones(equippedShell, equippedCount, false);
    //        AddOrRemoveAnemones(newShell, equippedCount, true);

    //        for (int i = 0; i < equippedCount; i++) //reset charges
    //        {
    //            SetAnemoneCharge(false);
    //        }

    //        for (int i = 0; i < chargeCount; i++)
    //        {
    //            SetAnemoneCharge(true);
    //        }
    //    }
    //}

    //void AddOrRemoveAnemones(GameObject shell, int count, bool activate)
    //{
    //    GameObject anemones = GetAnemoneContainer(shell);

    //    for (int i = 0; i < count; i++)
    //    {
    //        if (activate)
    //        {
    //            anemones.transform.GetChild(i).gameObject.SetActive(true);
    //        }
    //        else
    //        {
    //            anemones.transform.GetChild(i).gameObject.SetActive(false);
    //        }
    //    }
    //}

    //int GetAnemoneChargeCount()
    //{
    //    int count = 0;
    //    GameObject anemones = GetAnemoneContainer();

    //    if (anemones != null)
    //    {
    //        foreach (Transform anemone in anemones.transform)
    //        {
    //            if (anemone.gameObject.activeSelf && IsAnemoneCharged(anemone.gameObject)) //only count if it's active
    //            {
    //                count++;
    //            }
    //        }
    //    }
    //    return count;
    //}



}
