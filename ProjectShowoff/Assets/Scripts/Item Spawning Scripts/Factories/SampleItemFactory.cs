using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//sample factory inherited from ItemFactory
//a lot of fixed numbers used, must be made random/"procedurally generated"
public class SampleItemFactory : ItemFactory
{
	public GameObject[] itemPrefabs;
	public Material[] materials;
	public const int NUMBER_OF_UNIQUE_ITEMS = 6;
	//implement item prefab spawning here by using scriptable objects
	public override Item CreateRandomItem()
	{
		Item item = null;
		int rand = UnityEngine.Random.Range(0, NUMBER_OF_UNIQUE_ITEMS);
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
			default:
			case ItemType.Medicine:
				item = CreateMedicine();
				break;
		}
		//to be moved to each specific item later on, but now its only one item prefab and random material assigned so yeah
		item.ItemPrefab = itemPrefabs.Random();
		item.ItemMaterial = materials.Random();
		return item;
	}

	private Food CreateFood()
	{
		return new Food(45f, 100);
	}

	private Fuel CreateFuel()
	{
		return new Fuel(45f, 100);
	}

	private MechanicalParts CreateMechanicalParts()
	{
		return new MechanicalParts(45f, 100);
	}

	private Medicine CreateMedicine()
	{
		return new Medicine(45f, 100);
	}

}
