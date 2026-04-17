// CameraManager.cs
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera[] cameras;
    private int currentCamera = 0;

    void Start()
    {
        // Active seulement la première caméra
        for (int i = 0; i < cameras.Length; i++)
            cameras[i].gameObject.SetActive(i == 0);
    }

    void Update()
    {
        // Appuie sur C pour switcher
        if (Input.GetKeyDown(KeyCode.C))
        {
            cameras[currentCamera].gameObject.SetActive(false);
            currentCamera = (currentCamera + 1) % cameras.Length;
            cameras[currentCamera].gameObject.SetActive(true);
        }
    }
}