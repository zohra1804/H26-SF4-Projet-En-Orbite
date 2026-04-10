// Orbite.cs
using UnityEngine;

public class Orbite : MonoBehaviour
{
    public Transform soleil;
    public float orbitSpeed = 20f;

    void Update()
    {
        if (soleil != null)
        {
            transform.RotateAround(soleil.position, Vector3.up, orbitSpeed * Time.deltaTime);
        }
    }
}