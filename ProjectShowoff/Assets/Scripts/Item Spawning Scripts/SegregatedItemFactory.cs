using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Segragated Item Factory")]
public class SegregatedItemFactory : ItemFactory
{
	public GameObject[] fuelPrefabs;
	public GameObject[] consumerGoodsPrefabs;
	public GameObject[] foodPrefabs;
	public GameObject[] mechanicalPartsPrefabs;
	public GameObject[] medicinePrefabs;
	public GameObject[] peoplePrefabs;
	public override ConsumerGoods CreateConsumerGoods()
	{
		return new ConsumerGoods(45f, 100);
	}

	public override Food CreateFood()
	{
		return new Food(45f, 100);
	}

	public override Fuel CreateFuel()
	{
		return new Fuel(45f, 100);
	}

	public override MechanicalParts CreateMechanicalParts()
	{
		return new MechanicalParts(45f, 100);
	}

	public override Medicine CreateMedicine()
	{
		return new Medicine(45f, 100);
	}

	public override People CreatePeople()
	{
		return new People(45f, 100);
	}

	public override Item CreateRandomItem()
	{
		Item item = null;
		switch (Extensions.RandomEnumValue<ItemType>())
		{
			case ItemType.Food:
				item = CreateFood();
				break;
			case ItemType.ConsumerGoods:
				item = CreateConsumerGoods();
				break;
			case ItemType.Fuel:
				item = CreateFuel();
				break;
			case ItemType.MechanicalParts:
				item = CreateMechanicalParts();
				break;
			case ItemType.Medicine:
				item = CreateMedicine();
				break;
			case ItemType.People:
			default:
				item = CreatePeople();
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
			case ItemType.People: return peoplePrefabs.Random();
			case ItemType.ConsumerGoods: return consumerGoodsPrefabs.Random();
			case ItemType.Fuel:
			default:
				return fuelPrefabs.Random();
		}
	}
}