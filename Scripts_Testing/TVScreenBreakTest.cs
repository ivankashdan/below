using Unity.VisualScripting;
using UnityEngine;

public class TVScreenBreakTest : MonoBehaviour
{
    public GameObject tvBack;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
