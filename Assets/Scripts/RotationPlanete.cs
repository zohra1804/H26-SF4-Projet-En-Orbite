using UnityEngine;

public class RotationPlanete : MonoBehaviour
{
    [Header("Durée d'une rotation complète (en secondes)")]
    public float dureeRotation = 10f;

    [Header("Axe de rotation")]
    public Vector3 axeRotation = Vector3.up;

    void Update()
    {
        if (dureeRotation <= 0f)
            return;

        float vitesse = 360f / dureeRotation;
        transform.Rotate(axeRotation * vitesse * Time.deltaTime);
    }
}