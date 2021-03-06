using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class ItemSpawner : MonoBehaviour
{
	public bool CanSpawn
    {
		get => canSpawn;
		set => canSpawn = value;
    }

	[SerializeField] private ItemFactory itemFactory;
	[SerializeField] private bool canSpawn = true;
	// Start is called before the first frame update

	private void InstantiateItem()
	{
		//creates the item
		Item item = itemFactory.CreateRandomItem();
		//instantiates the item, sets material and adds the item script needed for info
		GameObject spawnedItemObject = LeanPool.Spawn(item.ItemPrefab, transform.position, Quaternion.identity);
		if (item.ItemMaterial != null) spawnedItemObject.GetComponent<Renderer>().material = item.ItemMaterial;
		ItemScript itemScript = spawnedItemObject.GetOrAddComponent<ItemScript>();
		itemScript.contained = item;
		// Debug.Log(item);
	}

	public void Spawn()
	{
		if (canSpawn)
		{
			InstantiateItem();
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Z))
		{
			InstantiateItem();
		}
	}
}
