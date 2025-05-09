using NewFeatures;
using UnityEngine;


[ExecuteInEditMode]
public class ShellConversion : MonoBehaviour
{
    public GameObject scent;

    //public Color freshScent;
    public Color wornScent;

    ShellCheck shellCheck;
    ScaleControl scaleControl;
    FoodSystem foodSystem;


    private void OnEnable()
    {
        shellCheck = FindAnyObjectByType<ShellCheck>();
        scaleControl = FindAnyObjectByType<ScaleControl>();
        foodSystem = FindAnyObjectByType<FoodSystem>(); 
    }

    public void ConvertToLooseShell(GameObject shell)
    {
        shell.layer = LayerMask.NameToLayer("Walkable");

        Rigidbody rb = shell.GetComponent<Rigidbody>();

        if (rb == null)
        {
            rb = shell.AddComponent<Rigidbody>();
        }
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
        rb.mass = 50;

        ShellReference shellReference = shell.GetComponent<ShellReference>();
        shellReference.connectedToRig = false;

        if (shell.TryGetComponent<HermieNearCameraFade>(out var cameraFade))
        {
            Destroy(cameraFade);
        }
    }

    public void ConvertToPlayerShell(GameObject shell)
    {
        shell.layer = LayerMask.NameToLayer("Attire");

        if (shell.TryGetComponent(out Rigidbody rb))
        {
            DestroyImmediate(rb);//
        }

        ShellReference shellReference = shell.GetComponent<ShellReference>();
        shellReference.connectedToRig = true;

        if (shell.GetComponent<HermieNearCameraFade>() == null)
        {
            var cameraFade = shell.AddComponent<HermieNearCameraFade>();
            cameraFade.material = shell.GetComponent<MeshRenderer>().material;
        }
    }

 

    public void ConvertShellToContext(GameObject shell)
    {
        BuildShellFoundation(shell);

        if (shellCheck.IsShellEquipped(gameObject))
        {
            ConvertToPlayerShell(shell);
        }
        else
        {
            ConvertToLooseShell(shell);
        }
    }

    void BuildShellFoundation(GameObject shell)
    {
        shell.transform.tag = "Shell";

        MeshCollider rbCollider = shell.GetComponent<MeshCollider>();
        //replace this w/ custom mesh made from primitives found on gameObject. Mesh if none found
        if (rbCollider == null)
        {
            rbCollider = shell.AddComponent<MeshCollider>();
        }
        rbCollider.convex = true;

        RebuildScent(shell);
    }

    void RebuildScent(GameObject shell)
    {
        if (shell.GetComponentInChildren<ShellScent>())
        {
            DestroyImmediate(shell.GetComponentInChildren<ShellScent>().gameObject);
        }
        ShellReference shellReference = shell.GetComponent<ShellReference>();
        GameObject newScent = Instantiate(scent, shell.transform);
        newScent.name = scent.name;
        newScent.transform.localScale = Vector3.one * scaleControl.GetScale;
        //newScent.transform.localScale = Vector3.one * scaleControl.CalculateGFXScaleWithFoodSystem(shellReference.foodMax, foodSystem);
            //GetStageGFXScale(shellReference.stage);
    }



}
