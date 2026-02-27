using UnityEngine;
using System.Collections.Generic;

public class Gravity : MonoBehaviour
{
    public static readonly float G = 6.674e-11f;

    private List<Planet> planets = new List<Planet>();
    private Satellite satellite;

    void Start()
    {
        planets.AddRange(FindObjectsByType<Planet>(FindObjectsSortMode.None));
        satellite = FindFirstObjectByType<Satellite>();
    }

    void FixedUpdate()
    {
        ApplyGravityPerPlanet(satellite);
        UpdateSatellitePosition();
    }

    public void ApplyGravityPerPlanet(Satellite sat)
    {
        foreach (Planet planet in planets)
        {
            Vector3 force = ComputeGravityFrom(planet, sat);
            Vector3 acceleration = force / sat.mass;
            sat.velocity += acceleration * Time.fixedDeltaTime;
        }
    }

    public Vector3 ComputeGravityFrom(CelestialBody body, Satellite sat)
    {
        Vector3 direction = body.Position - sat.Position;
        float distance = direction.magnitude;
        if (distance < 0.1f) return Vector3.zero;
        float forceMagnitude = G * body.mass * sat.mass / (distance * distance);
        return direction.normalized * forceMagnitude;
    }

    private void UpdateSatellitePosition()
    {
        satellite.transform.position += satellite.velocity * Time.fixedDeltaTime;
    }
}