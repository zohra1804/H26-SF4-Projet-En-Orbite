// CollisionSatellite.cs
using UnityEngine;

public class CollisionSatellite : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision avec : " + other.gameObject.name);
        // Arrête le satellite
        GetComponent<Satellite>().velocity = Vector3.zero;
    }
}