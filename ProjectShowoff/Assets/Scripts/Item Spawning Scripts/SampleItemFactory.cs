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
        switch (rand)
        {
            case 0:
                item = CreateConsumerGoods();
                break;
            case 1:
                item = CreateFood();
                break;
            case 2:
                item = CreateFuel();
                break;
            case 3:
                item = CreateMechanicalParts();
                break;
            case 4:
                item = CreateMedicine();
                break;
            case 5:
                item = CreatePeople();
                break;
            default:
                break;
        }
        if(item is null)
        {
            throw new NullReferenceException("dafuq is this item it doesnt exist");
        }
        //to be moved to each specific item later on, but now its only one item prefab and random material assigned so yeah
        item.ItemPrefab = itemPrefabs[UnityEngine.Random.Range(0, itemPrefabs.Length)];
        item.ItemMaterial = materials[UnityEngine.Random.Range(0, materials.Length)];
        return item;
    }

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

}
