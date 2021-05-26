using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider))]
public class PlanetCreationScript : MonoBehaviour
{
    //TODO make it so you add new rows of planet text stuff...right now its only 4
    //TODO make use of bounding boxes so that planets dont spawn too close to each other
    [SerializeField] private float uniformScale;
    [SerializeField] private int planetCount;
    [SerializeField] private bool isPlanetScaleRandom;
    [SerializeField] private GameObject planetPrefab;
    [SerializeField] private Bounds volumeBounds;



    [SerializeField] private TMP_Text foodText;
    [SerializeField] private TMP_Text fuelText;
    [SerializeField] private TMP_Text medicineText;
    [SerializeField] private TMP_Text mechanicalText;

    [SerializeField] private RectTransform hitMarker;
    [SerializeField] private CanvasScaler scaler;

    private float minPlanetScale, maxPlanetScale;


    private void Awake()
    {
        GetComponent<BoxCollider>().enabled = false;
    }
    private void OnEnable()
    {

    }
    private void OnDrawGizmos()
    {
        volumeBounds.center = GetComponent<BoxCollider>().bounds.center;
        volumeBounds.extents = GetComponent<BoxCollider>().bounds.extents;
        //transform.position = volumeBounds.center;
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, 0.5f);
    }

    private void PlacePlanetPositions()
    {

    }
    private void AddPlanet()
    {
        if (!planetPrefab) return;

        Vector3 randomPosition = GetRandomPosition();
        GameObject planet = Instantiate(planetPrefab, randomPosition, Quaternion.identity, transform);
        planet.name = "Planet " + transform.childCount;
        if (isPlanetScaleRandom)
        {
            float randomScale = Random.Range(minPlanetScale, maxPlanetScale);
            planet.transform.localScale = Vector3.one * randomScale;
        }
        else
        {
            planet.transform.localScale = Vector3.one * uniformScale;
        }
        SetupPlanetUI(planet.GetComponent<Planet>(), planet.GetComponent<PlanetUI>());
    }

    private void AddPlanets()
    {
        for (int i = 0; i < planetCount; i++)
        {
            AddPlanet();
        }
    }

    private void ClearPreviousPlanets()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            GameObject planet = transform.GetChild(i).gameObject;
#if UNITY_EDITOR
            Debug.Log("Removed planet: " + planet);
            DestroyImmediate(planet);
#endif
            if (Application.isPlaying)
            {
                Destroy(planet);
            }
        }
    }

    private Vector3 GetRandomPosition()
    {
        float randomXPosition = Random.Range(volumeBounds.min.x, volumeBounds.max.x);
        float randomYPosition = Random.Range(volumeBounds.min.y, volumeBounds.max.y);
        float randomZPosition = Random.Range(volumeBounds.min.z, volumeBounds.max.z);
        Vector3 randomPosition = new Vector3(randomXPosition, randomYPosition, randomZPosition);
        return randomPosition;
    }

    private void SetupPlanetUI(Planet planet, PlanetUI planetUI)
    {
        planet.HitMarker = hitMarker;
        planet.Scaler = scaler;

        if (foodText)
        {
            planetUI.FoodText = foodText;
            planetUI.FuelText = fuelText;
            planetUI.MechanicalText = mechanicalText;
            planetUI.MedicineText = medicineText;
        }
    }

    public void CreatePlanets()
    {
        ClearPreviousPlanets();
        AddPlanets();
    }
}
