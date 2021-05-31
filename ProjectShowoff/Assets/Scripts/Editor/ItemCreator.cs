using System;
using System.Linq;
using Lean.Pool;
using UnityEditor;
using UnityEngine;
public class ItemCreator : EditorWindow
{
	private enum ItemProperty
	{
		None,
		Destructible,
		Explosive
	}
	
    private ItemSpawningSettings settings;
    private ItemColliderSettings colliderSettings;
    private int objLayer;

    private GameObject modelPrefab;
    private ItemType itemType;
    private bool expensive;
    private string selectedCollider = "";
    private ItemProperty property = ItemProperty.None;
    private float breakForce = 0f;
    private float explosionForce = 0f;
    private float explosionRadius = 0f;
    private GameObject breakableReplacement;
    private Vector3 startingRotation;

    // TODO initial rotation
    // TODO special properties

    [MenuItem("Game Tools/Item Creator")]
    private static void ShowWindow()
    {
        var window = GetWindow<ItemCreator>();
        window.titleContent = new GUIContent("Item Creator");
        window.Show();
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Item Creator");

        settings = EditorGUILayout.ObjectField("Item Spawning Settings", settings, typeof(ItemSpawningSettings), false)
            as ItemSpawningSettings;
        colliderSettings = EditorGUILayout.ObjectField("Collider Settings", colliderSettings, typeof(ItemColliderSettings), false)
            as ItemColliderSettings;

        if (settings && colliderSettings)
        {
            if (colliderSettings.complexColliderPrefabs.Count <= 0 || colliderSettings.simpleColliderPrefabs.Count <= 0)
            {
                EditorGUILayout.HelpBox("The collider settings aren't valid!", MessageType.Warning);
            }
            else
            {
                EditorGUILayout.LabelField("Item Creator");
                modelPrefab = EditorGUILayout.ObjectField("Model", modelPrefab, typeof(GameObject), false)
                    as GameObject;
                itemType = (ItemType)EditorGUILayout.EnumPopup("Item Type", itemType);

                property = (ItemProperty) EditorGUILayout.EnumPopup("Item Property", property);
                if (property == ItemProperty.Destructible || property == ItemProperty.Explosive)
                {
	                breakableReplacement =
		                EditorGUILayout.ObjectField("Replacement for broken object", breakableReplacement, typeof(GameObject), false) as GameObject;
	                breakForce = EditorGUILayout.FloatField("Break Force", breakForce);
	                if (breakForce < 0f) breakForce = 0f;

	                if (property == ItemProperty.Explosive)
	                {
		                explosionForce = EditorGUILayout.FloatField("Explosion Force", explosionForce);
		                if (explosionForce < 0f) explosionForce = 0f;
		                explosionRadius = EditorGUILayout.FloatField("Explosion Radius", explosionRadius);
		                if (explosionRadius < 0f) explosionRadius = 0f;
	                }
                }

                startingRotation = EditorGUILayout.Vector3Field("Starting Rotation", startingRotation);

                expensive = EditorGUILayout.Toggle("Is Expensive", expensive);

                string[] layers = UnityEditorInternal.InternalEditorUtility.layers;
                string layerName = LayerMask.LayerToName(objLayer);
                for (int i = 0; i < layers.Length; i++)
                {
                    if (layerName.Equals(layers[i], StringComparison.Ordinal))
                    {
                        objLayer = i;
                        break;
                    }
                }

                int newLayerIndex = EditorGUILayout.Popup("Item Layer", objLayer, layers);
                string newLayerName = layers[newLayerIndex];
                objLayer = LayerMask.NameToLayer(newLayerName);

                if (expensive)
                {
                    var shapes = colliderSettings.complexColliderPrefabs.Keys.ToArray();
                    int index = IndexOf(shapes, selectedCollider);
                    int newIndex = EditorGUILayout.Popup("Collider shape", index, shapes);
                    newIndex = Mathf.Clamp(newIndex, 0, shapes.Length - 1);
                    string newLayer = shapes[newIndex];

                    if (!selectedCollider.Equals(newLayer, StringComparison.Ordinal))
                    {
                        selectedCollider = shapes[newIndex];
                    }
                }
                else
                {
                    var shapes = colliderSettings.simpleColliderPrefabs.Keys.ToArray();
                    int index = IndexOf(shapes, selectedCollider);
                    int newIndex = EditorGUILayout.Popup("Collider shape", index, shapes);
                    newIndex = Mathf.Clamp(newIndex, 0, shapes.Length - 1);
                    string newLayer = shapes[newIndex];

                    if (!selectedCollider.Equals(newLayer, StringComparison.Ordinal))
                    {
                        selectedCollider = shapes[newIndex];
                    }
                }

                bool guiEnabled = GUI.enabled;
                GUI.enabled = modelPrefab;
                if (GUILayout.Button("Create!"))
                {
                    AddNewItem();
                }

                GUI.enabled = guiEnabled;
            }
        }
        else
        {
            EditorGUILayout.HelpBox("There has to be a settings object to adjust", MessageType.Warning);
        }
    }

    private void AddNewItem()
    {
        GameObject colliderPrefab;
        try
        {
            colliderPrefab = expensive
                ? colliderSettings.complexColliderPrefabs[selectedCollider]
                : colliderSettings.simpleColliderPrefabs[selectedCollider];
        }
        catch (Exception e)
        {
            Debug.LogError($"Could not find collider type with that identifier ({e})");
            return;
        }

        string fileLocation =
            EditorUtility.SaveFilePanel("Save new item", Application.dataPath, modelPrefab.name, "prefab");
        if (fileLocation == null) return;

        // Instantiate a copy of the prefab to later save it to
        GameObject obj = PrefabUtility.InstantiatePrefab(colliderPrefab) as GameObject;

        Instantiate(modelPrefab, obj.transform).transform.position = Vector3.zero;
        // Add a Rigidbody and set the collision detection mode
        obj.AddComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
        // Add a LeanPooledRigidBody to work correctly with LeanPool
        obj.AddComponent<LeanPooledRigidbody>();

        ItemRotationScript rotationScript = obj.AddComponent<ItemRotationScript>();
        rotationScript.boxRotation = startingRotation;
        // Set the layer to the one requested
        obj.layer = objLayer;

        obj.AddComponent<ItemScript>();

        if (property == ItemProperty.Destructible)
        {
	        ItemPropertyHandler h = obj.AddComponent<ItemPropertyHandler>();
	        h.maxForce = breakForce;
	        h.replacedBy = breakableReplacement;
        }
        else if (property == ItemProperty.Explosive)
        {
	        ExplosiveItemPropertyHandler h = obj.AddComponent<ExplosiveItemPropertyHandler>();
	        h.maxForce = breakForce;
	        h.replacedBy = breakableReplacement;
	        h.force = explosionForce;
	        h.radius = explosionRadius;
        }

        // Save the prepared object as a new prefab at the requested location
        GameObject saved = PrefabUtility.SaveAsPrefabAsset(obj, fileLocation, out bool success);
        // Destroy the object again, because there's no way to
        // clone a GameObject without instantiating in the scene
        DestroyImmediate(obj);
        // Notify about a failed save
        if (!success)
        {
            Debug.LogWarning($"could not save as asset to {fileLocation}");
            return;
        }

        // Add the item to the corresponding list in the item settings
        switch (itemType)
        {
            case ItemType.Food:
                if (expensive) settings.FoodSettings.highValuePrefabs.Add(saved);
                else settings.FoodSettings.lowValuePrefabs.Add(saved);
                break;
            case ItemType.MechanicalParts:
                if (expensive) settings.MechanicalPartsSettings.highValuePrefabs.Add(saved);
                else settings.MechanicalPartsSettings.lowValuePrefabs.Add(saved);
                break;
            case ItemType.Medicine:
                if (expensive) settings.MedicineSettings.highValuePrefabs.Add(saved);
                else settings.MedicineSettings.lowValuePrefabs.Add(saved);
                break;
            case ItemType.Fuel:
            default:
                if (expensive) settings.FuelSettings.highValuePrefabs.Add(saved);
                else settings.FuelSettings.lowValuePrefabs.Add(saved);
                break;
        }
    }

    private static int IndexOf(string[] layers, string layer)
    {
        var index = Array.IndexOf(layers, layer);
        return Mathf.Clamp(index, 0, layers.Length - 1);
    }
}
