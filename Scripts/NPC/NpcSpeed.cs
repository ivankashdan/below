using UnityEngine;

public class NpcSpeed : NpcBase
{
    [SerializeField] float baseSpeed = 1f;

    public void ResetSpeed()
    {
        SetSpeed(baseSpeed);
    }
    public void SetSpeed(float speed)
    {
        agent.speed = speed;
    }
}