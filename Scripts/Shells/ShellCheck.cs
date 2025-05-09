using NewFeatures;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellCheck : MonoBehaviour
{
    FoodSystem foodSystem;
    ScaleControl scaleControl;
    StageSystem stageSystem; 

    enum Fit
    {
        TooLarge,
        Larger,
        Same,
        Smaller,
        TooSmall
    }

    private void Awake()
    {
        foodSystem = FindAnyObjectByType<FoodSystem>();
        scaleControl = FindAnyObjectByType<ScaleControl>();
        stageSystem = FindAnyObjectByType<StageSystem>();

    }


    public GameObject GetShell() //currently based on "Shell" tag
    {

        foreach (Transform child in transform)
        {
            if (child.tag == "Shell")
            {
                return child.gameObject;
            }
        }

        return null;
    }

    public ShellReference GetShellRef() => GetShellRef(GetShell());
    public ShellReference GetShellRef(GameObject shell)
    {
        

        ShellReference shellRef = shell.GetComponent<ShellReference>();

        if (shellRef == null)
        {
            throw new MissingComponentException("No shell reference available");
        }

        else
        {
            return shellRef;
        } 
    }

    public bool DoesShellHaveGlideAbility()
    {
        if (IsAShellEquipped())
        {
            //Debug.Log("Does shell have glide ability: " + GetShellRef(GetShell()).ability == ShellReference.Ability.glide.ToString());
            return GetShellRef(GetShell()).ability == ShellReference.Ability.glide;
        }
        return false;
    }

    public bool IsAShellEquipped()
    {
        if (GetShell() != null)
        {
            return true;
        }
        return false;
    }

    public bool IsShellEquipped(GameObject other)
    {
        GameObject shell = GetShell();
        if (shell != null)
        {
            if (shell.gameObject == other.gameObject)
            {
                return true;
            }
        }
        return false;
    }

    public bool DoesShellFit(GameObject shell)
    {
        if (IsAShellEquipped()) 
        {
            int shellStage = GetShellRef(shell).stage;
            int currentStage = GetShellRef().stage;
            int interval = stageSystem.scentVisibleInterval;

            return MathF.Abs(shellStage - currentStage) <= interval;
        }
        else
        {
            Debug.LogError("No shell equipped");
            return false;
        }
            
        //return scaleControl.GetScale < shellRef.stageRequired;

        //float currentFood = foodSystem.GetTotalFood;

        //return currentFood < shellRef.foodMin;
    }


    public bool IsNextBiggest(GameObject shell)
    {
        if (GetFit(shell) == Fit.Larger)
        {
            return true;
        }
        return false;
    }

    Fit GetFit(GameObject shell)
    {
        if (IsAShellEquipped())
        {
            int currentStage = GetShellRef().stage;
            int shellStage = GetShellRef(shell).stage;
            int interval = stageSystem.scentVisibleInterval;

            if (shellStage > currentStage + interval) return Fit.TooLarge;
            else if (shellStage > currentStage) return Fit.Larger;
            else if (shellStage == currentStage) return Fit.Same;
            else if (shellStage < currentStage - interval) return Fit.TooSmall;
            else return Fit.Smaller;
        }
        throw new SystemException("No shell equipped");
    }
   

    public bool CanShellBeEquipped(GameObject shell)
    {
        if (IsAShellEquipped() && !IsShellEquipped(shell) && DoesShellFit(shell))
        {
            return true;
        }
        return false;
    }


    //public int GetShellMinFood()
    //{
    //    if (IsAShellEquipped())
    //    {
    //        return GetShellMinFood(GetShell());
    //    }
    //    throw new SystemException("No shell equipped");

    //}
    //public int GetShellMaxFood()
    //{
    //    if (IsAShellEquipped())
    //    {
    //        return GetShellMaxFood(GetShell());
    //    }
    //    throw new SystemException("No shell equipped");
    //}

    //public int GetShellMinFood(GameObject shell)
    //{
    //    return GetShellRef(shell).foodMin;
    //}



    //public int GetShellMaxFood(GameObject shell)
    //{
    //    return GetShellRef(shell).foodMax;
    //}

    public bool IsShellBreakable()
    {
        if (IsAShellEquipped())
        {
            return (GetShellRef().brokenShell != null);
        }
        return false;
    }

    //public bool CanIEatMore()
    //{
    //    GameObject shell = GetShell();
    //    if (shell != null)
    //    {
    //        ShellReference shellRef = GetShellRef(shell);
    //        float maxScale = shellRef.maxScale;
    //        float currentScale = scaleControl.GetScale();

    //        if (currentScale >= maxScale) return false;
    //    }
    //    return true;
    //}


    //public bool DoesShellFitSmaller(GameObject shell)
    //{
    //    ShellReference shellRef = GetShellRef(shell);

    //    int shellStage = shellRef.stage;
    //    int currentStage = scaleControl.GetStage();

    //    int roomToGrow = 1;

    //    return shellStage == currentStage - roomToGrow;
    //}

    //public bool DoesShellFitSame(GameObject shell)
    //{
    //    ShellReference shellRef = GetShellRef(shell);

    //    int shellStage = shellRef.stage;
    //    int currentStage = scaleControl.GetStage();

    //    return shellStage == currentStage;
    //}


    //public enum ShellFit
    //{
    //    sizeDown,
    //    sizeSame,
    //    sizeUp,
    //    sizeWrong
    //}

    //public ShellFit ShellFitQuality(GameObject shell) //needs DoesShellFit check to pass
    //{
    //    ShellReference shellRef = GetShellRef(shell);

    //    int shellStage = shellRef.stage;
    //    int currentStage = scaleControl.GetStage();

    //    if (shellStage < currentStage) return ShellFit.sizeDown;
    //    else if (shellStage == currentStage) return ShellFit.sizeSame;
    //    else return ShellFit.sizeUp;
    //}
}