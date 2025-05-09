using NewFeatures;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShellEquip : MonoBehaviour
{
    public event Action ShellChanged;


    public ParticleSystem levelUpSparks;

    public float dropOffset = 1f;


    ShellCheck shellCheck;
    ShellConversion shellConversion;
    ScaleControl scaleControl;
    SpeedChange speedChange;
    HopState hopState;
    FallState fallState;
    GroundState groundState;
    //SFXBehaviour sfx;
    AnimationBehaviour animationBehaviour;

    private void Awake()
    {
        shellCheck = FindAnyObjectByType<ShellCheck>();
        shellConversion = FindAnyObjectByType<ShellConversion>();
        scaleControl = FindAnyObjectByType<ScaleControl>();
        speedChange = FindAnyObjectByType<SpeedChange>();
        hopState = FindAnyObjectByType<HopState>();
        fallState = FindAnyObjectByType<FallState>();
        groundState = FindAnyObjectByType<GroundState>();
        //sfx = FindAnyObjectByType<SFXBehaviour>();
        animationBehaviour = FindAnyObjectByType<AnimationBehaviour>();

    }

  

    public void EquipShell(GameObject newShell)
    {
        ShellReference newShellRef = shellCheck.GetShellRef(newShell); //make shell player mode

        PutShellOn(newShell, newShellRef);
        //SetPlayerGFXScale(newShellRef);
        ApplyAnimationSet(newShellRef);

        levelUpSparks.Play();
        //sfx.Play(sfx.shellPickupSFX);

        ShellChanged?.Invoke();

        StopAllCoroutines();
        StartCoroutine(DelayedMovementModifierRoutine(newShellRef));
    }


    public void DropShell()
    {
        GameObject shell = shellCheck.GetShell();
        shellConversion.ConvertToLooseShell(shell);
        shell.transform.parent = null;
    }

    void SetScentColor(GameObject shell, Color color)
    {
        ShellScent shellScent = shell.GetComponentInChildren<ShellScent>();
        var particleMain = shellScent.scentParticle.main;
        particleMain.startColor = color;
        Debug.Log("Shell scent colour changed to grey");
    }

    void PutShellOn(GameObject newShell, ShellReference newShellRef)
    {
        if (shellCheck.IsAShellEquipped()) //change shells if wearing
        {
            //anemoneBehaviour.GiveAnemonesToShell(newShell); //keep anemones
            GameObject equippedShell = shellCheck.GetShell();

            PointOfInterest.Discover(false, equippedShell);
            PointOfInterest.Discover(true, newShell);


            //SetScentColor(equippedShell, shellConversion.wornScent); //when dropped, turn grey
            PlaceShellOnGround(equippedShell, newShell.transform.position);
        }


        newShell.transform.parent = transform; //put shell on

        //OverrideTransformPass(newShell, newShellRef);

        shellConversion.ConvertToPlayerShell(newShell);
    }

    //void OverrideTransformPass(GameObject newShell, ShellReference newShellRef)
    //{
    //    if (newShellRef.overrideTransformLocal)
    //    {
    //        if (newShellRef.overridePosLocal != Vector3.zero)
    //        {
    //            newShell.transform.localPosition = newShellRef.overridePosLocal;
    //        }
    //        if (newShellRef.overrideEulerLocal != Vector3.zero)
    //        {
    //            newShell.transform.localEulerAngles = newShellRef.overrideEulerLocal;
    //        }
    //    }
    //}


    IEnumerator DelayedMovementModifierRoutine(ShellReference shellReference) //temp fix for GFX change
    {
        yield return new WaitForSeconds(1f);
        ApplyModifiers(shellReference);
    }

    void ApplyModifiers(ShellReference newShellRef)
    {

        //if (newShellRef.changesSize)
        //{
        //    scaleControl.SetGFXScale(newShellRef.sizeChange);
        //}

        if (newShellRef.gameObject.TryGetComponent(out ShellModifierMovement shellModifierMovement))
        {
            speedChange.SetBaseSpeed(shellModifierMovement.speedBase);
            fallState.maxGravity = shellModifierMovement.gravityFall;
            groundState.maxGravity = shellModifierMovement.gravityWalk;

        }
        else
        {
            speedChange.SetBaseSpeed(speedChange.defaultBaseSpeed);
            fallState.maxGravity = fallState.maxGravityDefault;
            groundState.maxGravity = groundState.maxGravityDefault;
        }


        //speedChange.SetBaseSpeed(newShellRef.speedBase);

        

        //if (newShellRef.changesSpeed)
        //{
        //    speedChange.ModifyBaseSpeed(newShellRef.speedMultipler); //modify speed after scaleModifier
        //}

        //hopAbility.SetJumpSettings(newShellRef.jumpMaxTime, newShellRef.jumpMultiplier, newShellRef.jumpUpReduction);
    }

    void ApplyAnimationSet(ShellReference newShellRef)
    {
        if (newShellRef.controller != null)
        {
            animationBehaviour.animator.runtimeAnimatorController = newShellRef.controller;
        }
    }

    void PlaceShellOnGround(GameObject shell, Vector3 groundPosition)
    {
        shellConversion.ConvertToLooseShell(shell);
        shell.transform.parent = null;

        Vector3 rayPosition = groundPosition + new Vector3(0, dropOffset, 0);
        RaycastHit hit;
        if (Physics.Raycast(rayPosition, Vector3.down, out hit, Mathf.Infinity))
        {
            float hitOffset = 2f;
            shell.transform.position = hit.point + new Vector3(0, hitOffset, 0);
        }
        else
        {
            shell.transform.position = groundPosition;
        }


    }
}