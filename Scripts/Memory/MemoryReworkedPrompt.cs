using UnityEngine;
using UnityEngine.UI;

public class MemoryReworkedPrompt : PromptUI, IControllerSwitch
{
    public Image readIcon;
    public Image exitIcon;

    public override void UpdateOnControllerSwitch()//
    {
        if (MenuManager.Instance.IsMenuOpen(menu))
        {
            exitIcon.sprite = controlSchemeUIManager.currentSprites.exit;
            readIcon.sprite = controlSchemeUIManager.currentSprites.read;

            promptUIManager.ResizeUI(exitIcon);
            promptUIManager.ResizeUI(readIcon);
        }
    }
}
