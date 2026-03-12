using UnityEngine;

public class Orbite : MonoBehaviour
{
    public Transform sun; // Glisse ton objet "Soleil" ici dans l'inspecteur
    public float orbitSpeed = 20f;

    void Update()
    {
        if (sun != null)
        {
            // Fait tourner l'objet autour du centre du soleil, 
            // sur l'axe vertical (Y), à une vitesse donnée.
            transform.RotateAround(sun.position, Vector3.up, orbitSpeed * Time.deltaTime);
        }
    }
}
