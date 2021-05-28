using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Tooling/Item Spawning Settings")]
public class ItemSpawningSettings : ScriptableObject
{
	// TODO maybe a custom inspector that automatically validates percentages
	[System.Serializable]
	public class ItemTypeSettings
	{
		public ItemType type;
		public float chance;
		public float chanceLowValue;
		public int lowValue;
		public float chanceHighValue;
		public int highValue;
		public List<GameObject> lowValuePrefabs = new List<GameObject>();
		public List<GameObject> highValuePrefabs = new List<GameObject>();

		public GameObject GetRandom()
		{
			bool isLowValue = Random.Range(0f, chanceLowValue + chanceHighValue) < chanceLowValue;
			int value = isLowValue
				? lowValue
				: highValue;
			Item item = type switch
			{
				ItemType.Food => new Food(value),
				ItemType.MechanicalParts => new MechanicalParts(value),
				ItemType.Medicine => new Medicine(value),
				_ => new Fuel(value)
			};
			GameObject prefab = isLowValue ? lowValuePrefabs.Random() : highValuePrefabs.Random();
			ItemScript script = prefab.GetComponent<ItemScript>();
			if (!script) Debug.LogError("WTF???");
			script.contained = item;

			return prefab;
		}
	}

	public ItemTypeSettings FoodSettings => foodSettings;
	[SerializeField] private ItemTypeSettings foodSettings = new ItemTypeSettings();
	public ItemTypeSettings FuelSettings => fuelSettings;
	[SerializeField] private ItemTypeSettings fuelSettings = new ItemTypeSettings();
	public ItemTypeSettings MechanicalPartsSettings => mechanicalPartsSettings;
	[SerializeField] private ItemTypeSettings mechanicalPartsSettings = new ItemTypeSettings();
	public ItemTypeSettings MedicineSettings => medicineSettings;
	[SerializeField] private ItemTypeSettings medicineSettings = new ItemTypeSettings();

	public IEnumerable<ItemTypeSettings> AllSettings()
	{
		yield return foodSettings;
		yield return fuelSettings;
		yield return mechanicalPartsSettings;
		yield return medicineSettings;
	}

	public float GetWholePercentage()
	{
		float start = 0f;
		foreach (var s in AllSettings()) start += s.chance;
		return start;
	}
}
