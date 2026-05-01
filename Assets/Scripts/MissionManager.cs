using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class MissionManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject canvasMenu;
    public TMP_InputField inputField;
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
        string nom = inputField.text.ToLower().Trim();

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
            inputField.text = "";
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

        //gravity.enabled = false;

        // Phase 1 — Orbite autour de la Terre
        etat = EtatMission.VersOrbite;
        float angleAccumule = 0f;
        float toursNecessaires = Mathf.Clamp(distanceCible / 10000f, 1f, 8f);
        float angleTotalCible = 360f * toursNecessaires;

        // ✅ Force le satellite à la bonne distance de la Terre dès le départ
        satellite.transform.position = terre.position + new Vector3(15f, 0f, 0f);
        bool tourComplet = false;

        while (true)
        {
            float progression = Mathf.Clamp01(angleAccumule / angleTotalCible);
            float vitesse = Mathf.Lerp(50f, 150f, progression);

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
        while (Vector3.Distance(satellite.Position, cible.position) > 20f)
        {
            Vector3 direction = (cible.position - satellite.Position).normalized;
            satellite.velocity = direction * 50f;
            satellite.transform.position += satellite.velocity * Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        // Phase 3 — Orbite autour de la planète cible
        etat = EtatMission.EnOrbite;
        float debut = Time.time;

        while (Time.time - debut < 10f)
        {
            satellite.transform.position = cible.position +
                Quaternion.AngleAxis(100f * Time.fixedDeltaTime, Vector3.up) *
                (satellite.Position - cible.position);
            yield return new WaitForFixedUpdate();
        }

        // Phase 4 — Retour vers la Terre
        etat = EtatMission.RetourTerre;
        while (Vector3.Distance(satellite.Position, terre.position) > 10f)
        {
            Vector3 direction = (terre.position - satellite.Position).normalized;
            satellite.velocity = direction * 40f;
            satellite.transform.position += satellite.velocity * Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        // Phase 5 — Retour au menu
        satellite.velocity = Vector3.zero;
        etat = EtatMission.AtTerre;
        Time.timeScale = 0f;
        canvasMenu.SetActive(true);
        inputField.text = "";
    }
}