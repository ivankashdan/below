using UnityEngine;

public class BestiaryTest : MonoBehaviour
{
    public bool openAtStart;

    public BestiaryScriptableObject
        barnacles, sponge, worm, wrasse, hermit,
        octopus, krill, anemone, urchin, shell, phytoplankton;


    
    void Start()
    {

     
        BestiaryManager.AddEntry(barnacles);
        BestiaryManager.AddEntry(sponge);
        BestiaryManager.AddEntry(worm);
        BestiaryManager.AddEntry(wrasse);
        BestiaryManager.AddEntry(hermit);
        BestiaryManager.AddEntry(octopus);
        BestiaryManager.AddEntry(krill);
        BestiaryManager.AddEntry(anemone);
        BestiaryManager.AddEntry(urchin);
        BestiaryManager.AddEntry(shell);
        BestiaryManager.AddEntry(phytoplankton);

        if (openAtStart)
        {
            MenuManager.Instance.OpenMenu(MenuManager.Menu.Bestiary);
        }

    }



}
