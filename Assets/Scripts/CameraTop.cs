// CameraTop.cs
using UnityEngine;

public class CameraTop : MonoBehaviour
{
    public Transform target;
    public float hauteur = 100f;

    void LateUpdate()
    {
        if (target == null) return;

        transform.position = new Vector3(
            target.position.x,
            target.position.y + hauteur,
            target.position.z
        );

        // ✅ Force la caméra à regarder vers le bas
        transform.rotation = Quaternion.Euler(90f, 0f, 0f);
    }
}