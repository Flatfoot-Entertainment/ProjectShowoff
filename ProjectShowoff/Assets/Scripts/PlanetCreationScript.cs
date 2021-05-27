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

    [SerializeField] private RectTransform hitMarker;
    [SerializeField] private CanvasScaler scaler;

    [SerializeField] private GameObject planetUIContainer;
    [SerializeField] private Transform ordersParent;

    private float minPlanetScale, maxPlanetScale;


    private void Awake()
    {
        GetComponent<BoxCollider>().enabled = false;
    }

    private void OnDrawGizmos()
    {
        volumeBounds.center = GetComponent<BoxCollider>().bounds.center;
        volumeBounds.extents = GetComponent<BoxCollider>().bounds.extents;
        //transform.position = volumeBounds.center;
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, 0.5f);
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
        SetupPlanetUI(planet.GetComponent<Planet>());
        ConnectPlanetUI(transform.childCount - 1); //this can break so easily...
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

    private void ClearPreviousPlanetUI()
    {
        if (ordersParent.childCount > 0)
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                GameObject planetUI = ordersParent.GetChild(i).gameObject;
#if UNITY_EDITOR
                Debug.Log("Removed planet: " + planetUI);
                DestroyImmediate(planetUI);
#endif
                if (Application.isPlaying)
                {
                    Destroy(planetUI);
                }
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

    private void SetupPlanetUI(Planet planet)
    {
        planet.HitMarker = hitMarker;
        planet.Scaler = scaler;


        GameObject uiContainer = Instantiate(planetUIContainer, ordersParent.position, Quaternion.identity, ordersParent);
        uiContainer.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = planet.name;
    }

    private void ConnectPlanetUI(int index)
    {
        PlanetUI planetUI = transform.GetChild(index).GetComponent<PlanetUI>();
        //TODO jesus christ...
        TextMeshProUGUI fuelText = ordersParent.GetChild(index).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI mechanicalText = ordersParent.GetChild(index).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI medicineText = ordersParent.GetChild(index).GetChild(1).GetChild(2).GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI foodText = ordersParent.GetChild(index).GetChild(1).GetChild(3).GetComponent<TextMeshProUGUI>();

        planetUI.FoodText = foodText;
        planetUI.FuelText = fuelText;
        planetUI.MechanicalText = mechanicalText;
        planetUI.MedicineText = medicineText;
    }

    public void CreatePlanets()
    {
        ClearPreviousPlanetUI();
        ClearPreviousPlanets();
        AddPlanets();
    }
}
