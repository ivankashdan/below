using UnityEngine;

public class FoodVFX : MonoBehaviour
{
    public GameObject eatingVFXParent;

    PlayerManager playerManager;
    Transform player;

    //float offset = 2f;

    private void Awake()
    {
        playerManager = FindAnyObjectByType<PlayerManager>();
        player = playerManager.playerObject.transform;
    }

    public void PlayEatingVFX(Transform food)
    {
        eatingVFXParent.transform.position = food.position;

        //food.GetPositionAndRotation(out var foodPosition, out var foodRotation);
        //Vector3 direction = (player.position - foodPosition).normalized;
        //Vector3 offsetPosition = foodPosition + direction * offset;
        //eatingVFXParent.transform.SetPositionAndRotation(offsetPosition, foodRotation);

        eatingVFXParent.transform.LookAt(player);

        foreach (Transform child in eatingVFXParent.transform)
        {
            if (child.TryGetComponent(out ParticleSystem particleSystem))
                particleSystem.Play();
        }
    }
}
