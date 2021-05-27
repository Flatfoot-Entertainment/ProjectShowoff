using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Events;
using UnityEngine.UI;
using TMPro;

public class PlanetWindow : EditorWindow
{
    private static GameObject objectCenter;
    private GameObject planetPrefab;

    private GameObject planetCanvasComponents;
    private PlanetPrefabSettings planetPrefabSettings; //to replace the one prefab that its used rn
    private bool isPlanetScaleRandom = false;
    private int planetCount = 3;
    private float uniformScale = 2f;
    private float minScale = 1f, maxScale = 4f;

    private RectTransform hitMarker;
    private CanvasScaler scaler;

    private GameObject planetUIContainer;
    private Transform ordersParent;

    private Bounds volumeBounds;

    [MenuItem("Game Tools/Planet Creator")]

    public static void ShowWindow()
    {
        GetWindow<PlanetWindow>(false, "Planet Creator", true);
        GameObject center = new GameObject("Planet Center", typeof(BoxCollider));
        objectCenter = center;
    }

    private void OnGUI()
    {
        //planetPrefabSettings = EditorGUILayout.ObjectField("Planet Prefab Settings", planetPrefabSettings, typeof(PlanetPrefabSettings), false) as PlanetPrefabSettings;
        planetPrefab = EditorGUILayout.ObjectField("Planet Prefab", planetPrefab, typeof(GameObject), false) as GameObject;
        if (planetPrefab)
        {
            planetCount = EditorGUILayout.IntField("Planet Count", planetCount);
            isPlanetScaleRandom = EditorGUILayout.Toggle("Random Planet Scale?", isPlanetScaleRandom);
            if (isPlanetScaleRandom)
            {
                minScale = EditorGUILayout.FloatField("Minimum Scale", minScale);
                maxScale = EditorGUILayout.FloatField("Maximum Scale", maxScale);
            }
            else
            {
                uniformScale = EditorGUILayout.FloatField("Uniform Scale", 1.5f);
            }

            EditorGUILayout.Space(25f);
            EditorGUILayout.LabelField("This is the center of the object (designated as a blue sphere)");
            objectCenter = EditorGUILayout.ObjectField("Planet Center: ", objectCenter, typeof(GameObject), true) as GameObject;

            EditorGUILayout.Space(25f);
            EditorGUILayout.LabelField("The hit marker object that will show when you click on a planet (found in PlanetUI, drag it here)");
            hitMarker = EditorGUILayout.ObjectField("Hit Marker: ", hitMarker, typeof(RectTransform), true) as RectTransform;

            EditorGUILayout.Space(25f);
            EditorGUILayout.LabelField("The Canvas Scaler, for whatever reason it's used (drag the UI object itself)");
            scaler = EditorGUILayout.ObjectField("Canvas Scaler: ", scaler, typeof(CanvasScaler), true) as CanvasScaler;

            EditorGUILayout.Space(25f);
            EditorGUILayout.LabelField("The UI container per planet (must be a prefab)");
            planetUIContainer = EditorGUILayout.ObjectField("Planet UI Container: ", planetUIContainer, typeof(GameObject), false) as GameObject;

            EditorGUILayout.Space(25f);
            EditorGUILayout.LabelField("The Planet Canvas components needed to be able to click on the planets (UI/PlanetUI/PlanetCanvasComponents");
            planetCanvasComponents = EditorGUILayout.ObjectField("Planet Canvas component: ", planetCanvasComponents, typeof(GameObject), true) as GameObject;

            EditorGUILayout.Space(25f);
            EditorGUILayout.LabelField("The order UI container for the planets' UIs (The scene you are on/UI/PersistentUI/GameObject/Orders)");
            ordersParent = EditorGUILayout.ObjectField("Orders Parent: ", ordersParent, typeof(Transform), true) as Transform;

            if (GUILayout.Button("Generate planets"))
            {
                CreatePlanets();
            }
        }
    }

    private void OnInspectorUpdate()
    {
        //for some reason it doesn't update idk, does the user even need to see bounds numbers?
        volumeBounds = objectCenter.GetComponent<BoxCollider>().bounds;
    }
    private void AddPlanet()
    {
        if (!planetPrefab) return;

        Vector3 randomPosition = GetRandomPosition();
        GameObject planet = Instantiate(planetPrefab, randomPosition, Quaternion.identity, objectCenter.transform);
        planet.name = "Planet " + objectCenter.transform.childCount;
        if (isPlanetScaleRandom)
        {
            float randomScale = Random.Range(minScale, maxScale);
            planet.transform.localScale = Vector3.one * randomScale;
        }
        else
        {
            planet.transform.localScale = Vector3.one * uniformScale;
        }
        SetupPlanetUI(planet.GetComponent<Planet>());
        ConnectPlanetUI(objectCenter.transform.childCount - 1); //this can break so easily...
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
        for (int i = objectCenter.transform.childCount - 1; i >= 0; i--)
        {
            GameObject planet = objectCenter.transform.GetChild(i).gameObject;
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
            for (int i = objectCenter.transform.childCount - 1; i >= 0; i--)
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
        planet.PlanetaryShipmentCenter = planetCanvasComponents.GetComponent<PlanetaryShipmentCenter>();

        Debug.Log("trying to add listener...");
        //TODO setup the unity event from the Planet script

        GameObject uiContainer = Instantiate(planetUIContainer, ordersParent.position, Quaternion.identity, ordersParent);
        uiContainer.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = planet.name;
    }

    private void ConnectPlanetUI(int index)
    {
        PlanetUI planetUI = objectCenter.transform.GetChild(index).GetComponent<PlanetUI>();
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

    private void CreatePlanets()
    {
        volumeBounds = objectCenter.GetComponent<BoxCollider>().bounds;
        ClearPreviousPlanetUI();
        ClearPreviousPlanets();
        AddPlanets();
    }
}
