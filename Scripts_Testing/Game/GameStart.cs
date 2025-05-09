using NewFeatures;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public StartPoint startPoint;
    public ShellReference defaultShell;
    //public bool previewPosition; //add logic


    Transform playerObject;
    FoodSystem foodSystem;
    ShellCheck shellCheck;
    ShellEquip shellEquip;
    ScaleControl scaleControl;

    private void Awake()
    {
        playerObject = GameObject.FindWithTag("Player").transform;
        foodSystem = FindAnyObjectByType<FoodSystem>();
        shellCheck = FindAnyObjectByType<ShellCheck>();
        shellEquip = FindAnyObjectByType<ShellEquip>();
        scaleControl = FindAnyObjectByType<ScaleControl>();


        bool isStartPoint = startPoint != null;
        //foodSystem.startFoodOverriden = isStartPoint;
        scaleControl.startStageOverriden = isStartPoint;
    }


    private void Start()
    {
        if (startPoint != null)
        {
            //check values are allowed

            playerObject.transform.position = startPoint.transform.position;

            //assign camera

            AssignShell();

            //foodSystem.SetFood(startPoint.food);
            //foodSystem.Molt(startPoint.stage);

            DestroyTrash();
        }
    }


    void AssignShell()
    {
        //GameObject copiedShell;

        //if (shellCheck.IsAShellEquipped())
        //{
        //    Destroy(shellCheck.GetShell());
        //}

        if (startPoint.shellReference != null)
        {
            shellEquip.EquipShell(startPoint.shellReference.gameObject);
        }
        else if (defaultShell != null)
        {
            shellEquip.EquipShell(defaultShell.gameObject);
        }


        //if (startPoint.shellReference != null)
        //{
        //    copiedShell = Instantiate(startPoint.shellReference.gameObject);
        //    Destroy(startPoint.shellReference.gameObject);
        //}
        //else
        //{
        //    copiedShell = Instantiate(defaultShell.gameObject);
        //    Destroy(defaultShell.gameObject);
        //}
      
        //if (shellCheck.IsAShellEquipped())
        //{
        //    Destroy(shellCheck.GetShell());
        //}

        //if (copiedShell != null)
        //{
        //    shellEquip.EquipShell(copiedShell);
        //}
    }
    
    void DestroyTrash()
    {
        foreach (GameObject item in startPoint.trash)
        {
            Destroy(item);
        }
    }


}
