using UnityEngine;
public class Satellite : CelestialBody
{
    public float fuel = 100f;

    /// <summary>
    /// Applique une impulsion de vitesse (manœuvre orbitale)
    /// </summary>
    public void ApplyImpulse(Vector3 deltaV)
    {
        if (fuel <= 0) return;
        velocity += deltaV;
        fuel -= deltaV.magnitude; // coût en carburant proportionnel
    }
}