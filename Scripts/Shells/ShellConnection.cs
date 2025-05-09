using UnityEngine;

public class ShellConnection : MonoBehaviour
{
    
    [HideInInspector] public Transform shellTargetReference;

    public bool bypassConnection;

    Transform defaultTarget;
    Transform looseTarget;

    ShellCheck shellCheck;
    Anatomy anatomy;

    private void Awake()
    {
        shellCheck = FindAnyObjectByType<ShellCheck>();
        anatomy = FindAnyObjectByType<Anatomy>();
        defaultTarget = anatomy.GetShellTarget();
        looseTarget = anatomy.GetShellTarget();  //will need to update if using this method
    }

    private void Start()
    {
        if (defaultTarget != null && looseTarget != null)
        { 
            SetShellTargetToDefault();
        }
        else
        {
            throw new System.ArgumentException("No transform target for shell");
        }
    }

    private void LateUpdate()
    {
        if (Time.timeScale == 1 && !bypassConnection)
        {
            MaintainConnectionToRig();
        }
    }

    void MaintainConnectionToRig()
    {
        GameObject shell = shellCheck.GetShell();

        if (shell != null)
        {
            shell.transform.position = shellTargetReference.transform.position;

            //Vector3 adjustedRotationForBlender = new Vector3(90, 0, 0);
            //Vector3 shellTargetRotation = shellTargetReference.transform.eulerAngles;
            //adjustedRotationForBlender += shellTargetRotation;

            shell.transform.rotation = shellTargetReference.transform.rotation * Quaternion.Euler(90, 0, 0);

            //shell.transform.rotation = Quaternion.Euler(adjustedRotationForBlender);
        }
    }

    public bool IsShellTargetLoose => shellTargetReference == looseTarget;
    public void SetShellTargetToLoose() => SetShellTarget(looseTarget);
    public void SetShellTargetToDefault() => SetShellTarget(defaultTarget);

    public void SetShellTarget(Transform target)
    {
        shellTargetReference = target;
    }
 
  

   

}
