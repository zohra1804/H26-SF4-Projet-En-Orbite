// CameraXY.cs
using UnityEngine;

public class CameraXY : MonoBehaviour
{
    public Transform target;
    public float distance = 50f;
    public float hauteur = 45f;

    void LateUpdate()
    {
        if (target == null) return;

        // ✅ Vue de côté avec hauteur
        transform.position = new Vector3(
            target.position.x + distance,
            target.position.y + hauteur,
            target.position.z
        );

        // ✅ Regarde toujours vers le satellite
        transform.LookAt(target);
    }
}