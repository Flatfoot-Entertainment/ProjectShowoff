using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PercentageItemFactory : MonoBehaviour
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
		public List<GameObject> lowValuePrefabs;
		public List<GameObject> highValuePrefabs;

		public Item GetRandom()
		{
			bool isLowValue = Random.Range(0f, chanceLowValue + chanceHighValue) < chanceLowValue;
			int value = isLowValue
				? lowValue
				: highValue;
			Item item = null;
			switch (type)
			{
				case ItemType.Food:
					item = new Food(value);
					break;
				case ItemType.MechanicalParts:
					item = new MechanicalParts(value);
					break;
				case ItemType.Medicine:
					item = new Medicine(value);
					break;
				case ItemType.Fuel:
				default:
					item = new Fuel(value);
					break;
			}
			item.ItemPrefab = isLowValue ? lowValuePrefabs.Random() : highValuePrefabs.Random();

			return item;
		}
	}

	public ItemTypeSettings FoodSettings => foodSettings;
	[SerializeField] private ItemTypeSettings foodSettings;
	public ItemTypeSettings FuelSettings => fuelSettings;
	[SerializeField] private ItemTypeSettings fuelSettings;
	public ItemTypeSettings MechanicalPartsSettings => mechanicalPartsSettings;
	[SerializeField] private ItemTypeSettings mechanicalPartsSettings;
	public ItemTypeSettings MedicineSettings => medicineSettings;
	[SerializeField] private ItemTypeSettings medicineSettings;

	private IEnumerable<ItemTypeSettings> AllSettings()
	{
		yield return foodSettings;
		yield return fuelSettings;
		yield return mechanicalPartsSettings;
		yield return medicineSettings;
	}

	private float GetWholePercentage()
	{
		float start = 0f;
		foreach (var settings in AllSettings()) start += settings.chance;
		return start;
	}

	public Item CreateRandomItem()
	{
		float randVal = Random.Range(0f, GetWholePercentage());
		float running = 0f;
		foreach (var set in AllSettings())
		{
			if (set.chance + running >= randVal) return set.GetRandom();
			running += set.chance;
		}
		// Weird case, shouldn't happen
		// I like food, so in this edge case, just return a random food item
		return foodSettings.GetRandom();
	}
}
