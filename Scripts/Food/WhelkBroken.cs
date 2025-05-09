using Unity.VisualScripting;
using UnityEngine;

public class WhelkBroken : MonoBehaviour
{
    public GameObject whelk;

    public GameObject pieces;




    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player collided with broken whelk shell");
            if (whelk != null)
            {
                Destroy(whelk);

                if (pieces != null)
                {
                    pieces.SetActive(true);

                    foreach (Transform piece in pieces.transform)
                    {
                        Rigidbody rb = piece.GetComponent<Rigidbody>();
                        if (rb != null)
                        {
                            rb.isKinematic = false;
                            rb.useGravity = true;
                        }
                    }
                }

                PointOfInterest.Discover(true, gameObject);

                BestiaryManager.AddEntry(GetComponent<BestiaryEntry>().reference);


            }
        }


    }






}
