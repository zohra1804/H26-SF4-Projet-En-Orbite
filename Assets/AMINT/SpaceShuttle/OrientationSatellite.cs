// OrientationSatellite.cs
using UnityEngine;

public class OrientationSatellite : MonoBehaviour
{
    private Vector3 lastPosition;

    void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        Vector3 direction = transform.position - lastPosition;

        if (direction.sqrMagnitude > 0.0001f)
        {
            transform.rotation = Quaternion.LookRotation(direction.normalized);
        }

        lastPosition = transform.position;
    }
}