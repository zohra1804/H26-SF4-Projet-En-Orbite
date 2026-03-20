using UnityEngine;

public class SuivreSatellite : MonoBehaviour
{
    [Header("Cible à suivre")]
    public Transform cible;

    [Header("Réglages caméra")]
    public Vector3 offset = new Vector3(0, 5, -10);
    public float vitesseSuivi = 5f;

    void LateUpdate()
    {
        if (cible == null)
            return;

        // Position souhaitée
        Vector3 positionDesiree = cible.position + offset;

        // Déplacement fluide
        transform.position = Vector3.Lerp(transform.position, positionDesiree, vitesseSuivi * Time.deltaTime);

        // Regarder la cible
        transform.LookAt(cible);
    }
}