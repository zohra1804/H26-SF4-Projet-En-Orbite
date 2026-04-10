// Gravity.cs
using UnityEngine;
using System.Collections.Generic;

public class Gravity : MonoBehaviour
{
    public float G = 0.5f;
    private List<CelestialBody> bodies = new List<CelestialBody>();
    private Satellite satellite;

    void Start()
    {
        bodies.AddRange(FindObjectsByType<CelestialBody>(FindObjectsSortMode.None));
        satellite = FindFirstObjectByType<Satellite>();
        if (satellite == null)
            Debug.LogError("Satellite introuvable !");
    }

    void FixedUpdate()
    {
        if (satellite == null) return;

        foreach (CelestialBody body in bodies)
        {
            // On skip le satellite lui-même
            if (body == satellite) continue;

            Vector3 direction = body.Position - satellite.Position;
            float distance = direction.magnitude;
            if (distance < 1f) continue;

            float force = G * body.mass * satellite.mass / (distance * distance);
            satellite.velocity += direction.normalized * force / satellite.mass * Time.fixedDeltaTime;
        }

        // Déplace le satellite
        satellite.transform.position += satellite.velocity * Time.fixedDeltaTime;
    }
}