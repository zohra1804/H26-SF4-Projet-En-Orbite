// UIManager.cs
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider carburantSlider;
    public Satellite satellite;

    void Start()
    {
        satellite = FindFirstObjectByType<Satellite>();
        carburantSlider.maxValue = satellite.fuel;
        carburantSlider.value = satellite.fuel;
    }

    void Update()
    {
        if (satellite == null) return;
        carburantSlider.value = satellite.fuel;
    }
}