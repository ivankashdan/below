using UnityEngine;

public class BestiaryStart : MonoBehaviour
{
    public BestiaryScriptableObject hermit;
    //public BestiaryScriptableObject barnacles;
    //public BestiaryScriptableObject krill;

    public bool openAtStart;

    void Start()
    {
        BestiaryManager.AddEntry(hermit);
        //BestiaryManager.AddEntry(barnacles);
        //BestiaryManager.AddEntry(krill);

        if (openAtStart)
        {
            MenuManager.Instance.OpenMenu(MenuManager.Menu.Bestiary);
        }
    }

}
