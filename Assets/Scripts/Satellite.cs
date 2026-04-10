// Satellite.cs
using UnityEngine;

public class Satellite : CelestialBody
{
    public float fuel = 100f;

    public void ApplyImpulse(Vector3 deltaV)
    {
        if (fuel <= 0) return;
        velocity += deltaV;
        fuel -= deltaV.magnitude;
    }
}