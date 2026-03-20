using UnityEngine;

public class PlacerSatellite : MonoBehaviour
{
    public Transform terre;
    public float altitude = 0.1f;

    void Start()
    {
        if (terre == null)
        {
            Debug.LogError("Terre non assignée !");
            return;
        }

        // Rayon réel basé sur le scale
        float rayonTerre = terre.localScale.x / 2f;

        // Direction (au-dessus)
        Vector3 direction = (transform.position - terre.position).normalized;

        // Si jamais direction = 0 (cas fréquent)
        if (direction == Vector3.zero)
        {
            direction = Vector3.up;
        }

        transform.position = terre.position + direction * (rayonTerre + altitude);
    }
}