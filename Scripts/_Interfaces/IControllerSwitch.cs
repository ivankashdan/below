using System;
using UnityEngine;

public interface IControllerSwitch
{
    public void UpdateOnControllerSwitch();

}

public interface ITutorialPrompt
{
    bool IsActive();
}

public interface IPromptUI
{
    event Action TargetActive;
    event Action TargetInactive;
    bool IsActive();

    Sprite GetSprite();
    string GetText();
    int GetPriority();
}