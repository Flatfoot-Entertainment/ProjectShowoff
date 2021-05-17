using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
	[SerializeField] private ItemFactory itemFactory;
	// Start is called before the first frame update

	private void InstantiateItem()
	{
		//creates the item
		Item item = itemFactory.CreateRandomItem();
		//instantiates the item, sets material and adds the item script needed for info
		GameObject spawnedItemObject = Instantiate(item.ItemPrefab, transform.position, Quaternion.identity);
		spawnedItemObject.GetComponent<Renderer>().material = item.ItemMaterial;
		ItemScript itemScript = spawnedItemObject.AddComponent<ItemScript>();
		itemScript.contained = item;
		// Debug.Log(item);
	}

	public void Spawn()
	{
		InstantiateItem();
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
