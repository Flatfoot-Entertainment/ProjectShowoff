using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
	//temporary variables for item creation (planned to be moved over to SampleItemFactory)
	[SerializeField] private GameObject itemPrefab;
	[SerializeField] private Material[] materials;
	private ItemFactory itemFactory;
	// Start is called before the first frame update
	void Awake()
	{
		itemFactory = new SampleItemFactory();
	}

	private void InstantiateItem()
	{
		//creates the item
		Item item = itemFactory.CreateRandomItem();
		item.ItemPrefab = itemPrefab;
		//instantiates the item, sets material and adds the item script needed for info
		GameObject spawnedItemObject = Instantiate(item.ItemPrefab, transform.position, Quaternion.identity);
		spawnedItemObject.GetComponent<Renderer>().material = materials[Random.Range(0, materials.Length)];
		ItemScript itemScript = spawnedItemObject.AddComponent<ItemScript>();
		itemScript.Item = item;
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
