using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class CurrentState : AbstractState
{
    public event Action<Current, bool> CurrentAdded;
    public event Action CurrentRemoved;

    protected float waterSpeed = 10f;
    protected Current waterCurrent;
    protected Vector3 waterDirection;
    protected bool cameraTurn;


    protected bool breakingFromCurrent;
    public bool IsBreakingFromCurrent() => breakingFromCurrent;
    public bool SetBreakingFromCurrent(bool breaking) => breakingFromCurrent = breaking;
    public bool IsHermieInCurrent() => waterCurrent != null;
    public bool IsCameraTurn => cameraTurn;

    public virtual void AddCurrent(Current current)
    {
        waterCurrent = current;
        waterDirection = current.GetWaterDirection();
        waterSpeed = current.waterSpeed;
        cameraTurn = current.cameraTurn;
        CurrentAdded?.Invoke(current, current.cameraTurn);
    }
    public virtual void RemoveCurrent(Current current)
    {
        if (waterCurrent == current)
        {
            waterCurrent = null;
            CurrentRemoved?.Invoke();
        }
    }

} 