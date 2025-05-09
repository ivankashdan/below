using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[ExecuteInEditMode]
public class ShellReference : MonoBehaviour, IInteractable
{
    public enum Ability
    {
        none,
        glide, 
        amplifyGesture,
        storeAnemones
    }

    

    [Header("Shell")]
    public bool convertShellToContext = false;
    [HideInInspector] public bool connectedToRig;


    [Header("Modifiers")]
    public int stage = 1;
    
    public Ability ability;

    //public float speedBase = 1f;
    //public bool 
    //public float gravity = -15;

    [Header("Optional")]
   
    public RuntimeAnimatorController controller;
    public GameObject brokenShell;
   

    ShellConversion shellConversion;
    ShellEquip shellEquip;
    ShellCheck shellCheck;
    InteractionBehaviour interactionBehaviour;

    private void OnEnable()
    {
        shellConversion = FindAnyObjectByType<ShellConversion>();
        shellEquip = FindAnyObjectByType<ShellEquip>();
        shellCheck = FindAnyObjectByType<ShellCheck>();
        interactionBehaviour = FindAnyObjectByType<InteractionBehaviour>();
    }

    private void Update()
    {
        if (convertShellToContext)
        {
            convertShellToContext = false;
            shellConversion.ConvertShellToContext(gameObject);
        }

    }

    public void Interact()
    {
        shellEquip.EquipShell(gameObject);
        interactionBehaviour.RemoveInteractable(gameObject);
    }

    public bool IsInteractable() => shellCheck.CanShellBeEquipped(gameObject);

    public int GetInteractPriority() => 2;


    //public int shellStage = 1;
    //public int stageRequired = 1;

    //public int foodMin;
    //public int foodMax;

    //public string tempProperty;

    //[Space(10)]
    //public bool changesSize = false;
    //public float sizeChange = 1f;

    //[Space(10)]
    //public bool changesSpeed = false;
    //public float speedMultipler = 1f;

    //public float jumpMaxTime = 0.3f;
    //public float jumpMultiplier = 0.5f;
    //public float jumpUpReduction = 8f;

    //[Header("Override")]
    //public bool overrideTransformLocal = false;
    //public Vector3 overridePosLocal = new();
    //public Vector3 overrideEulerLocal = new();

    //public string description;

    //[Space(10)]
    //public Transform wearTarget;
    //public Transform hideTarget;

    //private void Start()
    //{
    //    //if (description == "")
    //    //{
    //    //    description = transform.name;
    //    //}

    //    //if (!bypassRig) //needs logic for reassigning the target
    //    //{
    //    //    if (wearTarget != null && hideTarget != null)
    //    //    {
    //    //        shellConnection.SetShellTarget(ShellRefToHide(false);
    //    //    }
    //    //    else
    //    //    {
    //    //        throw new System.ArgumentException("No transform target for shell");
    //    //    }
    //    //}
    //}




}
