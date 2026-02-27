using UnityEngine;
public class CelestialBody : MonoBehaviour
{
    public float mass;
    public Vector3 velocity;

    public Vector3 Position => transform.position;
}