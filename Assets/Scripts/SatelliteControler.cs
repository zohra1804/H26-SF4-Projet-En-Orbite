// SatelliteController.cs
using UnityEngine;

public class SatelliteController : MonoBehaviour
{
    public float puissance = 1f;
    private Satellite sat;

    void Start()
    {
        sat = GetComponent<Satellite>();
    }

    void Update()
    {
        if (sat == null) return;

        Vector3 forward = sat.velocity.normalized;
        if (forward.sqrMagnitude < 0.001f) forward = Vector3.forward;

        Vector3 referenceUp = (Mathf.Abs(Vector3.Dot(forward, Vector3.up)) > 0.99f)
            ? Vector3.forward : Vector3.up;

        Vector3 right = Vector3.Cross(forward, referenceUp).normalized;
        Vector3 up = Vector3.Cross(right, forward).normalized;

        if (right.sqrMagnitude < 0.001f || up.sqrMagnitude < 0.001f) return;

        if (Input.GetKey(KeyCode.UpArrow))
            sat.ApplyImpulse(forward * puissance * Time.deltaTime);
        if (Input.GetKey(KeyCode.DownArrow))
            sat.ApplyImpulse(-forward * puissance * Time.deltaTime);
        if (Input.GetKey(KeyCode.LeftArrow))
            sat.ApplyImpulse(-right * puissance * Time.deltaTime);
        if (Input.GetKey(KeyCode.RightArrow))
            sat.ApplyImpulse(right * puissance * Time.deltaTime);
        if (Input.GetKey(KeyCode.Space))
            sat.ApplyImpulse(up * puissance * Time.deltaTime);
        if (Input.GetKey(KeyCode.LeftShift))
            sat.ApplyImpulse(-up * puissance * Time.deltaTime);
    }
}