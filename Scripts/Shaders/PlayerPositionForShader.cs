using UnityEngine;

public class PlayerPositionForShader : MonoBehaviour
{
    public float verticalOffset = 1f;

    void Update()
    {
        Shader.SetGlobalVector("_Player", transform.position);// + Vector3.up * verticalOffset);
    }
}
