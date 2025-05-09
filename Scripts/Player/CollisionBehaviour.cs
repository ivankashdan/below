using UnityEngine;

public class CollisionBehaviour : MonoBehaviour
{
    public float pushForce = 10f; // Adjust push strength
    public bool applyUpwardForce = true; // Toggle vertical push
    public float upwardModifier = 0.5f; // Controls how much force is applied upwards

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.rigidbody;

        if (rb != null) // Ensure the object has a Rigidbody
        {
            Vector3 pushDirection = collision.transform.position - transform.position;
            pushDirection.Normalize(); // Get direction without magnitude

            if (applyUpwardForce)
            {
                pushDirection.y += upwardModifier; // Add vertical force
            }

            rb.AddForce(pushDirection * pushForce, ForceMode.Impulse);
        }
    }
}
