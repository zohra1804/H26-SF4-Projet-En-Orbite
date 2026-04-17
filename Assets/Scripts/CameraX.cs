// CameraX.cs
using UnityEngine;

public class CameraX : MonoBehaviour
{
    public Transform target;
    public float distance = 50f;
    public float hauteur = 20f;

    void LateUpdate()
    {
        if (target == null) return;

        // ✅ Vue de côté sur l'axe X
        transform.position = new Vector3(
            target.position.x,
            target.position.y + hauteur,
            target.position.z - distance
        );

        transform.LookAt(target);
    }
}