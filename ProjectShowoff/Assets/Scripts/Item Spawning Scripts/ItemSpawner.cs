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

    [SerializeField] private Transform itemsSpawned;
    [SerializeField] private PercentageItemFactory itemFactory;
    [SerializeField] private bool canSpawn = true;
    // Start is called before the first frame update

    private void Awake()
    {
        itemFactory = FindObjectOfType<PercentageItemFactory>();
        itemsSpawned = GameObject.Find("Items Spawned").transform;
    }

	private void InstantiateItem()
	{
		//creates the item
		GameObject item = itemFactory.CreateRandomItem();
		//instantiates the item, sets material and adds the item script needed for info
		GameObject spawnedItemObject = LeanPool.Spawn(item, transform.position, Quaternion.identity);
		//since we're using pooling, objects get reused and velocity carries over - this makes sure it doesn't
		spawnedItemObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
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
        // if (Input.GetKeyDown(KeyCode.Z))
        // {
        // 	InstantiateItem();
        // }
    }

    public IEnumerator Stop(float delay)
    {
        CanSpawn = false;
        yield return new WaitForSeconds(delay);
        CanSpawn = true;
    }
}
