// CelestialBody.cs
using UnityEngine;

public class CelestialBody : MonoBehaviour
{
    public float mass = 1f;
    public Vector3 velocity;
    public Vector3 Position => transform.position;
}