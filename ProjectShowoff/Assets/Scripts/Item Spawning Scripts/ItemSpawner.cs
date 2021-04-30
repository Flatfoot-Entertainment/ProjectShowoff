using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    ItemFactory itemFactory;
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
            Debug.Log(item);
        }
    }
}
