using System.Collections.Generic;
using UnityEngine;

public class GameObjectMemory : MemoryTextModel
{
    public GameObject image;

    public override void Interact()
    {
        base.Interact();

        SetPanel(true);
    }

    public override void CloseMemory()
    {
        base.CloseMemory();

        SetPanel(false);
    }
    void SetPanel(bool value)
    {
        image.SetActive(value);
    }

  


}
