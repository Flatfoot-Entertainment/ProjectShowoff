using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Segragated Item Factory")]
public class SegregatedItemFactory : ItemFactory
{
	public GameObject[] fuelPrefabs;
	public GameObject[] foodPrefabs;
	public GameObject[] mechanicalPartsPrefabs;
	public GameObject[] medicinePrefabs;

	private Food CreateFood()
	{
		return new Food(100);
	}

	private Fuel CreateFuel()
	{
		return new Fuel(100);
	}

	private MechanicalParts CreateMechanicalParts()
	{
		return new MechanicalParts(100);
	}

	private Medicine CreateMedicine()
	{
		return new Medicine(100);
	}

	public override Item CreateRandomItem()
	{
		Item item = null;
		switch (Extensions.RandomEnumValue<ItemType>())
		{
			case ItemType.Food:
				item = CreateFood();
				break;
			case ItemType.Fuel:
				item = CreateFuel();
				break;
			case ItemType.MechanicalParts:
				item = CreateMechanicalParts();
				break;
			case ItemType.Medicine:
			default:
				item = CreateMedicine();
				break;
		}
		item.ItemPrefab = GetRandomPrefab(item.Type);

		return item;
	}

	private GameObject GetRandomPrefab(ItemType type)
	{
		switch (type)
		{
			case ItemType.Food: return foodPrefabs.Random();
			case ItemType.MechanicalParts: return mechanicalPartsPrefabs.Random();
			case ItemType.Medicine: return medicinePrefabs.Random();
			case ItemType.Fuel:
			default:
				return fuelPrefabs.Random();
		}
	}
}