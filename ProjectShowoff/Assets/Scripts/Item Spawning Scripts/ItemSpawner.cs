using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Material[] materials;
    private ItemFactory itemFactory;
    // Start is called before the first frame update
    void Start()
    {
        itemFactory = new SampleItemFactory();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Item item = itemFactory.CreateRandomItem();
            item.ItemPrefab = itemPrefab;
            GameObject spawnedItemObject = Instantiate(item.ItemPrefab, transform.position, Quaternion.identity);
            spawnedItemObject.GetComponent<Renderer>().material = materials[Random.Range(0, materials.Length)];
            ItemScript itemScript = spawnedItemObject.AddComponent<ItemScript>();
            itemScript.Item = item;
            Debug.Log(item);
        }
    }
}
