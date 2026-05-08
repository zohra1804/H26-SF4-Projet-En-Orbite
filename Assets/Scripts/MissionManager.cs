using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class MissionManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject canvasMenu;
    public TMP_Dropdown dropdown;
    public Button confirmerButton;

    [Header("Références")]
    public Satellite satellite;
    public Transform terre;
    public Gravity gravity;

    [Header("Planètes")]
    public Transform mercure;
    public Transform venus;
    public Transform mars;
    public Transform jupiter;
    public Transform saturne;
    public Transform uranus;
    public Transform neptune;
    public Transform pluton;

    private Transform cible;
    private enum EtatMission { AtTerre, VersOrbite, EnOrbite, RetourTerre }
    private EtatMission etat = EtatMission.AtTerre;

    void Start()
    {
        Time.timeScale = 0f;
        canvasMenu.SetActive(true);
        confirmerButton.onClick.AddListener(DemarrerMission);
    }

    void DemarrerMission()
    {
        string nom = dropdown.options[dropdown.value].text.ToLower().Trim();

        cible = nom switch
        {
            "mercure" => mercure,
            "venus" or "vénus" => venus,
            "mars" => mars,
            "jupiter" => jupiter,
            "saturne" => saturne,
            "uranus" => uranus,
            "neptune" => neptune,
            "pluton" => pluton,
            _ => null
        };

        if (cible == null)
        {
            return;
        }

        canvasMenu.SetActive(false);
        Time.timeScale = 1f;
        etat = EtatMission.VersOrbite;
        StartCoroutine(GererMission());
    }
    IEnumerator GererMission()
    {
        float distanceCible = Vector3.Distance(terre.position, cible.position);
        float tempsOrbite = Mathf.Clamp(distanceCible / 50f, 3f, 20f);

        // Phase 1 — Orbite autour de la Terre
        etat = EtatMission.VersOrbite;
        float angleAccumule = 0f;
        float toursNecessaires = Mathf.Clamp(distanceCible / 10000f, 1f, 8f);
        float angleTotalCible = 360f * toursNecessaires;

        
        satellite.transform.position = terre.position + new Vector3(15f, 0f, 0f);
        bool tourComplet = false;

        while (true)
        {
            float progression = Mathf.Clamp01(angleAccumule / angleTotalCible);
            float vitesse = Mathf.Lerp(50f, 300f, progression);

            satellite.transform.position = terre.position +
                Quaternion.AngleAxis(vitesse * Time.fixedDeltaTime, Vector3.up) *
                (satellite.Position - terre.position);

            angleAccumule += vitesse * Time.fixedDeltaTime;

            if (angleAccumule >= angleTotalCible)
            {
                Vector3 versCible = (cible.position - satellite.Position).normalized;
                Vector3 versTerre = (terre.position - satellite.Position).normalized;
                Vector3 dirMouvement = Vector3.Cross(versTerre, Vector3.up).normalized;
                float alignement = Vector3.Dot(dirMouvement, versCible);

                if (alignement > 0.8f) break;
            }

            yield return new WaitForFixedUpdate();
        }



        // Phase 2 — Vol vers la planète cible
        Dictionary<string, float> rayons = new Dictionary<string, float>()
        {
            { "Terre", 7f },
            { "Pluton", 1.74f },
            { "Saturne", 13f },
            { "Soleil", 60f },
            { "Mercure", 3f },
            { "Venus", 6f },
            { "Mars", 5f },
            { "Jupiter", 15f },
            { "Uranus", 10f },
            { "Neptune", 9f }
        };

        float rayonCible = rayons.ContainsKey(cible.name) ? rayons[cible.name] : 5f;

        while (Vector3.Distance(satellite.Position, cible.position) > rayonCible + 2f)
        {
            Vector3 direction = (cible.position - satellite.Position).normalized;
            satellite.transform.position += direction * 50f * Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        // Phase 3 — Orbite autour de la planète cible
        etat = EtatMission.EnOrbite;
        float rayonOrbite3 = rayonCible + 2f;
        float angleAccumule3 = 0f;
        float angleTotalCible3 = 360f * 3f;

        satellite.transform.position = cible.position + new Vector3(rayonOrbite3, 0f, 0f);

        while (angleAccumule3 < angleTotalCible3)
        {
            float progression = Mathf.Clamp01(angleAccumule3 / angleTotalCible3);
            float vitesse = Mathf.Lerp(50f, 300f, progression);

            satellite.transform.position = cible.position +
                Quaternion.AngleAxis(vitesse * Time.fixedDeltaTime, Vector3.up) *
                (satellite.Position - cible.position);

            angleAccumule3 += vitesse * Time.fixedDeltaTime;

            yield return new WaitForFixedUpdate();
        }

        // Phase 4 — Retour vers la Terre
        etat = EtatMission.RetourTerre;

        Vector3 departRetour = satellite.Position;
        Vector3 arriveeRetour = terre.position + (departRetour - terre.position).normalized * 9f; // ✅ surface de la Terre

        // ✅ Point de contrôle perpendiculaire à la trajectoire
        Vector3 milieu = (departRetour + arriveeRetour) / 2f;
        Vector3 perpendiculaire = Vector3.Cross(
            (arriveeRetour - departRetour).normalized,
            Vector3.up
        ).normalized;
        Vector3 controleRetour = milieu + perpendiculaire * Vector3.Distance(departRetour, arriveeRetour) * 0.2f;

        float tmps = 0f;
        while (tmps < 1f)
        {
            tmps += Time.fixedDeltaTime * 0.2f;
            tmps = Mathf.Clamp01(tmps);

            Vector3 pos = Mathf.Pow(1 - tmps, 2) * departRetour +
                          2 * (1 - tmps) * tmps * controleRetour +
                          Mathf.Pow(tmps, 2) * arriveeRetour;

            satellite.transform.position = pos;
            yield return new WaitForFixedUpdate();
        }

        // Phase 5 — Retour au menu
        satellite.velocity = Vector3.zero;
        etat = EtatMission.AtTerre;
        Time.timeScale = 0f;
        canvasMenu.SetActive(true);
        dropdown.value = 0;
    }
}