using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BestiaryPrompt : PromptUI, IControllerSwitch
{
    public Image selectIcon;
    public Image exitIcon;

    public override void UpdateOnControllerSwitch()
    {
        if (MenuManager.Instance.IsMenuOpen(menu))
        {
            if (ControllerSwitchBehaviour.Instance.IsSchemeActive(ControlScheme.Keyboard))
            {
                exitIcon.sprite = controlSchemeUIManager.currentSprites.bestiary;
                selectIcon.sprite = controlSchemeUIManager.currentSprites.select;
            }
            else if (ControllerSwitchBehaviour.Instance.IsSchemeActive(ControlScheme.Xbox360))
            {
                exitIcon.sprite = controlSchemeUIManager.currentSprites.escape;
                selectIcon.sprite = controlSchemeUIManager.currentSprites.move;
            }

            float smallLargeHeight = 85f;

            promptUIManager.ResizeUI(exitIcon, smallLargeHeight);
            promptUIManager.ResizeUI(selectIcon, smallLargeHeight);
        }
    }


}
